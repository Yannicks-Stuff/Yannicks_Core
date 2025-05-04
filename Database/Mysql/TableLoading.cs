namespace Yannick.Database.Mysql;

public sealed partial class Table
{
    private readonly object _layoutFetchLock = new();
    private TableLayout? _cachedLayout;


    public IReadOnlyCollection<RowEntry> Load(string extraCode = "")
    {
        if (_connection == null)
            return new List<RowEntry>();

        var data = SelectDirect(addition: extraCode);
        var list = new List<RowEntry>();

        foreach (var e in data)
            list.Add(new RowEntry(e, this));

        return list;
    }

    public async Task<IReadOnlyDictionary<string, object?>> SelectSingleAsync(string where,
        CancellationToken token = default)
    {
        await foreach (var i in SelectAsync(["*"], $"WHERE {where} LIMIT 1", token))
            return i;

        return new Dictionary<string, object?>();
    }

    /// <summary>
    /// Cache the layout in memory to reduce DB hits.
    /// </summary>
    private async Task<TableLayout> GetCachedLayoutAsync(CancellationToken token = default)
    {
        if (_cachedLayout != null)
            return _cachedLayout.Value;

        lock (_layoutFetchLock)
        {
            if (_cachedLayout != null)
                return _cachedLayout.Value;
        }

        var layout = await GetLayoutAsync(token).ConfigureAwait(false);

        lock (_layoutFetchLock)
        {
            _cachedLayout ??= layout;
        }

        return _cachedLayout.Value;
    }

    /// <summary>
    /// Builds a WHERE clause based on the table's "best" unique columns:
    ///  1) Primary key columns (PRI)
    ///  2) If none found, Unique columns (UNI)
    ///  3) If none found, optionally fallback to indexed columns (MUL)
    /// 
    /// You can configure whether to skip or throw if a key column is missing in <paramref name="row"/>.
    /// You can also add custom type handling for different data types.
    /// </summary>
    /// <param name="row">
    /// A dictionary of column-name-to-value pairs representing the row data.
    /// The dictionary must contain entries for any key columns used by the table.
    /// </param>
    /// <param name="useIndexedFallback">
    /// If true, after failing to find PRI or UNI columns, we look for "MUL" columns as a last resort.
    /// </param>
    /// <param name="throwIfMissingKey">
    /// If true, throws an exception if the row is missing a required column. If false, that column is skipped.
    /// </param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>A SQL WHERE clause string or an empty string if no suitable columns are found.</returns>
    public async Task<string> BuildWhereClauseAsync(
        Dictionary<string, object?> row,
        bool useIndexedFallback = true,
        bool throwIfMissingKey = false,
        CancellationToken token = default)
    {
        if (row == null || row.Count == 0)
            return string.Empty;

        // 1. Get the table layout (columns).
        var layout = await GetCachedLayoutAsync(token).ConfigureAwait(false);
        var columns = layout.Columns;

        // 2. Collect columns by key type.
        var priCols = new List<string>();
        var uniCols = new List<string>();
        var mulCols = new List<string>();

        foreach (var col in columns)
        {
            // MySQL sets Key == "PRI" for primary key, "UNI" for unique, and "MUL" for indexed multi columns
            if (string.Equals(col.Key, "PRI", StringComparison.OrdinalIgnoreCase))
                priCols.Add(col.Field);
            else if (string.Equals(col.Key, "UNI", StringComparison.OrdinalIgnoreCase))
                uniCols.Add(col.Field);
            else if (string.Equals(col.Key, "MUL", StringComparison.OrdinalIgnoreCase))
                mulCols.Add(col.Field);
        }

        // 3. Determine which columns we’ll actually use, in order of priority.
        List<string> selectedCols;
        if (priCols.Count > 0)
        {
            selectedCols = priCols;
        }
        else if (uniCols.Count > 0)
        {
            selectedCols = uniCols;
        }
        else if (useIndexedFallback && mulCols.Count > 0)
        {
            selectedCols = mulCols;
        }
        else
        {
            // No suitable columns found
            return string.Empty;
        }

        // 4. Build conditions from the row dictionary for the selected columns.
        var conditions = new List<string>();
        foreach (var colName in selectedCols)
        {
            if (!row.TryGetValue(colName, out var value))
            {
                // If the dictionary is missing the key column, decide how to handle it.
                if (throwIfMissingKey)
                {
                    throw new KeyNotFoundException(
                        $"Row is missing the required column '{colName}' for the WHERE clause.");
                }
                else
                {
                    // Skip this column
                    continue;
                }
            }

            // 5. Apply different logic for different data types (example only).
            switch (value)
            {
                case null:
                    // e.g., `columnName IS NULL`
                    conditions.Add($"`{colName}` IS NULL");
                    break;
                case string strVal:
                    // In a real implementation, escape strVal to prevent SQL injection.
                    conditions.Add($"`{colName}` = '{strVal}'");
                    break;
                case DateTime dateTimeVal:
                    // Format as needed for your DB engine, e.g. yyyy-MM-dd HH:mm:ss
                    var dateString = dateTimeVal.ToString("yyyy-MM-dd HH:mm:ss");
                    conditions.Add($"`{colName}` = '{dateString}'");
                    break;
                case bool boolVal:
                    // Some DB engines use 1/0, others use true/false
                    conditions.Add($"`{colName}` = {(boolVal ? 1 : 0)}");
                    break;
                default:
                    // For numeric or other data types, just convert to string
                    // (still consider injection safe-handling if it's user input).
                    conditions.Add($"`{colName}` = {value}");
                    break;
            }
        }

        // 6. If no columns ended up being used (e.g., all missing or no row data), return empty or decide on fallback
        if (conditions.Count == 0)
            return string.Empty;

        return "WHERE " + string.Join(" AND ", conditions);
    }

