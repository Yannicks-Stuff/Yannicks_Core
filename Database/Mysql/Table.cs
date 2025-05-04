using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Yannick.Extensions.ArrayExtensions;
using System.Reflection;
using MySql.Data.MySqlClient;
using Yannick.Native.OS.Windows.Apps;

namespace Yannick.Database.Mysql;

/// <summary>
/// Represents a table within a MySQL database with full CRUD operations,
/// schema manipulation and optimization capabilities.
/// </summary>
public sealed partial class Table
{
    private readonly Connection? _connection;

    /// <summary>
    /// The database associated with the table.
    /// </summary>
    public readonly Database? Database;

    private string? _name;

    /// <summary>
    /// Private constructor for internal use.
    /// </summary>
    private Table()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Table"/> class.
    /// </summary>
    /// <param name="database">The associated database.</param>
    /// <param name="name">The name of the table.</param>
    /// <exception cref="ArgumentNullException">Thrown when database or name is null.</exception>
    public Table(Database database, string name)
    {
        Database = database ?? throw new ArgumentNullException(nameof(database));
        _connection = database;
        _name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Gets or sets the name of the table.
    /// </summary>
    public string Name
    {
        get => _name ?? string.Empty;
        set => Rename(value);
    }

    /// <summary>
    /// Implicitly converts a Table to its associated Database.
    /// </summary>
    /// <param name="t">The table to convert.</param>
    /// <returns>The database associated with the table.</returns>
    public static implicit operator Database?(Table t) => t.Database;

    #region Optimization

    /// <summary>
    /// Flags for table optimization operations.
    /// </summary>
    [Flags]
    public enum OptimisationFlags
    {
        /// <summary>No optimization.</summary>
        None = 0,

        /// <summary>Optimize table structure.</summary>
        OptimizeTable = 1,

        /// <summary>Update table statistics.</summary>
        UpdateStatistics = 4,

        /// <summary>Clear table cache.</summary>
        ClearTableCache = 8,

        /// <summary>Convert to InnoDB engine.</summary>
        ConvertToInnoDB = 16,

        /// <summary>Convert to MyISAM engine.</summary>
        ConvertToMyISAM = 32,

        /// <summary>Check table for errors.</summary>
        CheckTable = 64,

        /// <summary>Repair table.</summary>
        RepairTable = 128,

        /// <summary>Compress table data.</summary>
        CompressTable = 512
    }


    /// <summary>
    /// Optimizes a table using the specified optimization flags.
    /// </summary>
    /// <param name="tb">The table to optimize.</param>
    /// <param name="flags">The optimization flags.</param>
    /// <exception cref="InvalidOperationException">Thrown when conflicting flags are provided.</exception>
    public static void Optimise(Table tb, OptimisationFlags flags)
    {
        if (tb.Database == null || tb._connection == null)
            return;

        if (flags.HasFlag(OptimisationFlags.ConvertToInnoDB) && flags.HasFlag(OptimisationFlags.ConvertToMyISAM))
        {
            throw new InvalidOperationException("Cannot convert to both InnoDB and MyISAM simultaneously");
        }

        var con = (Connection)tb;

        if (flags.HasFlag(OptimisationFlags.ConvertToInnoDB))
            con.UpdateSelect($"ALTER TABLE `{tb.Database.Name}`.`{tb.Name}` ENGINE=INNODB");

        if (flags.HasFlag(OptimisationFlags.ConvertToMyISAM))
            con.UpdateSelect($"ALTER TABLE `{tb.Database.Name}`.`{tb.Name}` ENGINE=MYISAM");

        if (flags.HasFlag(OptimisationFlags.CompressTable))
            con.UpdateSelect($"ALTER TABLE `{tb.Database.Name}`.`{tb.Name}` ROW_FORMAT=COMPRESSED");

        if (flags.HasFlag(OptimisationFlags.OptimizeTable))
            con.UpdateSelect($"OPTIMIZE TABLE `{tb.Database.Name}`.`{tb.Name}`");

        if (flags.HasFlag(OptimisationFlags.UpdateStatistics))
            con.UpdateSelect($"ANALYZE TABLE `{tb.Database.Name}`.`{tb.Name}`");

        if (flags.HasFlag(OptimisationFlags.CheckTable))
            con.UpdateSelect($"CHECK TABLE `{tb.Database.Name}`.`{tb.Name}`");

        if (flags.HasFlag(OptimisationFlags.RepairTable))
            con.UpdateSelect($"REPAIR TABLE `{tb.Database.Name}`.`{tb.Name}`");

        if (flags.HasFlag(OptimisationFlags.ClearTableCache))
            con.UpdateSelect($"FLUSH TABLE `{tb.Database.Name}`.`{tb.Name}`");
    }

    /// <summary>
    /// Optimizes a table asynchronously using the specified optimization flags.
    /// </summary>
    /// <param name="tb">The table to optimize.</param>
    /// <param name="flags">The optimization flags.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when conflicting flags are provided.</exception>
    public static async Task OptimiseAsync(Table tb, OptimisationFlags flags, CancellationToken token = default)
    {
        if (tb.Database == null || tb._connection == null)
            return;

        if (flags.HasFlag(OptimisationFlags.ConvertToInnoDB) && flags.HasFlag(OptimisationFlags.ConvertToMyISAM))
        {
            throw new InvalidOperationException("Cannot convert to both InnoDB and MyISAM simultaneously");
        }

        var con = (Connection)tb;

        if (flags.HasFlag(OptimisationFlags.ConvertToInnoDB))
            await con.UpdateSelectAsync($"ALTER TABLE `{tb.Database.Name}`.`{tb.Name}` ENGINE=INNODB", token);

        if (flags.HasFlag(OptimisationFlags.ConvertToMyISAM))
            await con.UpdateSelectAsync($"ALTER TABLE `{tb.Database.Name}`.`{tb.Name}` ENGINE=MYISAM", token);

        if (flags.HasFlag(OptimisationFlags.CompressTable))
            await con.UpdateSelectAsync($"ALTER TABLE `{tb.Database.Name}`.`{tb.Name}` ROW_FORMAT=COMPRESSED", token);

        if (flags.HasFlag(OptimisationFlags.OptimizeTable))
            await con.UpdateSelectAsync($"OPTIMIZE TABLE `{tb.Database.Name}`.`{tb.Name}`", token);

        if (flags.HasFlag(OptimisationFlags.UpdateStatistics))
            await con.UpdateSelectAsync($"ANALYZE TABLE `{tb.Database.Name}`.`{tb.Name}`", token);

        if (flags.HasFlag(OptimisationFlags.CheckTable))
            await con.UpdateSelectAsync($"CHECK TABLE `{tb.Database.Name}`.`{tb.Name}`", token);

        if (flags.HasFlag(OptimisationFlags.RepairTable))
            await con.UpdateSelectAsync($"REPAIR TABLE `{tb.Database.Name}`.`{tb.Name}`", token);

        if (flags.HasFlag(OptimisationFlags.ClearTableCache))
            await con.UpdateSelectAsync($"FLUSH TABLE `{tb.Database.Name}`.`{tb.Name}`", token);
    }

    #endregion

    #region Indexing

    /// <summary>
    /// Normalize indexes starting from the specified start table.
    /// This will process only the subgraph of tables connected to startTable.
    /// </summary>
    /// <param name="primaryKeyColumnName">The primary key column name, defaults to "id".</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void NormalizeIndexesFromTable(string primaryKeyColumnName = "id") =>
        MysqlDatabaseReindexer.DatabaseReindexer.Start(this, primaryKeyColumnName);

    /// <summary>
    /// Normalize indexes starting from the specified start table asynchronously.
    /// </summary>
    /// <param name="primaryKeyColumnName">The primary key column name, defaults to "id".</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task NormalizeIndexesFromTableAsync(string primaryKeyColumnName = "id",
        CancellationToken token = default)
    {
        await Task.Run(() => NormalizeIndexesFromTable(primaryKeyColumnName), token);
    }

    #endregion

    #region Existence Checks

    /// <summary>
    /// Asynchronously checks if the table exists.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <param name="names">Optional names to check against.</param>
    /// <returns>True if the table exists; otherwise, false.</returns>
    public async Task<bool> IsTableExistsAsync(CancellationToken token, params string[] names)
    {
        if (_name == null || Database == null)
            return false;

        if (names.Length == 0)
            names = new[] { _name };

        await foreach (var t in Database.ShowTablesAsync(token).ConfigureAwait(false))
            if (names.Any(e => e.Equals(t._name, StringComparison.OrdinalIgnoreCase)))
                return true;

        return false;
    }

    /// <summary>
    /// Checks if the table exists.
    /// </summary>
    /// <param name="names">Optional names to check against.</param>
    /// <returns>True if the table exists; otherwise, false.</returns>
    public bool IsTableExists(params string[] names)
    {
        if (_name == null || Database == null)
            return false;

        if (names.Length == 0)
            names = new[] { _name };

        foreach (var t in Database.ShowTables())
            if (names.Any(e => e.Equals(t._name, StringComparison.OrdinalIgnoreCase)))
                return true;

        return false;
    }

    #endregion

    #region SELECT Operations

    #region ORM Mapping Attributes

    /// <summary>
    /// Attribute used to specify the column name for a property or field when mapping database results.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ColumnNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnNameAttribute"/> class.
        /// </summary>
        /// <param name="name">The column name.</param>
        public ColumnNameAttribute(string name) => Name = name;