    public struct RowEntry
    {
        private readonly Table _table;

        /// <summary>
        /// The original data as loaded from DB (or as last accepted).
        /// </summary>
        private readonly Dictionary<string, object?> _data;

        /// <summary>
        /// The set of updated columns/values that haven’t yet been persisted.
        /// </summary>
        private readonly Dictionary<string, object?> _updatedData;

        /// <summary>
        /// Create a new RowEntry from read-only data plus a reference to the Table.
        /// </summary>
        public RowEntry(IReadOnlyDictionary<string, object?> data, Table table)
        {
            _table = table;
            _data = new Dictionary<string, object?>(data);
            _updatedData = new Dictionary<string, object?>();
        }

        /// <summary>
        /// Indexer for getting/setting fields. If a field is in _updatedData, returns that;
        /// otherwise returns the original data.
        /// </summary>
        public object? this[string key]
        {
            get => _updatedData.TryGetValue(key, out var val) ? val : _data[key];
            set
            {
                if (!_updatedData.TryAdd(key, value))
                {
                    _updatedData[key] = value;
                }
            }
        }

        /// <summary>
        /// Returns true if the row has any pending changes that are not yet persisted.
        /// </summary>
        public bool HasChanges => _updatedData.Count > 0;

        /// <summary>
        /// Discards any local changes, reverting to the last known data from DB.
        /// </summary>
        public void DiscardChanges()
        {
            _updatedData.Clear();
        }

        /// <summary>
        /// Accepts changes into _data without reloading from DB.
        /// (In other words, we say “this is now our new baseline”.)
        /// </summary>
        public void AcceptChanges()
        {
            foreach (var kvp in _updatedData)
            {
                _data[kvp.Key] = kvp.Value;
            }

            _updatedData.Clear();
        }

        /// <summary>
        /// Reloads the data from the DB, discarding local changes.
        /// This method uses your table’s single-row SELECT to re-fetch the data.
        /// </summary>
        public async Task ReloadAsync(CancellationToken token = default)
        {
            // Build a WHERE clause from our original data
            var whereClause = await _table.BuildWhereClauseAsync(_data, token: token);
            if (string.IsNullOrEmpty(whereClause))
                throw new InvalidOperationException("Cannot reload row. No valid WHERE clause found.");

            var newData = await _table.SelectSingleAsync(whereClause, token);
            if (newData == null)
            {
                // Row might have been deleted or we no longer match
                throw new Exception("Row not found in DB when reloading. It may have been deleted or changed.");
            }

            // Clear & store the fresh data
            _data.Clear();
            foreach (var kvp in newData)
            {
                _data[kvp.Key] = kvp.Value;
            }

            // All local changes are discarded
            _updatedData.Clear();
        }

        /// <summary>
        /// Update/Insert changes in the DB, if any. After success, updates our baseline data.
        /// </summary>
        public async Task UpdateAsync(CancellationToken token = default)
        {
            if (_updatedData.Count == 0)
                return; // No changes to save

            // Build a WHERE clause from the original data
            var whereClause = await _table.BuildWhereClauseAsync(_data, token: token);
            if (string.IsNullOrEmpty(whereClause))
                throw new Exception("No valid WHERE clause. Cannot update.");

            // Update in DB
            await _table.UpdateInsertAsync(_updatedData, whereClause, token);

            // If successful, move updated data into _data
            AcceptChanges();
        }
    }

    #region Index Structures