        /// <summary>
        /// Gets the column name.
        /// </summary>
        public string Name { get; }
    }

    /// <summary>
    /// Attribute used to ignore a property or field when mapping database results.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ColumnIgnoreAttribute : Attribute
    {
    }

    /// <summary>
    /// Attribute used to specify a foreign key relationship for a property or field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ForeignKeyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyAttribute"/> class.
        /// </summary>
        /// <param name="referenceTable">The reference table name.</param>
        /// <param name="referenceColumn">The reference column name.</param>
        /// <param name="localColumn">The local column name.</param>
        public ForeignKeyAttribute(string referenceTable, string referenceColumn, string localColumn)
        {
            ReferenceTable = referenceTable ?? throw new ArgumentNullException(nameof(referenceTable));
            ReferenceColumn = referenceColumn ?? throw new ArgumentNullException(nameof(referenceColumn));
            LocalColumn = localColumn ?? throw new ArgumentNullException(nameof(localColumn));
        }

        /// <summary>
        /// Gets the reference table name.
        /// </summary>
        public string ReferenceTable { get; }

        /// <summary>
        /// Gets the reference column name.
        /// </summary>
        public string ReferenceColumn { get; }

        /// <summary>
        /// Gets the local column name.
        /// </summary>
        public string LocalColumn { get; }
    }

    #endregion

    #region ORM Methods

    /// <summary>
    /// Selects data from the table and maps it to objects of type T.
    /// </summary>
    /// <typeparam name="T">The type to map the data to.</typeparam>
    /// <returns>An enumerable of objects of type T.</returns>
    public IEnumerable<T> SelectTo<T>() where T : new() => SelectTo<T>(this);

    /// <summary>
    /// Selects data from the specified table and maps it to objects of type T.
    /// </summary>
    /// <typeparam name="T">The type to map the data to.</typeparam>
    /// <param name="table">The table to select from.</param>
    /// <returns>An enumerable of objects of type T.</returns>
    public static IEnumerable<T> SelectTo<T>(Table table) where T : new()
    {
        if (table._connection == null || table.Database == null)
            yield break;

        var type = typeof(T);
        var members = GetMappedMembers(type);
        var columnNames = members.Where(m => !m.IsForeignKey).Select(m => m.ColumnName).ToArray();

        foreach (var row in table.Select(columnNames))
        {
            var item = new T();
            MapRowToItem(row, item, members, table);
            yield return item;
        }
    }

    /// <summary>
    /// Asynchronously selects data from the table and maps it to objects of type T.
    /// </summary>
    /// <typeparam name="T">The type to map the data to.</typeparam>
    /// <param name="token">The cancellation token.</param>
    /// <returns>An async enumerable of objects of type T.</returns>
    public async IAsyncEnumerable<T> SelectToAsync<T>(
        [EnumeratorCancellation] CancellationToken token = default) where T : new()
    {
        await foreach (var k in SelectToAsync<T>(this, token).ConfigureAwait(false))
            yield return k;
    }

    /// <summary>
    /// Asynchronously selects data from the specified table and maps it to objects of type T.
    /// </summary>
    /// <typeparam name="T">The type to map the data to.</typeparam>
    /// <param name="table">The table to select from.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>An async enumerable of objects of type T.</returns>
    public static async IAsyncEnumerable<T> SelectToAsync<T>(Table table,
        [EnumeratorCancellation] CancellationToken token = default) where T : new()
    {
        if (table._connection == null || table.Database == null)
            yield break;

        var type = typeof(T);
        var members = GetMappedMembers(type);
        var columnNames = members.Where(m => !m.IsForeignKey).Select(m => m.ColumnName).ToArray();

        await foreach (var row in table.SelectAsync(columnNames, "", token).ConfigureAwait(false))
        {
            if (token.IsCancellationRequested)
                yield break;

            var item = new T();
            MapRowToItem(row, item, members, table);
            yield return item;
        }
    }

    /// <summary>
    /// Gets the mapped members for the specified type.
    /// </summary>
    /// <param name="type">The type to get mapped members for.</param>
    /// <returns>An enumerable of mapped members.</returns>
    private static IEnumerable<MappedMember> GetMappedMembers(Type type)
    {
        var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
        var members = type.GetMembers(bindingFlags)
            .Where(m => (m.MemberType == MemberTypes.Property || m.MemberType == MemberTypes.Field)
                        && !Attribute.IsDefined(m, typeof(ColumnIgnoreAttribute)));

        foreach (var member in members)
        {
            // Skip if neither property nor field
            if (member is not PropertyInfo && member is not FieldInfo)
                continue;

            var columnNameAttr = member.GetCustomAttribute<ColumnNameAttribute>();
            var columnName = columnNameAttr?.Name ?? member.Name;

            // Check for ForeignKeyAttribute
            var foreignKeyAttr = member.GetCustomAttribute<ForeignKeyAttribute>();
            var isForeignKey = foreignKeyAttr != null;

            yield return new MappedMember
            {
                Member = member,
                ColumnName = columnName,
                IsForeignKey = isForeignKey,
                ForeignKeyAttr = foreignKeyAttr
            };
        }
    }

    /// <summary>
    /// Maps a row of data to an object.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="row">The row of data.</param>
    /// <param name="item">The object to map the data to.</param>
    /// <param name="members">The mapped members.</param>
    /// <param name="table">The table.</param>
    private static void MapRowToItem<T>(IReadOnlyDictionary<string, object?> row, T item,
        IEnumerable<MappedMember> members, Table table)
    {
        // Create a case-insensitive dictionary for the row data
        var rowDict = new Dictionary<string, object?>(row, StringComparer.OrdinalIgnoreCase);

        foreach (var memberInfo in members)
        {
            if (memberInfo.IsForeignKey && memberInfo.ForeignKeyAttr != null)
            {
                // Handle foreign key mapping
                var localColumn = memberInfo.ForeignKeyAttr.LocalColumn;
                if (rowDict.TryGetValue(localColumn, out var foreignKeyValue) && foreignKeyValue != null)
                {
                    try
                    {
                        // Get the member type
                        var memberType = GetMemberType(memberInfo.Member);

                        // Create an instance of the referenced table
                        var foreignTable = new Table(table.Database!, memberInfo.ForeignKeyAttr.ReferenceTable);

                        // Build the WHERE clause to select the referenced item
                        string whereClause =
                            $"WHERE `{memberInfo.ForeignKeyAttr.ReferenceColumn}` = {FormatValue(foreignKeyValue)} LIMIT 1";

                        // Use reflection to call SelectSingle<T> on the foreign table with the WHERE clause
                        var selectMethod = typeof(Table)
                            .GetMethod("SelectSingle", BindingFlags.NonPublic | BindingFlags.Static)
                            ?.MakeGenericMethod(memberType);

                        if (selectMethod != null)
                        {
                            var foreignItem = selectMethod.Invoke(null, new object[] { foreignTable, whereClause });
                            SetMemberValue(memberInfo.Member, item, foreignItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception appropriately
                        System.Diagnostics.Debug.WriteLine($"Error resolving foreign key: {ex.Message}");
                    }
                }
            }
            else
            {
                // Map the value to the property/field
                if (rowDict.TryGetValue(memberInfo.ColumnName, out var value))
                {
                    try
                    {
                        SetMemberValue(memberInfo.Member, item, value);
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception appropriately
                        System.Diagnostics.Debug.WriteLine($"Error setting member value: {ex.Message}");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Formats a value for use in a SQL query.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>The formatted value.</returns>
    private static string FormatValue(object value)
    {
        return TranslateCSharpTypeToMysqlValue(value);
    }

    /// <summary>
    /// Gets the type of a member.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <returns>The type of the member.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the member is not a property or field.</exception>
    private static Type GetMemberType(MemberInfo member)
    {
        return member switch
        {
            PropertyInfo pi => pi.PropertyType,
            FieldInfo fi => fi.FieldType,
            _ => throw new InvalidOperationException("Member is not a property or field.")
        };
    }

    /// <summary>
    /// Sets the value of a member.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <param name="obj">The object.</param>
    /// <param name="value">The value.</param>
    private static void SetMemberValue(MemberInfo member, object obj, object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            switch (member)
            {
                case PropertyInfo pi:
                {
                    if (pi.CanWrite)
                        pi.SetValue(obj, null);
                    break;
                }
                case FieldInfo fi:
                    fi.SetValue(obj, null);
                    break;
            }

            return;
        }

        switch (member)
        {
            case PropertyInfo { CanWrite: false }:
                return;
            case PropertyInfo pi:
            {
                var targetType = Nullable.GetUnderlyingType(pi.PropertyType) ?? pi.PropertyType;
                var convertedValue = ChangeType(value, targetType);
                pi.SetValue(obj, convertedValue);
                break;
            }
            case FieldInfo fi:
            {
                var targetType = Nullable.GetUnderlyingType(fi.FieldType) ?? fi.FieldType;
                var convertedValue = ChangeType(value, targetType);
                fi.SetValue(obj, convertedValue);
                break;
            }
        }
    }

    /// <summary>
    /// Changes the type of a value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="conversionType">The conversion type.</param>
    /// <returns>The converted value.</returns>
    private static object? ChangeType(object? value, Type conversionType)
    {
        if (value == null || value == DBNull.Value)
            return null;

        // Handle enum conversions
        if (conversionType.IsEnum)
        {
            // If the value is a string, parse it as an enum value
            if (value is string strValue)
                return Enum.Parse(conversionType, strValue, ignoreCase: true);

            // If the value is numeric, convert it to the enum's underlying type and then to the enum
            var underlyingType = Enum.GetUnderlyingType(conversionType);
            var convertedValue = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
            return Enum.ToObject(conversionType, convertedValue);
        }

        // Handle Guid conversions
        if (conversionType == typeof(Guid) && value is string guidStr)
            return Guid.Parse(guidStr);

        // Handle TimeSpan conversions
        if (conversionType == typeof(TimeSpan) && value is string timeStr)
            return TimeSpan.Parse(timeStr);

        // Handle standard conversions
        return Convert.ChangeType(value, conversionType, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Selects a single item from the table.
    /// </summary>
    /// <typeparam name="T">The type to map the data to.</typeparam>
    /// <param name="table">The table.</param>
    /// <param name="whereClause">The WHERE clause.</param>
    /// <returns>The selected item or default if not found.</returns>
    private static T? SelectSingle<T>(Table table, string whereClause) where T : new()
    {
        if (table._connection == null || table.Database == null)
            return default;

        var type = typeof(T);
        var members = GetMappedMembers(type);
        var columnNames = members.Where(m => !m.IsForeignKey).Select(m => m.ColumnName).ToArray();

        var rows = table.Select(columnNames, whereClause);

        var row = rows.FirstOrDefault();
        if (row == null)
            return default;

        var item = new T();
        MapRowToItem(row, item, members, table);
        return item;
    }

    /// <summary>
    /// Represents a mapped member.
    /// </summary>
    private class MappedMember
    {
        /// <summary>
        /// Gets or sets the member.
        /// </summary>
        public required MemberInfo Member { get; set; }

        /// <summary>
        /// Gets or sets the column name.
        /// </summary>
        public required string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the member is a foreign key.
        /// </summary>
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// Gets or sets the foreign key attribute.
        /// </summary>
        public ForeignKeyAttribute? ForeignKeyAttr { get; set; }
    }

    #endregion

    #region Last Insert ID

    /// <summary>
    /// Gets the last inserted ID.
    /// </summary>
    /// <param name="name">The name of the ID column.</param>
    /// <returns>The last inserted ID, or -1 if not found.</returns>
    public int Last_Insert_ID(string name = "id") => Last_Insert_ID(this, name);

    /// <summary>
    /// Gets the last inserted ID from the specified table.
    /// </summary>
    /// <param name="tb">The table.</param>
    /// <param name="name">The name of the ID column.</param>
    /// <returns>The last inserted ID, or -1 if not found.</returns>
    public static int Last_Insert_ID(Table tb, string name = "id")
    {
        if (tb._connection == null || tb.Database == null)
            return -1;

        return int.TryParse(
            Select(tb, new[] { name }, $"ORDER BY {name} DESC LIMIT 1;").FirstOrDefault()
                ?.FirstOrDefault(e => e.Key == name).Value?.ToString(), out var target)
            ? target
            : -1;
    }

    /// <summary>
    /// Asynchronously gets the last inserted ID.
    /// </summary>
    /// <param name="name">The name of the ID column.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>The last inserted ID, or -1 if not found.</returns>
    public async Task<int> Last_Insert_IDAsync(string name = "id", CancellationToken token = default) =>
        await Last_Insert_IDAsync(this, name, token);

    /// <summary>
    /// Asynchronously gets the last inserted ID from the specified table.
    /// </summary>
    /// <param name="tb">The table.</param>
    /// <param name="name">The name of the ID column.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>The last inserted ID, or -1 if not found.</returns>
    public static async Task<int> Last_Insert_IDAsync(Table tb, string name = "id", CancellationToken token = default)
    {
        if (tb._connection == null || tb.Database == null)
            return -1;

        IReadOnlyDictionary<string, object?>? data = null;

        await foreach (var id in SelectAsync(tb, new[] { name }, $"ORDER BY {name} DESC LIMIT 1;", token)
                           .ConfigureAwait(false))
        {
            data = id;
            break;
        }

        if (data == null)
            return -1;

        return int.TryParse(
            data.FirstOrDefault(e => e.Key == name).Value?.ToString(), out var target)
            ? target
            : -1;
    }

    #endregion

    #region Base SELECT Methods

    /// <summary>
    /// Selects data from the table and returns it as a list of dictionaries.
    /// </summary>
    /// <param name="columns">The columns to select.</param>
    /// <param name="addition">Additional SQL to append to the query.</param>
    /// <returns>The selected data as a list of dictionaries.</returns>
    public IList<IReadOnlyDictionary<string, object?>> SelectDirect(string[]? columns = null, string addition = "")
    {
        var list = new List<IReadOnlyDictionary<string, object?>>();

        foreach (var d in Select(columns, addition))
            list.Add(new Dictionary<string, object?>(d));

        return list;
    }

    /// <summary>
    /// Selects data from the table.
    /// </summary>
    /// <param name="columns">The columns to select.</param>
    /// <param name="addition">Additional SQL to append to the query.</param>
    /// <returns>The selected data as an enumerable of dictionaries.</returns>
    public IEnumerable<IReadOnlyDictionary<string, object?>> Select(string[]? columns = null, string addition = "") =>
        Select(this, columns, addition);

    /// <summary>
    /// Selects data from the specified table.
    /// </summary>
    /// <param name="tb">The table.</param>
    /// <param name="columns">The columns to select.</param>
    /// <param name="addition">Additional SQL to append to the query.</param>
    /// <returns>The selected data as an enumerable of dictionaries.</returns>
    public static IEnumerable<IReadOnlyDictionary<string, object?>> Select(Table tb, string[]? columns = null,
        string addition = "")
    {
        if (tb._connection == null || tb.Database == null)
            return new List<IReadOnlyDictionary<string, object?>>();

        var columnClause = BuildColumnClause(columns);
        var cmd = $"SELECT {columnClause} FROM `{tb.Database.Name}`.`{tb.Name}` {addition}";

        return tb._connection.Select(cmd);
    }

    /// <summary>
    /// Asynchronously selects data from the table.
    /// </summary>
    /// <param name="columns">The columns to select.</param>
    /// <param name="addition">Additional SQL to append to the query.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>The selected data as an async enumerable of dictionaries.</returns>
    public async IAsyncEnumerable<IReadOnlyDictionary<string, object?>> SelectAsync(string[]? columns = null,
        string addition = "", [EnumeratorCancellation] CancellationToken token = default)
    {
        await foreach (var v in SelectAsync(this, columns, addition, token).ConfigureAwait(false))
            yield return v;
    }

    /// <summary>
    /// Asynchronously selects data from the specified table.
    /// </summary>
    /// <param name="tb">The table.</param>
    /// <param name="columns">The columns to select.</param>
    /// <param name="addition">Additional SQL to append to the query.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>The selected data as an async enumerable of dictionaries.</returns>
    public static async IAsyncEnumerable<IReadOnlyDictionary<string, object?>> SelectAsync(Table tb,
        string[]? columns = null, string addition = "",
        [EnumeratorCancellation] CancellationToken token = default)
    {
        if (tb._connection == null || tb.Database == null)
            yield break;

        var columnClause = BuildColumnClause(columns);
        var cmd = $"SELECT {columnClause} FROM `{tb.Database.Name}`.`{tb.Name}` {addition}";

        await foreach (var v in tb._connection.SelectAsync(cmd, token).ConfigureAwait(false))
        {
            if (token.IsCancellationRequested)
                yield break;

            yield return v;
        }
    }

    /// <summary>
    /// Builds a column clause for a SELECT statement.
    /// </summary>
    /// <param name="columns">The columns to include in the clause.</param>
    /// <returns>The built column clause.</returns>
    private static string BuildColumnClause(string[]? columns)
    {
        if (columns.IsEmptyOrNull())
            return "*";

        // Properly escape and quote column names
        var escapedColumns = columns!.Select(c => $"`{c}`");
        return string.Join(", ", escapedColumns);
    }

    #endregion

    #endregion

    #region COUNT Operations

    /// <summary>
    /// Counts the number of records in the table matching the specified condition.
    /// </summary>
    /// <param name="addition">Additional SQL clauses to append to the query (e.g., WHERE conditions).</param>
    /// <param name="columns">Optional columns to count. If none are provided, "*" is used.</param>
    /// <returns>The count of records as an unsigned integer.</returns>
    public uint Count(string addition, params string[] columns)
    {
        return Count(this, addition, columns);
    }

    /// <summary>
    /// Static method that counts the number of records in the specified table matching the condition.
    /// </summary>
    /// <param name="tb">The table instance to query.</param>
    /// <param name="addition">Additional SQL clauses to append to the query (e.g., WHERE conditions).</param>
    /// <param name="columns">Optional columns to count. If none are provided, "*" is used.</param>
    /// <returns>The count of records as an unsigned integer.</returns>
    public static uint Count(Table tb, string addition, params string[] columns)
    {
        if (tb._connection == null || tb.Database == null)
            return 0;

        var c = columns.IsEmptyOrNull()
            ? "*"
            : string.Join(", ", columns.Select(col => $"`{col}`"));

        var cmd = $"SELECT COUNT({c}) as Count FROM `{tb.Database.Name}`.`{tb.Name}` {addition}";

        var row = tb._connection.Select(cmd).FirstOrDefault();

        if (row == null || !row.TryGetValue("Count", out var countObj) || countObj == null)
            return 0;

        return uint.TryParse(countObj.ToString(), out var count)
            ? count
            : 0;
    }

    /// <summary>
    /// Asynchronously counts the number of records in the table matching the specified condition.
    /// </summary>
    /// <param name="addition">Additional SQL clauses to append to the query (e.g., WHERE conditions).</param>
    /// <param name="columns">Optional columns to count. If none are provided, "*" is used.</param>
    /// <returns>
    /// A task representing the asynchronous operation, with the count of records as an unsigned integer.
    /// </returns>
    public async Task<uint> CountAsync(string addition, params string[] columns)
    {
        return await CountAsync(this, addition, columns);
    }

    /// <summary>
    /// Static asynchronous method that counts the number of records in the specified table matching the condition.
    /// </summary>
    /// <param name="tb">The table instance to query.</param>
    /// <param name="addition">Additional SQL clauses to append to the query (e.g., WHERE conditions).</param>
    /// <param name="columns">Optional columns to count. If none are provided, "*" is used.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>
    /// A task representing the asynchronous operation, with the count of records as an unsigned integer.
    /// </returns>
    public static async Task<uint> CountAsync(Table tb, string addition, string[] columns,
        CancellationToken token = default)
    {
        if (tb._connection == null || tb.Database == null)
            return 0;

        // Use "*" if no columns are provided; otherwise, build a comma-separated list.
        var c = columns.IsEmptyOrNull()
            ? "*"
            : string.Join(", ", columns.Select(col => $"`{col}`"));

        // Build the COUNT query.
        var cmd = $"SELECT COUNT({c}) as Count FROM `{tb.Database.Name}`.`{tb.Name}` {addition}";

        uint count = 0;
        // Execute the asynchronous query and retrieve the first row.
        await foreach (var row in tb._connection.SelectAsync(cmd, token).ConfigureAwait(false))
        {
            if (token.IsCancellationRequested)
                break;

            if (row != null && row.TryGetValue("Count", out var countObj) && countObj != null)
            {
                if (uint.TryParse(countObj.ToString(), out var countValue))
                {
                    count = countValue;
                }
            }

            break; // Only need the first row.
        }

        return count;
    }

    #endregion

    #region CREATE TABLE

    /// <summary>
    /// Creates a new table with the specified columns, foreign keys, and indexes.
    /// </summary>
    /// <param name="columns">The columns to create.</param>
    /// <param name="foreignKeys">The foreign keys to create.</param>
    /// <param name="indexes">The indexes to create.</param>
    /// <returns>The number of affected rows.</returns>
    public int CreateTable(IEnumerable<ColumnDefinition> columns,
        IEnumerable<ForeignKeyDefinition>? foreignKeys = null,
        IEnumerable<IndexDefinition>? indexes = null)
    {
        if (_connection == null || _name == null || Database == null)
            return -1;

        // Build the CREATE TABLE command using the builder.
        var builder = new MySqlTableBuilder(Database.Name, Name);
        builder.WithCharset("utf8mb4_unicode_ci");

        foreach (var col in columns)
            builder.AddColumn(col);

        if (foreignKeys != null)
            foreach (var fk in foreignKeys)
                builder.AddForeignKey(fk);

        if (indexes != null)
            foreach (var idx in indexes)
                builder.AddIndex(idx);

        var command = builder.BuildCreateTableCommand();

        // Execute the command using your connection.
        return _connection.UpdateSelect(command);
    }

    /// <summary>
    /// Creates a new table with the specified columns, foreign keys, and indexes.
    /// </summary>
    /// <param name="table">The table to create.</param>
    /// <param name="columns">The columns to create.</param>
    /// <param name="foreignKeys">The foreign keys to create.</param>
    /// <param name="indexes">The indexes to create.</param>
    /// <returns>The number of affected rows.</returns>
    public static int CreateTable(Table table,
        IEnumerable<ColumnDefinition> columns,
        IEnumerable<ForeignKeyDefinition>? foreignKeys = null,
        IEnumerable<IndexDefinition>? indexes = null)
    {
        return table.CreateTable(columns, foreignKeys, indexes);
    }

    /// <summary>
    /// Asynchronously creates a new table with the specified columns, foreign keys, and indexes.
    /// </summary>
    /// <param name="columns">The columns to create.</param>
    /// <param name="foreignKeys">The foreign keys to create.</param>
    /// <param name="indexes">The indexes to create.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public async Task<int> CreateTableAsync(IEnumerable<ColumnDefinition> columns,
        IEnumerable<ForeignKeyDefinition>? foreignKeys = null,
        IEnumerable<IndexDefinition>? indexes = null, CancellationToken token = default)
    {
        if (_connection == null || _name == null || Database == null)
            return -1;

        var builder = new MySqlTableBuilder(Database.Name, Name);
        builder.WithCharset("utf8mb4_unicode_ci");

        foreach (var col in columns)
            builder.AddColumn(col);

        if (foreignKeys != null)
            foreach (var fk in foreignKeys)
                builder.AddForeignKey(fk);

        if (indexes != null)
            foreach (var idx in indexes)
                builder.AddIndex(idx);

        var command = builder.BuildCreateTableCommand();
        return await _connection.UpdateSelectAsync(command, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously creates a new table with the specified columns, foreign keys, and indexes.
    /// </summary>
    /// <param name="table">The table to create.</param>
    /// <param name="columns">The columns to create.</param>
    /// <param name="foreignKeys">The foreign keys to create.</param>
    /// <param name="indexes">The indexes to create.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public static async Task<int> CreateTableAsync(Table table,
        IEnumerable<ColumnDefinition> columns,
        IEnumerable<ForeignKeyDefinition>? foreignKeys = null,
        IEnumerable<IndexDefinition>? indexes = null, CancellationToken token = default)
    {
        return await table.CreateTableAsync(columns, foreignKeys, indexes, token).ConfigureAwait(false);
    }

    #region Enums

    /// <summary>
    /// Represents MySQL data types.
    /// </summary>
    public enum DataType
    {
        // Numeric Types
        TinyInt,
        SmallInt,
        MediumInt,
        Int,
        Integer,
        BigInt,
        Decimal,
        Numeric,
        Float,
        Double,
        Bit,
        Boolean,
        Bool,

        // Date and Time Types
        Date,
        DateTime,
        Timestamp,
        Time,
        Year,

        // String Types
        Char,
        Varchar,
        Binary,
        Varbinary,
        TinyBlob,
        Blob,
        MediumBlob,
        LongBlob,
        TinyText,
        Text,
        MediumText,
        LongText,
        Enum,
        Set,
        Json,

        // Spatial Types
        Geometry,
        Point,
        Linestring,
        Polygon,
        MultiPoint,
        MultiLinestring,
        MultiPolygon,
        GeometryCollection
    }

    /// <summary>
    /// Represents referential actions for foreign keys.
    /// </summary>
    public enum ReferentialAction
    {
        /// <summary>No action.</summary>
        NoAction,

        /// <summary>Cascade changes to foreign keys.</summary>
        Cascade,

        /// <summary>Set foreign keys to NULL.</summary>
        SetNull,

        /// <summary>Set foreign keys to DEFAULT.</summary>
        SetDefault,
    }

    /// <summary>
    /// Represents index types.
    /// </summary>
    public enum IndexType
    {
        /// <summary>Non-unique index.</summary>
        NonUnique,

        /// <summary>Unique index.</summary>
        Unique,

        /// <summary>Full-text index.</summary>
        FullText,

        /// <summary>Spatial index.</summary>
        Spatial,
    }

    #endregion

    #region ColumnDefinition

    /// <summary>
    /// Represents a column in a MySQL table.
    /// This class uses a fluent API to simplify configuration.
    /// </summary>
    public class ColumnDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition"/> class.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="dataType">The data type of the column.</param>
        /// <exception cref="ArgumentNullException">Thrown when name is null.</exception>
        public ColumnDefinition(string name, DataType dataType)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType;
        }

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the data type of the column.
        /// </summary>
        public DataType DataType { get; }

        /// <summary>
        /// Gets or sets the length of the column.
        /// </summary>
        public int? Length { get; private set; }

        /// <summary>
        /// Gets or sets the precision of the column.
        /// </summary>
        public int? Precision { get; private set; }

        /// <summary>
        /// Gets or sets the scale of the column.
        /// </summary>
        public int? Scale { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the column is nullable.
        /// </summary>
        public bool IsNullable { get; private set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the column is a primary key.
        /// </summary>
        public bool IsPrimaryKey { get; private set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the column is auto-incrementing.
        /// </summary>
        public bool IsAutoIncrement { get; private set; } = false;

        /// <summary>
        /// Gets or sets the default value of the column.
        /// </summary>
        public string? DefaultValue { get; private set; }

        /// <summary>
        /// Gets or sets extra information for the column.
        /// </summary>
        public string? Extra { get; private set; }

        /// <summary>
        /// Gets or sets the comment for the column.
        /// </summary>
        public string? Comment { get; private set; }

        /// <summary>
        /// Gets or sets the enum values for the column.
        /// </summary>
        public List<string>? EnumValues { get; private set; }

        /// <summary>
        /// Gets or sets the set values for the column.
        /// </summary>
        public List<string>? SetValues { get; private set; }

        /// <summary>
        /// Gets or sets the bit length of the column.
        /// </summary>
        public int? BitLength { get; private set; }

        // Fluent setters

        /// <summary>
        /// Sets the length of the column.
        /// </summary>
        /// <param name="length">The length of the column.</param>
        /// <returns>The column definition.</returns>
        public ColumnDefinition WithLength(int length)
        {
            Length = length;
            return this;
        }

        /// <summary>
        /// Sets the precision of the column.
        /// </summary>
        /// <param name="precision">The precision of the column.</param>
        /// <returns>The column definition.</returns>
        public ColumnDefinition WithPrecision(int precision)
        {
            Precision = precision;
            return this;
        }

        /// <summary>
        /// Sets the scale of the column.
        /// </summary>
        /// <param name="scale">The scale of the column.</param>
        /// <returns>The column definition.</returns>
        public ColumnDefinition WithScale(int scale)
        {
            Scale = scale;
            return this;
        }

        /// <summary>
        /// Sets the column as not nullable.
        /// </summary>
        /// <returns>The column definition.</returns>
        public ColumnDefinition NotNullable()
        {
            IsNullable = false;
            return this;
        }

        /// <summary>
        /// Sets the column as a primary key.
        /// </summary>
        /// <returns>The column definition.</returns>
        public ColumnDefinition PrimaryKey()
        {
            IsPrimaryKey = true;
            return this;
        }

        /// <summary>
        /// Sets the column as auto-incrementing.
        /// </summary>
        /// <returns>The column definition.</returns>
        public ColumnDefinition AutoIncrement()
        {
            IsAutoIncrement = true;
            return this;
        }

        /// <summary>
        /// Sets the default value of the column.
        /// </summary>
        /// <param name="defaultValue">The default value of the column.</param>
        /// <returns>The column definition.</returns>
        public ColumnDefinition WithDefault(string defaultValue)
        {
            DefaultValue = defaultValue;
            return this;
        }

        /// <summary>
        /// Sets extra information for the column.
        /// </summary>
        /// <param name="extra">Extra information for the column.</param>
        /// <returns>The column definition.</returns>
        public ColumnDefinition WithExtra(string extra)
        {
            Extra = extra;
            return this;
        }

        /// <summary>
        /// Sets the comment for the column.
        /// </summary>
        /// <param name="comment">The comment for the column.</param>
        /// <returns>The column definition.</returns>
        public ColumnDefinition WithComment(string comment)
        {
            Comment = comment;
            return this;
        }

        /// <summary>
        /// Marks a numeric type as UNSIGNED.
        /// </summary>
        /// <returns>The column definition.</returns>
        public ColumnDefinition Unsigned()
        {
            Extra = "UNSIGNED";
            return this;
        }

        /// <summary>
        /// Sets the enum values for the column.
        /// </summary>
        /// <param name="values">The enum values for the column.</param>
        /// <returns>The column definition.</returns>
        public ColumnDefinition WithEnumValues(params string[] values)
        {
            EnumValues = values.ToList();
            return this;
        }

        /// <summary>
        /// Sets the set values for the column.
        /// </summary>
        /// <param name="values">The set values for the column.</param>
        /// <returns>The column definition.</returns>
        public ColumnDefinition WithSetValues(params string[] values)
        {
            SetValues = values.ToList();
            return this;
        }

        /// <summary>
        /// Sets the bit length of the column.
        /// </summary>
        /// <param name="bitLength">The bit length of the column.</param>
        /// <returns>The column definition.</returns>
        public ColumnDefinition WithBitLength(int bitLength)
        {
            BitLength = bitLength;
            return this;
        }

        /// <summary>
        /// Generates the SQL snippet for this column.
        /// </summary>
        /// <returns>The SQL snippet for this column.</returns>
        /// <exception cref="ArgumentException">Thrown when a required property is missing.</exception>
        /// <exception cref="NotSupportedException">Thrown when the data type is not supported.</exception>
        public string GetSqlDefinition()
        {
            string dataTypeStr = DataType switch
            {
                // Numeric Types
                DataType.TinyInt => Length.HasValue ? $"TINYINT({Length})" : "TINYINT",
                DataType.SmallInt => Length.HasValue ? $"SMALLINT({Length})" : "SMALLINT",
                DataType.MediumInt => Length.HasValue ? $"MEDIUMINT({Length})" : "MEDIUMINT",
                DataType.Int => Length.HasValue ? $"INT({Length})" : "INT",
                DataType.Integer => Length.HasValue ? $"INTEGER({Length})" : "INTEGER",
                DataType.BigInt => Length.HasValue ? $"BIGINT({Length})" : "BIGINT",
                DataType.Decimal => (Precision.HasValue && Scale.HasValue)
                    ? $"DECIMAL({Precision},{Scale})"
                    : "DECIMAL",
                DataType.Numeric => (Precision.HasValue && Scale.HasValue)
                    ? $"NUMERIC({Precision},{Scale})"
                    : "NUMERIC",
                DataType.Float => (Precision.HasValue && Scale.HasValue)
                    ? $"FLOAT({Precision},{Scale})"
                    : "FLOAT",
                DataType.Double => (Precision.HasValue && Scale.HasValue)
                    ? $"DOUBLE({Precision},{Scale})"
                    : "DOUBLE",
                DataType.Bit => BitLength.HasValue ? $"BIT({BitLength})" : "BIT",
                DataType.Boolean => "BOOLEAN",
                DataType.Bool => "BOOL",

                // Date and Time Types
                DataType.Date => "DATE",
                DataType.DateTime => "DATETIME",
                DataType.Timestamp => "TIMESTAMP",
                DataType.Time => "TIME",
                DataType.Year => "YEAR",

                // String Types
                DataType.Char => Length.HasValue
                    ? $"CHAR({Length})"
                    : throw new ArgumentException($"Length is required for CHAR type in column '{Name}'."),
                DataType.Varchar => Length.HasValue ? $"VARCHAR({Length})" : "VARCHAR(255)",
                DataType.Binary => Length.HasValue
                    ? $"BINARY({Length})"
                    : throw new ArgumentException($"Length is required for BINARY type in column '{Name}'."),
                DataType.Varbinary => Length.HasValue ? $"VARBINARY({Length})" : "VARBINARY(255)",
                DataType.TinyBlob => "TINYBLOB",
                DataType.Blob => "BLOB",
                DataType.MediumBlob => "MEDIUMBLOB",
                DataType.LongBlob => "LONGBLOB",
                DataType.TinyText => "TINYTEXT",
                DataType.Text => "TEXT",
                DataType.MediumText => "MEDIUMTEXT",
                DataType.LongText => "LONGTEXT",
                DataType.Enum => EnumValues != null && EnumValues.Any()
                    ? $"ENUM({string.Join(", ", EnumValues.Select(ev => $"'{MySqlHelper.EscapeString(ev)}'"))})"
                    : throw new ArgumentException($"Enum values must be specified for ENUM type in column '{Name}'."),
                DataType.Set => SetValues != null && SetValues.Any()
                    ? $"SET({string.Join(", ", SetValues.Select(sv => $"'{MySqlHelper.EscapeString(sv)}'"))})"
                    : throw new ArgumentException($"Set values must be specified for SET type in column '{Name}'."),
                DataType.Json => "JSON",

                // Spatial Types
                DataType.Geometry => "GEOMETRY",
                DataType.Point => "POINT",
                DataType.Linestring => "LINESTRING",
                DataType.Polygon => "POLYGON",
                DataType.MultiPoint => "MULTIPOINT",
                DataType.MultiLinestring => "MULTILINESTRING",
                DataType.MultiPolygon => "MULTIPOLYGON",
                DataType.GeometryCollection => "GEOMETRYCOLLECTION",
                _ => throw new NotSupportedException($"Data type {DataType} is not supported."),
            };

            var sb = new StringBuilder();
            sb.Append($"`{Name}` {dataTypeStr}");

            if (!IsNullable)
                sb.Append(" NOT NULL");

            if (IsAutoIncrement)
                sb.Append(" AUTO_INCREMENT");

            if (DefaultValue != null)
                sb.Append($" DEFAULT {DefaultValue}");

            if (!string.IsNullOrEmpty(Extra))
                sb.Append($" {Extra}");

            if (!string.IsNullOrEmpty(Comment))
                sb.Append($" COMMENT '{MySqlHelper.EscapeString(Comment)}'");

            return sb.ToString();
        }
    }

    #endregion

    #region ForeignKeyDefinition

    /// <summary>
    /// Represents a foreign key constraint in a MySQL table.
    /// </summary>
    public class ForeignKeyDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyDefinition"/> class.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <param name="referenceTable">The reference table.</param>
        /// <param name="referenceColumn">The reference column.</param>
        /// <param name="onDelete">The on delete action.</param>
        /// <param name="onUpdate">The on update action.</param>
        /// <exception cref="ArgumentNullException">Thrown when columnName, referenceTable, or referenceColumn is null.</exception>
        public ForeignKeyDefinition(string columnName, string referenceTable, string referenceColumn,
            ReferentialAction? onDelete = null, ReferentialAction? onUpdate = null)
        {
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            ReferenceTable = referenceTable ?? throw new ArgumentNullException(nameof(referenceTable));
            ReferenceColumn = referenceColumn ?? throw new ArgumentNullException(nameof(referenceColumn));
            OnDelete = onDelete;
            OnUpdate = onUpdate;
        }

        /// <summary>
        /// Gets the column name.
        /// </summary>
        public string ColumnName { get; }

        /// <summary>
        /// Gets the reference table.
        /// </summary>
        public string ReferenceTable { get; }

        /// <summary>
        /// Gets the reference column.
        /// </summary>
        public string ReferenceColumn { get; }

        /// <summary>
        /// Gets the on delete action.
        /// </summary>
        public ReferentialAction? OnDelete { get; }

        /// <summary>
        /// Gets the on update action.
        /// </summary>
        public ReferentialAction? OnUpdate { get; }

        /// <summary>
        /// Gets the SQL definition for this foreign key.
        /// </summary>
        /// <param name="dbName">The database name.</param>
        /// <returns>The SQL definition for this foreign key.</returns>
        public string GetSqlDefinition(string dbName)
        {
            var sb = new StringBuilder();
            sb.Append($"FOREIGN KEY (`{ColumnName}`) REFERENCES `{dbName}`.`{ReferenceTable}`(`{ReferenceColumn}`)");

            if (OnDelete.HasValue)
            {
                var actionString = OnDelete.Value == ReferentialAction.NoAction
                    ? "RESTRICT"
                    : OnDelete.Value.ToString().ToUpper();
                sb.Append($" ON DELETE {actionString}");
            }

            if (OnUpdate.HasValue)
            {
                var actionString = OnUpdate.Value == ReferentialAction.NoAction
                    ? "RESTRICT"
                    : OnUpdate.Value.ToString().ToUpper();
                sb.Append($" ON UPDATE {actionString}");
            }

            return sb.ToString();
        }
    }

    #endregion


    #region IndexDefinition

    /// <summary>
    /// Represents an index on one or more columns.
    /// </summary>
    public class IndexDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexDefinition"/> class.
        /// </summary>
        /// <param name="name">The name of the index.</param>
        /// <param name="indexType">The type of the index.</param>
        /// <param name="columns">The columns included in the index.</param>
        /// <exception cref="ArgumentNullException">Thrown when name or columns is null.</exception>
        /// <exception cref="ArgumentException">Thrown when columns is empty.</exception>
        public IndexDefinition(string name, IndexType indexType, params string[] columns)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IndexType = indexType;

            if (columns == null || columns.Length == 0)
                throw new ArgumentException("At least one column must be specified for an index.", nameof(columns));

            Columns = columns.ToList();
        }

        /// <summary>
        /// Gets the name of the index.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the index.
        /// </summary>
        public IndexType IndexType { get; }

        /// <summary>
        /// Gets the columns included in the index.
        /// </summary>
        public List<string> Columns { get; }

        /// <summary>
        /// Gets the SQL definition for this index.
        /// </summary>
        /// <returns>The SQL definition for this index.</returns>
        public string GetSqlDefinition()
        {
            string indexTypeStr = IndexType switch
            {
                IndexType.Unique => "UNIQUE INDEX",
                IndexType.FullText => "FULLTEXT INDEX",
                IndexType.Spatial => "SPATIAL INDEX",
                IndexType.NonUnique => "INDEX",
                _ => "INDEX"
            };

            return $"{indexTypeStr} `{Name}` ({string.Join(", ", Columns.Select(c => $"`{c}`"))})";
        }
    }

    #endregion

    #region MySqlTableBuilder

    /// <summary>
    /// Provides a fluent API for building MySQL CREATE TABLE commands.
    /// </summary>
    public class MySqlTableBuilder
    {
        private readonly List<ColumnDefinition> _columns = new();
        private readonly List<ForeignKeyDefinition> _foreignKeys = new();
        private readonly List<IndexDefinition> _indexes = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlTableBuilder"/> class.
        /// </summary>
        /// <param name="databaseName">The database name.</param>
        /// <param name="tableName">The table name.</param>
        /// <exception cref="ArgumentNullException">Thrown when databaseName or tableName is null.</exception>
        public MySqlTableBuilder(string databaseName, string tableName)
        {
            DatabaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
        }

        /// <summary>
        /// Gets the database name.
        /// </summary>
        public string DatabaseName { get; }

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public string TableName { get; }

        /// <summary>
        /// Gets or sets the character set.
        /// </summary>
        public string? Charset { get; private set; }

        /// <summary>
        /// Sets the character set for the table.
        /// </summary>
        /// <param name="charset">The character set.</param>
        /// <returns>The table builder.</returns>
        public MySqlTableBuilder WithCharset(string charset)
        {
            Charset = charset;
            return this;
        }

        /// <summary>
        /// Adds a column to the table.
        /// </summary>
        /// <param name="column">The column to add.</param>
        /// <returns>The table builder.</returns>
        /// <exception cref="ArgumentNullException">Thrown when column is null.</exception>
        public MySqlTableBuilder AddColumn(ColumnDefinition column)
        {
            _columns.Add(column ?? throw new ArgumentNullException(nameof(column)));
            return this;
        }

        /// <summary>
        /// Adds a foreign key to the table.
        /// </summary>
        /// <param name="fk">The foreign key to add.</param>
        /// <returns>The table builder.</returns>
        /// <exception cref="ArgumentNullException">Thrown when fk is null.</exception>
        public MySqlTableBuilder AddForeignKey(ForeignKeyDefinition fk)
        {
            _foreignKeys.Add(fk ?? throw new ArgumentNullException(nameof(fk)));
            return this;
        }

        /// <summary>
        /// Adds an index to the table.
        /// </summary>
        /// <param name="index">The index to add.</param>
        /// <returns>The table builder.</returns>
        /// <exception cref="ArgumentNullException">Thrown when index is null.</exception>
        public MySqlTableBuilder AddIndex(IndexDefinition index)
        {
            _indexes.Add(index ?? throw new ArgumentNullException(nameof(index)));
            return this;
        }

        /// <summary>
        /// Builds and returns the full CREATE TABLE SQL command.
        /// </summary>
        /// <returns>The CREATE TABLE SQL command.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no columns are defined.</exception>
        public string BuildCreateTableCommand()
        {
            if (_columns.Count == 0)
                throw new InvalidOperationException("At least one column must be defined.");

            var columnDefinitions = new List<string>();

            // Column definitions
            foreach (var column in _columns)
            {
                columnDefinitions.Add(column.GetSqlDefinition());
            }

            // Primary Key definition (if declared via fluent API)
            var primaryKeys = _columns.Where(c => c.IsPrimaryKey).Select(c => $"`{c.Name}`").ToList();
            if (primaryKeys.Any())
            {
                columnDefinitions.Add($"PRIMARY KEY ({string.Join(", ", primaryKeys)})");
            }

            // Foreign Key constraints
            foreach (var fk in _foreignKeys)
            {
                columnDefinitions.Add(fk.GetSqlDefinition(DatabaseName));
            }

            var sb = new StringBuilder();
            sb.Append($"CREATE TABLE `{DatabaseName}`.`{TableName}` (\n  ");
            sb.Append(string.Join(",\n  ", columnDefinitions));
            sb.Append("\n) ENGINE=InnoDB");

            if (!string.IsNullOrEmpty(Charset))
            {
                sb.Append($" DEFAULT CHARSET={Charset}");
            }

            sb.Append(";");

            // Append indexes (each as a separate statement)
            foreach (var index in _indexes)
            {
                sb.Append($"\nCREATE {index.GetSqlDefinition()} ON `{DatabaseName}`.`{TableName}`;");
            }

            return sb.ToString();
        }
    }

    #endregion

    #endregion

    #region Table Maintenance

    #region ANALYZE

    /// <summary>
    /// Analyzes the table.
    /// </summary>
    /// <returns>The number of affected rows.</returns>
    public int Analyze() => Analyze(this);

    /// <summary>
    /// Analyzes the specified table.
    /// </summary>
    /// <param name="table">The table to analyze.</param>
    /// <returns>The number of affected rows.</returns>
    public static int Analyze(Table table)
    {
        if (table._connection == null || table.Database == null)
            return -1;

        var con = (Connection)table;

        return con.UpdateSelect($"ANALYZE TABLE `{table.Database.Name}`.`{table.Name}`");
    }

    /// <summary>
    /// Asynchronously analyzes the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public async Task<int> AnalyzeAsync(CancellationToken token = default) =>
        await AnalyzeAsync(this, token).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously analyzes the specified table.
    /// </summary>
    /// <param name="table">The table to analyze.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public static async Task<int> AnalyzeAsync(Table table, CancellationToken token = default)
    {
        if (table._connection == null || table.Database == null)
            return -1;

        var con = (Connection)table;

        return await con.UpdateSelectAsync($"ANALYZE TABLE `{table.Database.Name}`.`{table.Name}`", token)
            .ConfigureAwait(false);
    }

    #endregion

    #region CHECK

    /// <summary>
    /// Checks the table for errors.
    /// </summary>
    /// <returns>The number of affected rows.</returns>
    public int Check() => Check(this);

    /// <summary>
    /// Checks the specified table for errors.
    /// </summary>
    /// <param name="table">The table to check.</param>
    /// <returns>The number of affected rows.</returns>
    public static int Check(Table table)
    {
        if (table._connection == null || table.Database == null)
            return -1;

        var con = (Connection)table;

        return con.UpdateSelect($"CHECK TABLE `{table.Database.Name}`.`{table.Name}`");
    }

    /// <summary>
    /// Asynchronously checks the table for errors.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public async Task<int> CheckAsync(CancellationToken token = default) =>
        await CheckAsync(this, token).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously checks the specified table for errors.
    /// </summary>
    /// <param name="table">The table to check.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public static async Task<int> CheckAsync(Table table, CancellationToken token = default)
    {
        if (table._connection == null || table.Database == null)
            return -1;

        var con = (Connection)table;

        return await con.UpdateSelectAsync($"CHECK TABLE `{table.Database.Name}`.`{table.Name}`", token)
            .ConfigureAwait(false);
    }

    #endregion

    #region OPTIMIZE

    /// <summary>
    /// Optimizes the table.
    /// </summary>
    /// <returns>The number of affected rows.</returns>
    public int Optimize() => Optimize(this);

    /// <summary>
    /// Optimizes the specified table.
    /// </summary>
    /// <param name="table">The table to optimize.</param>
    /// <returns>The number of affected rows.</returns>
    public static int Optimize(Table table)
    {
        if (table._connection == null || table.Database == null)
            return -1;

        var con = (Connection)table;

        return con.UpdateSelect($"OPTIMIZE TABLE `{table.Database.Name}`.`{table.Name}`");
    }

    /// <summary>
    /// Asynchronously optimizes the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public async Task<int> OptimizeAsync(CancellationToken token = default) =>
        await OptimizeAsync(this, token).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously optimizes the specified table.
    /// </summary>
    /// <param name="table">The table to optimize.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public static async Task<int> OptimizeAsync(Table table, CancellationToken token = default)
    {
        if (table._connection == null || table.Database == null)
            return -1;

        var con = (Connection)table;

        return await con.UpdateSelectAsync($"OPTIMIZE TABLE `{table.Database.Name}`.`{table.Name}`", token)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Optimizes the table with the specified optimization flags.
    /// </summary>
    public int Optimise(OptimisationFlags flags)
    {
        Optimise(this, flags);
        return 0;
    }

    /// <summary>
    /// Asynchronously optimizes the table with the specified optimization flags.
    /// </summary>
    public async Task OptimiseAsync(OptimisationFlags flags, CancellationToken token = default)
    {
        await Task.Run(() => Optimise(this, flags), token);
    }

    #endregion

    #region FLUSH

    /// <summary>
    /// Flushes the table.
    /// </summary>
    /// <returns>The number of affected rows.</returns>
    public int Flush() => Flush(this);

    /// <summary>
    /// Flushes the specified table.
    /// </summary>
    /// <param name="table">The table to flush.</param>
    /// <returns>The number of affected rows.</returns>
    public static int Flush(Table table)
    {
        if (table._connection == null || table.Database == null)
            return -1;

        var con = (Connection)table;

        return con.UpdateSelect($"FLUSH TABLE `{table.Database.Name}`.`{table.Name}`");
    }

    /// <summary>
    /// Asynchronously flushes the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public async Task<int> FlushAsync(CancellationToken token = default) =>
        await FlushAsync(this, token).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously flushes the specified table.
    /// </summary>
    /// <param name="table">The table to flush.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public static async Task<int> FlushAsync(Table table, CancellationToken token = default)
    {
        if (table._connection == null || table.Database == null)
            return -1;

        var con = (Connection)table;

        return await con.UpdateSelectAsync($"FLUSH TABLE `{table.Database.Name}`.`{table.Name}`", token)
            .ConfigureAwait(false);
    }

    #endregion

    #region REPAIR

    /// <summary>
    /// Repairs the table.
    /// </summary>
    /// <returns>The number of affected rows.</returns>
    public int Repair() => Repair(this);

    /// <summary>
    /// Repairs the specified table.
    /// </summary>
    /// <param name="table">The table to repair.</param>
    /// <returns>The number of affected rows.</returns>
    public static int Repair(Table table)
    {
        if (table._connection == null || table.Database == null)
            return -1;

        var con = (Connection)table;

        return con.UpdateSelect($"REPAIR TABLE `{table.Database.Name}`.`{table.Name}`");
    }

    /// <summary>
    /// Asynchronously repairs the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public async Task<int> RepairAsync(CancellationToken token = default) =>
        await RepairAsync(this, token).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously repairs the specified table.
    /// </summary>
    /// <param name="table">The table to repair.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the number of affected rows.</returns>
    public static async Task<int> RepairAsync(Table table, CancellationToken token = default)
    {
        if (table._connection == null || table.Database == null)
            return -1;

        var con = (Connection)table;

        return await con.UpdateSelectAsync($"REPAIR TABLE `{table.Database.Name}`.`{table.Name}`", token)
            .ConfigureAwait(false);
    }

    #endregion

    #endregion

    #region ALTER Operations

    /// <summary>
    /// Generates the command for adding a new column to the table.
    /// </summary>
    /// <param name="db">The database.</param>
    /// <param name="tb">The table.</param>
    /// <param name="columnName">The name of the column to add.</param>
    /// <param name="columnType">The data type of the new column.</param>
    /// <returns>The generated ALTER TABLE ADD COLUMN command string.</returns>
    private static string GenerateCommandForAddColumn(Database db, Table tb, string columnName, string columnType)
    {
        return $"ALTER TABLE `{db.Name}`.`{tb.Name}` ADD COLUMN `{columnName}` {columnType};";
    }

    /// <summary>
    /// Asynchronously adds a new column to the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <param name="columnName">The name of the column to add.</param>
    /// <param name="columnType">The data type of the new column.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> AddColumnAsync(CancellationToken token, string columnName, string columnType)
    {
        if (_connection == null || _name == null || Database == null)
            return -1;

        var command = GenerateCommandForAddColumn(Database, this, columnName, columnType);
        return await _connection.UpdateSelectAsync(command, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Adds a new column to the table.
    /// </summary>
    /// <param name="columnName">The name of the column to add.</param>
    /// <param name="columnType">The data type of the new column.</param>
    /// <returns>The number of affected rows.</returns>
    public int AddColumn(string columnName, string columnType)
    {
        if (_connection == null || _name == null || Database == null)
            return -1;

        var command = GenerateCommandForAddColumn(Database, this, columnName, columnType);
        return _connection.UpdateSelect(command);
    }

    /// <summary>
    /// Generates the command for dropping a column from the table.
    /// </summary>
    /// <param name="db">The database.</param>
    /// <param name="tb">The table.</param>
    /// <param name="columnName">The name of the column to drop.</param>
    /// <returns>The generated ALTER TABLE DROP COLUMN command string.</returns>
    private static string GenerateCommandForDropColumn(Database db, Table tb, string columnName)
    {
        return $"ALTER TABLE `{db.Name}`.`{tb.Name}` DROP COLUMN `{columnName}`;";
    }

    /// <summary>
    /// Asynchronously drops a column from the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <param name="columnName">The name of the column to drop.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> DropColumnAsync(CancellationToken token, string columnName)
    {
        if (_connection == null || _name == null || Database == null)
            return -1;

        var command = GenerateCommandForDropColumn(Database, this, columnName);
        return await _connection.UpdateSelectAsync(command, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Drops a column from the table.
    /// </summary>
    /// <param name="columnName">The name of the column to drop.</param>
    /// <returns>The number of affected rows.</returns>
    public int DropColumn(string columnName)
    {
        if (_connection == null || _name == null || Database == null)
            return -1;

        var command = GenerateCommandForDropColumn(Database, this, columnName);
        return _connection.UpdateSelect(command);
    }

    /// <summary>
    /// Generates the command for modifying a column in the table.
    /// </summary>
    /// <param name="db">The database.</param>
    /// <param name="tb">The table.</param>
    /// <param name="columnName">The name of the column to modify.</param>
    /// <param name="newColumnType">The new data type of the column.</param>
    /// <returns>The generated ALTER TABLE MODIFY COLUMN command string.</returns>
    private static string GenerateCommandForModifyColumn(Database db, Table tb, string columnName, string newColumnType)
    {
        return $"ALTER TABLE `{db.Name}`.`{tb.Name}` MODIFY COLUMN `{columnName}` {newColumnType};";
    }

    /// <summary>
    /// Asynchronously modifies a column in the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <param name="columnName">The name of the column to modify.</param>
    /// <param name="newColumnType">The new data type of the column.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> ModifyColumnAsync(CancellationToken token, string columnName, string newColumnType)
    {
        if (_connection == null || _name == null || Database == null)
            return -1;

        var command = GenerateCommandForModifyColumn(Database, this, columnName, newColumnType);
        return await _connection.UpdateSelectAsync(command, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Modifies a column in the table.
    /// </summary>
    /// <param name="columnName">The name of the column to modify.</param>
    /// <param name="newColumnType">The new data type of the column.</param>
    /// <returns>The number of affected rows.</returns>
    public int ModifyColumn(string columnName, string newColumnType)
    {
        if (_connection == null || _name == null || Database == null)
            return -1;

        var command = GenerateCommandForModifyColumn(Database, this, columnName, newColumnType);
        return _connection.UpdateSelect(command);
    }

    #endregion

    #region INSERT_INTO

    /// <summary>
    /// Generates the command for inserting data into the table.
    /// </summary>
    /// <param name="db">The database.</param>
    /// <param name="tb">The table.</param>
    /// <param name="data">The data to insert.</param>
    /// <returns>The generated command string.</returns>
    /// <exception cref="ArgumentException">Thrown when data is empty.</exception>
    private static string GenerateCommandForInsertInto(Database db, Table tb,
        Dictionary<string, object?>[] data)
    {
        if (data.Length == 0)
            throw new ArgumentException("Data cannot be empty.", nameof(data));

        // Safely handle column names with proper escaping
        var columns = string.Join(", ", data[0].Keys.Select(k => $"`{k}`"));
        var values = new StringBuilder();

        foreach (var row in data)
        {
            var rowValues = string.Join(", ", row.Values.Select(TranslateCSharpTypeToMysqlValue));
            values.Append($"({rowValues}),");
        }

        // Remove the trailing comma
        if (values.Length > 0)
            values.Length--;

        return $"INSERT INTO `{db.Name}`.`{tb.Name}` ({columns}) VALUES {values}";
    }

    /// <summary>
    /// Asynchronously inserts data into the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <param name="data">The data to insert.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> InsertIntoAsync(CancellationToken token, params Dictionary<string, object?>[] data)
    {
        if (data.Length == 0 || _connection == null || _name == null || Database == null)
            return -1;

        return await _connection.UpdateSelectAsync(GenerateCommandForInsertInto(Database, this, data), token)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Inserts data into the table.
    /// </summary>
    /// <param name="data">The data to insert.</param>
    /// <returns>The number of affected rows.</returns>
    public int InsertInto(params Dictionary<string, object?>[] data)
    {
        if (_connection == null || _name == null || data.Length == 0 || Database == null)
            return -1;

        return _connection.UpdateSelect(GenerateCommandForInsertInto(Database, this, data));
    }

    #endregion

    #region UPDATE_INSERT (Upsert)

    /// <summary>
    /// Translates a C# value to a MySQL value.
    /// </summary>
    /// <param name="v">The value to translate.</param>
    /// <returns>The translated value.</returns>
    private static string TranslateCSharpTypeToMysqlValue(object? v)
    {
        return v switch
        {
            // Handle NULL
            null => "NULL",
            DBNull => "NULL",

            // Handle binary data (BLOB)
            byte[] bytes => $"x'{BitConverter.ToString(bytes).Replace("-", "")}'",
            IEnumerable<byte> bytes => $"x'{BitConverter.ToString(bytes.ToArray()).Replace("-", "")}'",

            // Handle string
            string s => $"'{MySqlHelper.EscapeString(s)}'",

            // Handle bool as 0 or 1
            bool b => b ? "1" : "0",

            // Handle DateTime 
            DateTime dt => $"'{dt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'",

            // Handle DateTimeOffset
            DateTimeOffset dto =>
                $"'{dto.UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'",

            // Handle Guid
            Guid g => $"'{g.ToString()}'",

            // Handle numeric types
            sbyte sb => sb.ToString(CultureInfo.InvariantCulture),
            byte by => by.ToString(CultureInfo.InvariantCulture),
            short sh => sh.ToString(CultureInfo.InvariantCulture),
            ushort ush => ush.ToString(CultureInfo.InvariantCulture),
            int i => i.ToString(CultureInfo.InvariantCulture),
            uint ui => ui.ToString(CultureInfo.InvariantCulture),
            long l => l.ToString(CultureInfo.InvariantCulture),
            ulong ul => ul.ToString(CultureInfo.InvariantCulture),
            float f => f.ToString(CultureInfo.InvariantCulture),
            double d => d.ToString(CultureInfo.InvariantCulture),
            decimal dec => dec.ToString(CultureInfo.InvariantCulture),

            // Handle Enums
            Enum e => $"'{MySqlHelper.EscapeString(e.ToString())}'",

            // Catch-all fallback
            _ => $"'{MySqlHelper.EscapeString(v.ToString() ?? string.Empty)}'"
        };
    }

    /// <summary>
    /// Generates the command for upserting data into the table.
    /// </summary>
    private static string GenerateCommandForUpsert(Database db, Table tb,
        string[] conflictColumns, Dictionary<string, object?>[] data)
    {
        if (data.Length == 0)
            throw new ArgumentException("Data cannot be empty.", nameof(data));

        var columns = string.Join(", ", data[0].Keys.Select(k => $"`{k}`"));
        var values = new StringBuilder();
        var updates = new StringBuilder();

        // Build the VALUES section
        foreach (var row in data)
        {
            var rowValues = string.Join(", ", row.Values.Select(TranslateCSharpTypeToMysqlValue));
            values.Append($"({rowValues}),");
        }

        // Remove the trailing comma
        if (values.Length > 0)
            values.Length--;

        // Build the ON DUPLICATE KEY UPDATE section
        foreach (var column in data[0].Keys)
        {
            // Skip conflict columns in the update part
            if (conflictColumns != null && conflictColumns.Contains(column, StringComparer.OrdinalIgnoreCase))
                continue;

            updates.Append($"`{column}` = VALUES(`{column}`), ");
        }

        // Remove the trailing comma and space
        if (updates.Length >= 2)
            updates.Length -= 2;

        return $"INSERT INTO `{db.Name}`.`{tb.Name}` ({columns}) " +
               $"VALUES {values} " +
               $"ON DUPLICATE KEY UPDATE {updates}";
    }

    /// <summary>
    /// Asynchronously upserts data into the table.
    /// </summary>
    public async Task<int> UpdateInsertAsync(CancellationToken token, string[] conflictColumns,
        params Dictionary<string, object?>[] data)
    {
        if (data.Length == 0 || _connection == null || _name == null || Database == null)
            return -1;

        var command = GenerateCommandForUpsert(Database, this, conflictColumns, data);
        return await _connection.UpdateSelectAsync(command, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Upserts data into the table.
    /// </summary>
    public int UpdateInsert(string[] conflictColumns, params Dictionary<string, object?>[] data)
    {
        if (_connection == null || _name == null || data.Length == 0 || Database == null)
            return -1;

        var command = GenerateCommandForUpsert(Database, this, conflictColumns, data);
        return _connection.UpdateSelect(command);
    }

    /// <summary>
    /// Generates the SQL command for updating existing rows.
    /// </summary>
    /// <param name="db">The database.</param>
    /// <param name="tb">The table.</param>
    /// <param name="data">The data to update.</param>
    /// <param name="whereCondition">The WHERE clause to identify existing rows.</param>
    /// <returns>The generated command string.</returns>
    private static string GenerateUpdateCommand(Database db, Table tb,
        Dictionary<string, object?> data, string whereCondition)
    {
        // Build SET clause with each column=value pair
        var updates = string.Join(", ", data.Select(kv =>
            $"`{kv.Key}` = {TranslateCSharpTypeToMysqlValue(kv.Value)}")
        );

        return $"UPDATE `{db.Name}`.`{tb.Name}` SET {updates} WHERE {whereCondition};";
    }

    /// <summary>
    /// Generates the SQL command for inserting new rows.
    /// </summary>
    /// <param name="db">The database.</param>
    /// <param name="tb">The table.</param>
    /// <param name="data">The data to insert.</param>
    /// <returns>The generated command string.</returns>
    private static string GenerateInsertCommand(Database db, Table tb, Dictionary<string, object?> data)
    {
        // Build column list and values list
        var columns = string.Join(", ", data.Keys.Select(c => $"`{c}`"));
        var values = string.Join(", ", data.Values.Select(TranslateCSharpTypeToMysqlValue));

        return $"INSERT INTO `{db.Name}`.`{tb.Name}` ({columns}) VALUES ({values});";
    }

    /// <summary>
    /// Performs an upsert operation by first attempting an update, and if no rows are affected, inserts a new row.
    /// Base implementation that other methods call.
    /// </summary>
    public async Task<int> UpdateInsertAsync(Dictionary<string, object?> data, string whereCondition,
        CancellationToken token)
    {
        if (data.Count == 0 || _connection == null || _name == null || Database == null)
            return -1;

        // Generate the update command
        var updateCommand = GenerateUpdateCommand(Database, this, data, whereCondition);
        var rowsAffected = await _connection.UpdateSelectAsync(updateCommand, token).ConfigureAwait(false);

        // If no rows were updated, perform an insert
        if (rowsAffected == 0)
        {
            var insertCommand = GenerateInsertCommand(Database, this, data);
            rowsAffected = await _connection.UpdateSelectAsync(insertCommand, token).ConfigureAwait(false);
        }

        return rowsAffected;
    }

    /// <summary>
    /// Performs an upsert operation synchronously by first attempting an update, and if no rows are affected, inserts a new row.
    /// Base implementation that other methods call.
    /// </summary>
    public int UpdateInsert(Dictionary<string, object?> data, string whereCondition)
    {
        if (data == null || data.Count == 0 || _connection == null || _name == null || Database == null)
            return -1;

        // Generate the update command
        var updateCommand = GenerateUpdateCommand(Database, this, data, whereCondition);
        var rowsAffected = _connection.UpdateSelect(updateCommand);

        // If no rows were updated, perform an insert
        if (rowsAffected == 0)
        {
            var insertCommand = GenerateInsertCommand(Database, this, data);
            rowsAffected = _connection.UpdateSelect(insertCommand);
        }

        return rowsAffected;
    }

    /// <summary>
    /// Performs an upsert operation by first attempting an update, and if no rows are affected, inserts a new row.
    /// </summary>
    public async Task<int> UpdateInsertAsync(CancellationToken token, string whereCondition,
        params KeyValuePair<string, object?>[] data)
    {
        return await UpdateInsertAsync(data.ToDictionary(), whereCondition, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Performs an upsert operation by first attempting an update, and if no rows are affected, inserts a new row.
    /// </summary>
    public async Task<int> UpdateInsertAsync(string whereCondition, params KeyValuePair<string, object?>[] data)
    {
        return await UpdateInsertAsync(data.ToDictionary(), whereCondition, CancellationToken.None)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Performs an upsert operation synchronously by first attempting an update, and if no rows are affected, inserts a new row.
    /// </summary>
    public int UpdateInsert(string whereCondition, params KeyValuePair<string, object?>[] data)
    {
        return UpdateInsert(data.ToDictionary(), whereCondition);
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Generates a DELETE SQL query for the specified table.
    /// </summary>
    /// <param name="tb">
    /// The table for which the DELETE statement is generated. This parameter should include the database name and table name.
    /// </param>
    /// <param name="where">The WHERE clause specifying which rows to delete.</param>
    /// <param name="add">Additional SQL to append to the query, such as ordering or limits (optional).</param>
    /// <returns>A string containing the generated SQL DELETE statement.</returns>
    private static string GenerateDelete(Table tb, string where, string add = "")
    {
        return $"DELETE FROM `{tb.Database.Name}`.`{tb.Name}` WHERE {where} {add}";
    }

    /// <summary>
    /// Executes a DELETE operation on the current table based on the specified condition.
    /// </summary>
    /// <param name="where">The WHERE clause specifying which rows to delete.</param>
    /// <param name="add">Additional SQL to append to the query (optional).</param>
    /// <returns>
    /// <c>true</c> if one or more rows were deleted; otherwise, <c>false</c>.
    /// </returns>
    public bool Delete(string where, string add = "") =>
        _connection.UpdateSelect(GenerateDelete(this, where, add)) > 0;

    /// <summary>
    /// Asynchronously executes a DELETE operation on the current table based on the specified condition.
    /// </summary>
    /// <param name="where">The WHERE clause specifying which rows to delete.</param>
    /// <param name="add">Additional SQL to append to the query (optional).</param>
    /// <param name="token">
    /// A cancellation token to observe while waiting for the task to complete (optional).
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous delete operation. The task result contains <c>true</c> if one or more rows were deleted; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> DeleteAsync(string where, string add = "", CancellationToken token = default) =>
        await _connection.UpdateSelectAsync(GenerateDelete(this, where, add), token) > 0;

    /// <summary>
    /// Executes a DELETE operation on the specified table based on the provided condition.
    /// </summary>
    /// <param name="tb">The table on which the DELETE operation will be performed.</param>
    /// <param name="where">The WHERE clause specifying which rows to delete.</param>
    /// <param name="add">Additional SQL to append to the query (optional).</param>
    /// <returns>
    /// <c>true</c> if one or more rows were deleted; otherwise, <c>false</c>.
    /// </returns>
    public static bool Delete(Table tb, string where, string add = "") =>
        ((Connection)tb).UpdateSelect(GenerateDelete(tb, where, add)) > 0;

    /// <summary>
    /// Asynchronously executes a DELETE operation on the specified table based on the provided condition.
    /// </summary>
    /// <param name="tb">The table on which the DELETE operation will be performed.</param>
    /// <param name="where">The WHERE clause specifying which rows to delete.</param>
    /// <param name="add">Additional SQL to append to the query (optional).</param>
    /// <param name="token">
    /// A cancellation token to observe while waiting for the task to complete (optional).
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous delete operation. The task result contains <c>true</c> if one or more rows were deleted; otherwise, <c>false</c>.
    /// </returns>
    public static async Task<bool> DeleteAsync(Table tb, string where, string add = "",
        CancellationToken token = default) =>
        await ((Connection)tb).UpdateSelectAsync(GenerateDelete(tb, where, add), token) > 0;

    #endregion

    #region RENAME

    /// <summary>
    /// Asynchronously renames the table.
    /// </summary>
    private async Task<bool> RenameAsync(string to, bool useChecks = true, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(to))
            throw new ArgumentNullException(nameof(to), "New table name cannot be null or empty");

        if (_connection == null || _name == null || Database == null)
            return false;

        if (useChecks && await IsTableExistsAsync(token, to).ConfigureAwait(false))
            return false;

        if (await _connection.UpdateSelectAsync(
                $"RENAME TABLE `{Database.Name}`.`{Name}` TO `{Database.Name}`.`{to}`;",
                token).ConfigureAwait(false) == 0)
            return false;

        _name = to;
        return true;
    }

    /// <summary>
    /// Renames the table.
    /// </summary>
    private bool Rename(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "New table name cannot be null or empty");

        try
        {
            var ra = RenameAsync(name, true, CancellationToken.None);
            ra.Wait();
            return ra.Result;
        }
        catch (AggregateException ae)
        {
            if (ae.InnerException != null)
                throw ae.InnerException;
            throw;
        }
    }

    /// <summary>
    /// Renames the table asynchronously with existence check.
    /// </summary>
    public async Task<bool> RenameAsync(string name, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "New table name cannot be null or empty");

        return await RenameAsync(name, true, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Renames the table ignoring existence checks.
    /// </summary>
    public bool RenameIgnoreChecks(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "New table name cannot be null or empty");

        try
        {
            var ra = RenameAsync(name, false, CancellationToken.None);
            ra.Wait();
            return ra.Result;
        }
        catch (AggregateException ae)
        {
            if (ae.InnerException != null)
                throw ae.InnerException;
            throw;
        }
    }

    /// <summary>
    /// Renames the table asynchronously ignoring existence checks.
    /// </summary>
    public async Task<bool> RenameAsyncIgnoreChecks(string name, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "New table name cannot be null or empty");

        return await RenameAsync(name, false, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Renames this table to the specified new name.
    /// </summary>
    public static bool Rename(Database db, string table, string newName)
    {
        if (db == null)
            throw new ArgumentNullException(nameof(db));
        if (string.IsNullOrEmpty(table))
            throw new ArgumentNullException(nameof(table));
        if (string.IsNullOrEmpty(newName))
            throw new ArgumentNullException(nameof(newName));

        var tableObj = new Table(db, table);
        return tableObj.Rename(newName);
    }

    /// <summary>
    /// Asynchronously renames a table to the specified new name.
    /// </summary>
    public static async Task<bool> RenameAsync(Database db, string table, string newName,
        CancellationToken token = default)
    {
        if (db == null)
            throw new ArgumentNullException(nameof(db));
        if (string.IsNullOrEmpty(table))
            throw new ArgumentNullException(nameof(table));
        if (string.IsNullOrEmpty(newName))
            throw new ArgumentNullException(nameof(newName));

        var tableObj = new Table(db, table);
        return await tableObj.RenameAsync(newName, token).ConfigureAwait(false);
    }

    #endregion

    #region OPS

    /// <summary>
    /// Implicitly converts a Table to its associated Connection.
    /// </summary>
    public static implicit operator Connection(Table t)
    {
        if (ReferenceEquals(t.Database, null))
            throw new NullReferenceException("Table has no associated database or connection is in dispose mode");

        return t.Database;
    }

    /// <summary>
    /// Implicitly converts a Table to its name as a string.
    /// </summary>
    public static implicit operator string(Table t)
    {
        return t.Name;
    }

    /// <summary>
    /// Operator to check equality between two Table objects.
    /// </summary>
    public static bool operator ==(Table? left, Table? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            return false;

        return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) &&
               ReferenceEquals(left.Database, right.Database);
    }

    /// <summary>
    /// Operator to check inequality between two Table objects.
    /// </summary>
    public static bool operator !=(Table? left, Table? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current Table.
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj is not Table other)
            return false;

        return this == other;
    }

    /// <summary>
    /// Returns a hash code for this Table.
    /// </summary>
    public override int GetHashCode()
    {
        return HashCode.Combine(
            Name?.ToLowerInvariant()?.GetHashCode() ?? 0,
            Database?.GetHashCode() ?? 0);
    }

    /// <summary>
    /// Returns a string that represents the current table.
    /// </summary>
    public override string ToString()
    {
        return Database != null ? $"{Database.Name}.{Name}" : Name;
    }

    #endregion
}