    /// <summary>
    /// Represents an index definition in a MySQL table.
    /// </summary>
    public readonly struct IndexDef
    {
        public string KeyName { get; }
        public bool IsUnique { get; }
        public IReadOnlyList<string> ColumnNames { get; }
        public string IndexType { get; }

        public IndexDef(string keyName, bool isUnique, IReadOnlyList<string> columnNames, string indexType)
        {
            KeyName = keyName;
            IsUnique = isUnique;
            ColumnNames = columnNames;
            IndexType = indexType;
        }
    }

    #endregion

    #region Foreign Key Structures

    /// <summary>
    /// Represents a foreign key definition in a MySQL table, including possible multi-column references.
    /// </summary>
    public readonly struct ForeignKeyDef
    {
        /// <summary>The name of the foreign key constraint.</summary>
        public string ConstraintName { get; }

        /// <summary>The local (child) columns that participate in this foreign key.</summary>
        public IReadOnlyList<string> LocalColumns { get; }

        /// <summary>The referenced table's schema (database name).</summary>
        public string ReferencedTableSchema { get; }

        /// <summary>The referenced table's name (parent table).</summary>
        public string ReferencedTable { get; }

        /// <summary>The referenced column names in the parent table.</summary>
        public IReadOnlyList<string> ReferencedColumns { get; }

        public ForeignKeyDef(
            string constraintName,
            IReadOnlyList<string> localColumns,
            string referencedTableSchema,
            string referencedTable,
            IReadOnlyList<string> referencedColumns)
        {
            ConstraintName = constraintName;
            LocalColumns = localColumns;
            ReferencedTableSchema = referencedTableSchema;
            ReferencedTable = referencedTable;
            ReferencedColumns = referencedColumns;
        }
    }

    #endregion

    #region Consolidated Table Metadata (Columns + Indexes + FKs)

    /// <summary>
    /// Represents full table metadata that includes columns, indexes, and foreign keys.
    /// </summary>
    public readonly struct TableMetadataEx
    {
        public string TableName { get; }
        public IReadOnlyList<ColumnDef> Columns { get; }
        public IReadOnlyList<IndexDef> Indexes { get; }
        public IReadOnlyList<ForeignKeyDef> ForeignKeys { get; }

        public TableMetadataEx(
            string tableName,
            IReadOnlyList<ColumnDef> columns,
            IReadOnlyList<IndexDef> indexes,
            IReadOnlyList<ForeignKeyDef> foreignKeys)
        {
            TableName = tableName;
            Columns = columns;
            Indexes = indexes;
            ForeignKeys = foreignKeys;
        }
    }

    #endregion

    #region Column Structures

    /// <summary>
    /// Represents a single column definition in a table, including extras like Collation, Privileges, and Comment.
    /// </summary>
    public readonly struct ColumnDef
    {
        public string Field { get; }
        public string Type { get; }
        public string Null { get; }
        public string Key { get; }
        public string Default { get; }
        public string Extra { get; }
        public string Collation { get; }
        public string Privileges { get; }
        public string Comment { get; }

        public ColumnDef(
            string field,
            string type,
            string isNullable,
            string key,
            string defaultValue,
            string extra,
            string collation,
            string privileges,
            string comment)
        {
            Field = field;
            Type = type;
            Null = isNullable;
            Key = key;
            Default = defaultValue;
            Extra = extra;
            Collation = collation;
            Privileges = privileges;
            Comment = comment;
        }
    }

    /// <summary>
    /// Represents the layout of a table, including its name and list of columns.
    /// </summary>
    public readonly struct TableLayout
    {
        public string TableName { get; }
        public IReadOnlyList<ColumnDef> Columns { get; }

        public TableLayout(string tableName, IReadOnlyList<ColumnDef> columns)
        {
            TableName = tableName;
            Columns = columns;
        }
    }

    #endregion

// Your class has the following fields/properties assumed to exist:
//    - private readonly IMyDbConnection _connection; // or something similar
//    - public MyDatabaseInfo Database { get; }       // which has Database.Name
//    - private string _name;                         // the current table name

    #region Retrieve Columns

    /// <summary>
    /// Asynchronously retrieves the table layout (columns) for the current table, including collation, privileges, comment, etc.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns>A <see cref="TableLayout"/> containing the table name and its columns.</returns>
    public async Task<TableLayout> GetLayoutAsync(CancellationToken token = default)
    {
        if (_connection == null)
            throw new InvalidOperationException("Connection is not initialized.");
        if (Database == null || string.IsNullOrWhiteSpace(Database.Name))
            throw new InvalidOperationException("Database is not specified.");
        if (string.IsNullOrWhiteSpace(_name))
            throw new InvalidOperationException("Table name is not specified.");

        // SHOW FULL COLUMNS includes collation, privileges, comment
        // (Columns typically: Field, Type, Collation, Null, Key, Default, Extra, Privileges, Comment)
        var query = $"SHOW FULL COLUMNS FROM `{Database.Name}`.`{_name}`;";
        var result = _connection.SelectAsync(query, token).ConfigureAwait(false);

        var columns = new List<ColumnDef>();
        await foreach (var row in result)
        {
            columns.Add(new ColumnDef(
                field: row["Field"]?.ToString() ?? "",
                type: row["Type"]?.ToString() ?? "",
                isNullable: row["Null"]?.ToString() ?? "",
                key: row["Key"]?.ToString() ?? "",
                defaultValue: row["Default"]?.ToString(),
                extra: row["Extra"]?.ToString() ?? "",
                collation: row["Collation"]?.ToString(),
                privileges: row["Privileges"]?.ToString(),
                comment: row["Comment"]?.ToString()
            ));
        }

        return new TableLayout(_name, columns);
    }

    /// <summary>
    /// Retrieves the table layout (columns) synchronously for the current table.
    /// In general, prefer the asynchronous version if possible.
    /// </summary>
    /// <returns>A <see cref="TableLayout"/> containing the table name and its columns.</returns>
    public TableLayout GetLayout()
    {
        return GetLayoutAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    #endregion

    #region Retrieve Indexes

    /// <summary>
    /// Asynchronously retrieves the index definitions for the current table.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns>A list of <see cref="IndexDef"/> for the current table.</returns>
    public async Task<IReadOnlyList<IndexDef>> GetIndexesAsync(CancellationToken token = default)
    {
        if (_connection == null)
            throw new InvalidOperationException("Connection is not initialized.");
        if (Database == null || string.IsNullOrWhiteSpace(Database.Name))
            throw new InvalidOperationException("Database is not specified.");
        if (string.IsNullOrWhiteSpace(_name))
            throw new InvalidOperationException("Table name is not specified.");

        // SHOW INDEX returns columns like: Table, Non_unique, Key_name, Seq_in_index, Column_name, Collation, ...
        var query = $"SHOW INDEX FROM `{Database.Name}`.`{_name}`;";
        var result = _connection.SelectAsync(query, token).ConfigureAwait(false);

        // We need to group rows by Key_name to gather multiple columns in one index
        var indexMap = new Dictionary<string, List<string>>();
        var indexInfo = new Dictionary<string, (bool IsUnique, string IndexType)>();

        await foreach (var row in result)
        {
            var keyName = row["Key_name"]?.ToString() ?? "";
            var columnName = row["Column_name"]?.ToString() ?? "";

            // Non_unique = 0 => index is unique; 1 => not unique
            bool nonUniqueParsed = int.TryParse(row["Non_unique"]?.ToString(), out var nonUnique);
            bool isUnique = (nonUniqueParsed && nonUnique == 0);

            // MySQL's SHOW INDEX has an 'Index_type' column
            var indexType = row["Index_type"]?.ToString() ?? "";

            if (!indexMap.ContainsKey(keyName))
                indexMap[keyName] = new List<string>();
            indexMap[keyName].Add(columnName);

            if (!indexInfo.ContainsKey(keyName))
                indexInfo[keyName] = (IsUnique: isUnique, IndexType: indexType);
        }

        // Now compose a list of IndexDef from the dictionary
        var indexes = new List<IndexDef>();
        foreach (var kvp in indexMap)
        {
            var keyName = kvp.Key;
            var columns = kvp.Value;
            var info = indexInfo[keyName];

            indexes.Add(new IndexDef(
                keyName: keyName,
                isUnique: info.IsUnique,
                columnNames: columns,
                indexType: info.IndexType
            ));
        }

        return indexes;
    }

    /// <summary>
    /// Retrieves the index definitions for the current table (synchronously).
    /// </summary>
    /// <returns>A list of <see cref="IndexDef"/> for the current table.</returns>
    public IReadOnlyList<IndexDef> GetIndexes()
    {
        return GetIndexesAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    #endregion

    #region Retrieve Foreign Keys

    /// <summary>
    /// Asynchronously retrieves the foreign key definitions for the current table from INFORMATION_SCHEMA.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns>A list of <see cref="ForeignKeyDef"/> for the current table.</returns>
    public async Task<IReadOnlyList<ForeignKeyDef>> GetForeignKeysAsync(CancellationToken token = default)
    {
        if (_connection == null)
            throw new InvalidOperationException("Connection is not initialized.");
        if (Database == null || string.IsNullOrWhiteSpace(Database.Name))
            throw new InvalidOperationException("Database is not specified.");
        if (string.IsNullOrWhiteSpace(_name))
            throw new InvalidOperationException("Table name is not specified.");

        // This query grabs only foreign key constraints (REFERENCED_TABLE_NAME != NULL).
        // We'll group rows by ConstraintName to handle multi-column FKs.
        var query = $@"
        SELECT
            TABLE_SCHEMA,
            TABLE_NAME,
            COLUMN_NAME,
            CONSTRAINT_NAME,
            REFERENCED_TABLE_SCHEMA,
            REFERENCED_TABLE_NAME,
            REFERENCED_COLUMN_NAME
        FROM information_schema.KEY_COLUMN_USAGE
        WHERE
            TABLE_SCHEMA = '{Database.Name}'
            AND TABLE_NAME = '{_name}'
            AND REFERENCED_TABLE_NAME IS NOT NULL;
    ";

        var result = _connection.SelectAsync(query, token).ConfigureAwait(false);

        // Dictionary grouping by foreign key name (constraint name).
        // We'll store a list of (LocalColumn, ReferencedColumn).
        var fkColumnsMap = new Dictionary<string, List<(string localCol, string refCol)>>();
        // Dictionary storing the basic info (schema, referenced table) for each foreign key.
        var fkInfo = new Dictionary<string, (string RefSchema, string RefTable)>();

        await foreach (var row in result)
        {
            var constraintName = row["CONSTRAINT_NAME"]?.ToString() ?? "";
            var localCol = row["COLUMN_NAME"]?.ToString() ?? "";
            var refSchema = row["REFERENCED_TABLE_SCHEMA"]?.ToString() ?? "";
            var refTable = row["REFERENCED_TABLE_NAME"]?.ToString() ?? "";
            var refCol = row["REFERENCED_COLUMN_NAME"]?.ToString() ?? "";

            if (!fkColumnsMap.ContainsKey(constraintName))
                fkColumnsMap[constraintName] = new List<(string, string)>();

            fkColumnsMap[constraintName].Add((localCol, refCol));

            if (!fkInfo.ContainsKey(constraintName))
                fkInfo[constraintName] = (RefSchema: refSchema, RefTable: refTable);
        }

        // Build a list of ForeignKeyDef objects
        var foreignKeys = new List<ForeignKeyDef>();
        foreach (var kvp in fkColumnsMap)
        {
            var constraintName = kvp.Key;
            var pairs = kvp.Value;

            // Separate out local vs referenced columns
            var localCols = new List<string>();
            var refCols = new List<string>();
            foreach (var (local, reference) in pairs)
            {
                localCols.Add(local);
                refCols.Add(reference);
            }

            var info = fkInfo[constraintName];

            var fk = new ForeignKeyDef(
                constraintName: constraintName,
                localColumns: localCols,
                referencedTableSchema: info.RefSchema,
                referencedTable: info.RefTable,
                referencedColumns: refCols
            );
            foreignKeys.Add(fk);
        }

        return foreignKeys;
    }

    /// <summary>
    /// Retrieves the foreign key definitions for the current table (synchronously).
    /// </summary>
    /// <returns>A list of <see cref="ForeignKeyDef"/> for the current table.</returns>
    public IReadOnlyList<ForeignKeyDef> GetForeignKeys()
    {
        return GetForeignKeysAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    #endregion

    #region Consolidated Extended Metadata

    /// <summary>
    /// Asynchronously retrieves the full metadata (columns + indexes + foreign keys) of the current table.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns>A <see cref="TableMetadataEx"/> representing columns, indexes, and foreign keys.</returns>
    public async Task<TableMetadataEx> GetTableMetadataExAsync(CancellationToken token = default)
    {
        var layout = await GetLayoutAsync(token).ConfigureAwait(false);
        var indexes = await GetIndexesAsync(token).ConfigureAwait(false);
        var foreignKeys = await GetForeignKeysAsync(token).ConfigureAwait(false);

        return new TableMetadataEx(
            tableName: layout.TableName,
            columns: layout.Columns,
            indexes: indexes,
            foreignKeys: foreignKeys
        );
    }

    /// <summary>
    /// Retrieves the full metadata (columns + indexes + foreign keys) for the current table (synchronously).
    /// </summary>
    /// <returns>A <see cref="TableMetadataEx"/> representing columns, indexes, and foreign keys.</returns>
    public TableMetadataEx GetTableMetadataEx()
    {
        return GetTableMetadataExAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    #endregion
}