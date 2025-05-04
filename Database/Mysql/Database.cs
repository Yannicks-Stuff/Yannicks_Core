using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Yannick.Extensions.ArrayExtensions;

namespace Yannick.Database.Mysql;

/// <summary>
/// Represents a MySQL database, providing methods to interact and manage it.
/// </summary>
public sealed class Database : IDisposable, IAsyncDisposable
{
    private readonly Connection? _connection;

    /// <summary>
    /// Gets the name of the database.
    /// </summary>
    public readonly string? Name;

    private Database()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="name">The name of the database.</param>
    /// <exception cref="ArgumentNullException">Thrown when connection or name is null.</exception>
    public Database(Connection connection, string name)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Asynchronously disposes of the object.
    /// </summary>
    /// <returns>A ValueTask representing the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
            await _connection.DisposeAsync();
    }

    /// <summary>
    /// Disposes of the object.
    /// </summary>
    public void Dispose()
    {
        _connection?.Dispose();
    }

    /*/// <summary>
    /// Gets or sets a table in the database.
    /// </summary>
    /// <param name="name">The name of the table.</param>
    /// <param name="columns">The column definitions for the table.</param>
    /// <param name="foreignKeys">The foreign key definitions for the table.</param>
    /// <param name="indexes">The index definitions for the table.</param>
    /// <param name="charset">The character set for the table.</param>
    /// <returns>The table instance.</returns>
    public Table this[string name, IEnumerable<Table.ColumnDefinition>? columns = null,
        IEnumerable<Table.ForeignKeyDefinition>? foreignKeys = null,
        IEnumerable<Table.IndexDefinition>? indexes = null, string? charset = null]
    {
        get => new Table(this, name);
        set
        {
            if (columns != null)
                Table.CreateTable(this[name], columns, foreignKeys, indexes, charset);
        }
    }*/

    /// <summary>
    /// Optimizes tables by adding indexes, constraints, and optimizing data types.
    /// </summary>
    /// <param name="tableNames">The names of the tables to optimize.</param>
    /// <returns>The number of tables optimized.</returns>
    public int OptimizeTables(params string[] tableNames)
    {
        if (tableNames.IsEmptyOrNull())
            return 0;

        var optimizedCount = 0;
        foreach (var tableName in tableNames)
            if (OptimizeTable(tableName))
                optimizedCount++;

        return optimizedCount;
    }

    /// <summary>
    /// Asynchronously optimizes tables by adding indexes, constraints, and optimizing data types.
    /// </summary>
    /// <param name="token">The cancellation token to use.</param>
    /// <param name="tableNames">The names of the tables to optimize.</param>
    /// <returns>The number of tables optimized.</returns>
    public async Task<int> OptimizeTablesAsync(CancellationToken token = default, params string[] tableNames)
    {
        if (tableNames.IsEmptyOrNull())
            return 0;

        var optimizedCount = 0;
        foreach (var tableName in tableNames)
            if (await OptimizeTableAsync(tableName, token))
                optimizedCount++;

        return optimizedCount;
    }

    /// <summary>
    /// Optimizes a specific table by adding indexes, constraints, and optimizing data types.
    /// </summary>
    /// <param name="tableName">The name of the table to optimize.</param>
    /// <returns><c>true</c> if the table was optimized; otherwise, <c>false</c>.</returns>
    private bool OptimizeTable(string tableName)
    {
        if (_connection == null || string.IsNullOrWhiteSpace(tableName))
            return false;

        try
        {
            // Analyze table structure
            var tableInfo = _connection.Select(
                $"SHOW FULL COLUMNS FROM `{tableName}` IN `{Name}`;");

            // Optimize date types
            OptimizeDateTypes(tableName, tableInfo);

            // Add missing indexes
            AddMissingIndexes(tableName, tableInfo);

            // Add constraints
            AddMissingConstraints(tableName);

            // Optimize table
            _connection.UpdateSelect($"OPTIMIZE TABLE `{tableName}`;");

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Asynchronously optimizes a specific table by adding indexes, constraints, and optimizing data types.
    /// </summary>
    /// <param name="tableName">The name of the table to optimize.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns><c>true</c> if the table was optimized; otherwise, <c>false</c>.</returns>
    private async Task<bool> OptimizeTableAsync(string tableName, CancellationToken token)
    {
        if (_connection == null || string.IsNullOrWhiteSpace(tableName))
            return false;

        try
        {
            // Analyze table structure
            var tableInfo = new List<IReadOnlyDictionary<string, object?>>();
            await foreach (var column in _connection.SelectAsync(
                               $"SHOW FULL COLUMNS FROM `{tableName}` IN `{Name}`;", token))
            {
                tableInfo.Add(column);
            }

            // Optimize date types
            await OptimizeDateTypesAsync(tableName, tableInfo, token);

            // Add missing indexes
            await AddMissingIndexesAsync(tableName, tableInfo, token);

            // Add constraints
            await AddMissingConstraintsAsync(tableName, token);

            // Optimize table
            await _connection.UpdateSelectAsync($"OPTIMIZE TABLE `{tableName}`;", token);

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Optimizes date types in a table.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="tableInfo">The table column information.</param>
    private void OptimizeDateTypes(string tableName, IEnumerable<dynamic> tableInfo)
    {
        foreach (var column in tableInfo)
        {
            string columnName = column["Field"].ToString()!;
            string columnType = column["Type"].ToString()!.ToLower();

            // Check if date type can be optimized
            if (columnType.Contains("datetime") && !columnType.Contains("("))
            {
                // Convert DATETIME to DATETIME(0) if no fractional seconds are needed
                _connection!.UpdateSelect(
                    $"ALTER TABLE `{tableName}` MODIFY COLUMN `{columnName}` DATETIME(0);");
            }
            else if (columnType.Contains("timestamp") && !columnType.Contains("("))
            {
                // Convert TIMESTAMP to TIMESTAMP(0) if no fractional seconds are needed
                _connection!.UpdateSelect(
                    $"ALTER TABLE `{tableName}` MODIFY COLUMN `{columnName}` TIMESTAMP(0);");
            }
            else if (columnType.Contains("varchar") &&
                     int.TryParse(columnType.Replace("varchar(", "").Replace(")", ""), out var size))
            {
                // Optimize VARCHAR columns with excessive length
                var actualMaxLength = GetColumnMaxLength(tableName, columnName);
                if (actualMaxLength > 0 && actualMaxLength < size * 0.5 && actualMaxLength > 10)
                {
                    // If actual length is less than half the defined size and > 10 chars
                    var optimizedSize = (int)(actualMaxLength * 1.5); // Add 50% buffer
                    _connection!.UpdateSelect(
                        $"ALTER TABLE `{tableName}` MODIFY COLUMN `{columnName}` VARCHAR({optimizedSize});");
                }
            }
        }
    }

    /// <summary>
    /// Gets the maximum length of data in a column.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="columnName">The name of the column.</param>
    /// <returns>The maximum length of data in the column.</returns>
    private int GetColumnMaxLength(string tableName, string columnName)
    {
        try
        {
            var result = _connection!.Select(
                $"SELECT MAX(CHAR_LENGTH(`{columnName}`)) AS max_length FROM `{tableName}`;");
            if (result.Any() && result.First()["max_length"] != null)
            {
                return Convert.ToInt32(result.First()["max_length"]);
            }
        }
        catch
        {
            // Ignore exceptions when calculating max length
        }

        return -1;
    }

    /// <summary>
    /// Asynchronously optimizes date types in a table.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="tableInfo">The table column information.</param>
    /// <param name="token">The cancellation token to use.</param>
    private async Task OptimizeDateTypesAsync(string tableName, IEnumerable<dynamic> tableInfo, CancellationToken token)
    {
        foreach (var column in tableInfo)
        {
            if (token.IsCancellationRequested)
                break;

            string columnName = column["Field"].ToString()!;
            string columnType = column["Type"].ToString()!.ToLower();

            // Check if date type can be optimized
            if (columnType.Contains("datetime") && !columnType.Contains("("))
            {
                // Convert DATETIME to DATETIME(0) if no fractional seconds are needed
                await _connection!.UpdateSelectAsync(
                    $"ALTER TABLE `{tableName}` MODIFY COLUMN `{columnName}` DATETIME(0);", token);
            }
            else if (columnType.Contains("timestamp") && !columnType.Contains("("))
            {
                // Convert TIMESTAMP to TIMESTAMP(0) if no fractional seconds are needed
                await _connection!.UpdateSelectAsync(
                    $"ALTER TABLE `{tableName}` MODIFY COLUMN `{columnName}` TIMESTAMP(0);", token);
            }
            else if (columnType.Contains("varchar") &&
                     int.TryParse(columnType.Replace("varchar(", "").Replace(")", ""), out var size))
            {
                // Optimize VARCHAR columns with excessive length
                var actualMaxLength = await GetColumnMaxLengthAsync(tableName, columnName, token);
                if (actualMaxLength > 0 && actualMaxLength < size * 0.5 && actualMaxLength > 10)
                {
                    // If actual length is less than half the defined size and > 10 chars
                    var optimizedSize = (int)(actualMaxLength * 1.5); // Add 50% buffer
                    await _connection!.UpdateSelectAsync(
                        $"ALTER TABLE `{tableName}` MODIFY COLUMN `{columnName}` VARCHAR({optimizedSize});", token);
                }
            }
        }
    }

    /// <summary>
    /// Asynchronously gets the maximum length of data in a column.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="columnName">The name of the column.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns>The maximum length of data in the column.</returns>
    private async Task<int> GetColumnMaxLengthAsync(string tableName, string columnName, CancellationToken token)
    {
        try
        {
            var maxLength = -1;
            await foreach (var item in _connection!.SelectAsync(
                               $"SELECT MAX(CHAR_LENGTH(`{columnName}`)) AS max_length FROM `{tableName}`;", token))
            {
                if (item["max_length"] != null)
                {
                    maxLength = Convert.ToInt32(item["max_length"]);
                    break; // Only need the first row
                }
            }

            return maxLength;
        }
        catch
        {
            // Ignore exceptions when calculating max length
        }

        return -1;
    }

    /// <summary>
    /// Adds missing indexes to a table.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="tableInfo">The table column information.</param>
    private void AddMissingIndexes(string tableName, IEnumerable<dynamic> tableInfo)
    {
        // Get existing indexes
        var existingIndexes = _connection!.Select(
            $"SHOW INDEX FROM `{tableName}` IN `{Name}`;");

        // Find commonly used column patterns for indexing
        var indexedColumns = new HashSet<string>();
        foreach (var index in existingIndexes)
        {
            indexedColumns.Add(index["Column_name"].ToString()!);
        }

        foreach (var column in tableInfo)
        {
            string columnName = column["Field"].ToString()!;
            string columnType = column["Type"].ToString()!.ToLower();

            // If column is not already indexed
            if (!indexedColumns.Contains(columnName))
            {
                // Add index for ID-like columns
                if (columnName.EndsWith("Id", StringComparison.OrdinalIgnoreCase) ||
                    columnName.EndsWith("_id", StringComparison.OrdinalIgnoreCase))
                {
                    _connection.UpdateSelect(
                        $"ALTER TABLE `{tableName}` ADD INDEX `idx_{tableName}_{columnName}` (`{columnName}`);");
                }
                // Add index for date columns
                else if (columnType.Contains("date") || columnType.Contains("time"))
                {
                    _connection.UpdateSelect(
                        $"ALTER TABLE `{tableName}` ADD INDEX `idx_{tableName}_{columnName}` (`{columnName}`);");
                }
                // Add index for common search columns
                else if (columnName.Contains("name", StringComparison.OrdinalIgnoreCase) ||
                         columnName.Contains("code", StringComparison.OrdinalIgnoreCase) ||
                         columnName.Contains("status", StringComparison.OrdinalIgnoreCase))
                {
                    _connection.UpdateSelect(
                        $"ALTER TABLE `{tableName}` ADD INDEX `idx_{tableName}_{columnName}` (`{columnName}`);");
                }
            }
        }
    }

    /// <summary>
    /// Asynchronously adds missing indexes to a table.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="tableInfo">The table column information.</param>
    /// <param name="token">The cancellation token to use.</param>
    private async Task AddMissingIndexesAsync(string tableName, IEnumerable<dynamic> tableInfo, CancellationToken token)
    {
        // Get existing indexes
        var indexedColumns = new HashSet<string>();
        await foreach (var index in _connection!.SelectAsync(
                           $"SHOW INDEX FROM `{tableName}` IN `{Name}`;", token))
        {
            indexedColumns.Add(index["Column_name"].ToString()!);
        }

        foreach (var column in tableInfo)
        {
            if (token.IsCancellationRequested)
                break;

            string columnName = column["Field"].ToString()!;
            string columnType = column["Type"].ToString()!.ToLower();

            // If column is not already indexed
            if (!indexedColumns.Contains(columnName))
            {
                // Add index for ID-like columns
                if (columnName.EndsWith("Id", StringComparison.OrdinalIgnoreCase) ||
                    columnName.EndsWith("_id", StringComparison.OrdinalIgnoreCase))
                {
                    await _connection.UpdateSelectAsync(
                        $"ALTER TABLE `{tableName}` ADD INDEX `idx_{tableName}_{columnName}` (`{columnName}`);", token);
                }
                // Add index for date columns
                else if (columnType.Contains("date") || columnType.Contains("time"))
                {
                    await _connection.UpdateSelectAsync(
                        $"ALTER TABLE `{tableName}` ADD INDEX `idx_{tableName}_{columnName}` (`{columnName}`);", token);
                }
                // Add index for common search columns
                else if (columnName.Contains("name", StringComparison.OrdinalIgnoreCase) ||
                         columnName.Contains("code", StringComparison.OrdinalIgnoreCase) ||
                         columnName.Contains("status", StringComparison.OrdinalIgnoreCase))
                {
                    await _connection.UpdateSelectAsync(
                        $"ALTER TABLE `{tableName}` ADD INDEX `idx_{tableName}_{columnName}` (`{columnName}`);", token);
                }
            }
        }
    }

    /// <summary>
    /// Adds missing constraints to a table.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    private void AddMissingConstraints(string tableName)
    {
        // Check if there's a primary key
        var primaryKey = _connection!.Select(
            $"SHOW KEYS FROM `{tableName}` WHERE Key_name = 'PRIMARY';");

        if (!primaryKey.Any())
        {
            // Try to find an ID column to make as primary key
            var idColumn = _connection.Select(
                $"SHOW COLUMNS FROM `{tableName}` WHERE Field = 'id' OR Field = 'ID' OR Field = '{tableName}Id';");

            if (idColumn.Any())
            {
                var columnName = idColumn.First()["Field"].ToString()!;
                _connection.UpdateSelect(
                    $"ALTER TABLE `{tableName}` ADD PRIMARY KEY (`{columnName}`);");
            }
        }

        // Find potential foreign key relationships
        var columnInfo = _connection.Select($"SHOW COLUMNS FROM `{tableName}`;");
        foreach (var column in columnInfo)
        {
            var columnName = column["Field"].ToString()!;

            // If column looks like a foreign key (ends with Id or _id)
            if ((columnName.EndsWith("Id", StringComparison.OrdinalIgnoreCase) ||
                 columnName.EndsWith("_id", StringComparison.OrdinalIgnoreCase)) &&
                columnName.Length > 3)
            {
                // Extract potential referenced table name
                string potentialTableName;
                if (columnName.EndsWith("Id", StringComparison.OrdinalIgnoreCase))
                {
                    potentialTableName = columnName.Substring(0, columnName.Length - 2);
                }
                else // _id
                {
                    potentialTableName = columnName.Substring(0, columnName.Length - 3);
                }

                // Check if the table exists
                var tableExists = _connection.Select(
                    $"SELECT COUNT(*) as count FROM information_schema.tables WHERE " +
                    $"table_schema = '{Name}' AND table_name = '{potentialTableName}';");

                if (tableExists.Any() && Convert.ToInt32(tableExists.First()["count"]) > 0)
                {
                    // Check if constraint already exists
                    var constraintExists = _connection.Select(
                        $"SELECT COUNT(*) as count FROM information_schema.KEY_COLUMN_USAGE " +
                        $"WHERE TABLE_SCHEMA = '{Name}' AND TABLE_NAME = '{tableName}' " +
                        $"AND COLUMN_NAME = '{columnName}' AND REFERENCED_TABLE_NAME IS NOT NULL;");

                    if (constraintExists.Any() && Convert.ToInt32(constraintExists.First()["count"]) == 0)
                    {
                        try
                        {
                            // Add foreign key constraint with ON DELETE CASCADE
                            _connection.UpdateSelect(
                                $"ALTER TABLE `{tableName}` ADD CONSTRAINT `fk_{tableName}_{columnName}` " +
                                $"FOREIGN KEY (`{columnName}`) REFERENCES `{potentialTableName}`(id) " +
                                $"ON DELETE CASCADE ON UPDATE CASCADE;");
                        }
                        catch
                        {
                            // If failed to add constraint, just continue
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Asynchronously adds missing constraints to a table.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="token">The cancellation token to use.</param>
    private async Task AddMissingConstraintsAsync(string tableName, CancellationToken token)
    {
        // Check if there's a primary key
        var hasPrimaryKey = false;
        await foreach (var key in _connection!.SelectAsync(
                           $"SHOW KEYS FROM `{tableName}` WHERE Key_name = 'PRIMARY';", token))
        {
            hasPrimaryKey = true;
            break;
        }

        if (!hasPrimaryKey)
        {
            // Try to find an ID column to make as primary key
            string? idColumnName = null;
            await foreach (var column in _connection.SelectAsync(
                               $"SHOW COLUMNS FROM `{tableName}` WHERE Field = 'id' OR Field = 'ID' OR Field = '{tableName}Id';",
                               token))
            {
                idColumnName = column["Field"].ToString();
                break;
            }

            if (!string.IsNullOrEmpty(idColumnName))
            {
                await _connection.UpdateSelectAsync(
                    $"ALTER TABLE `{tableName}` ADD PRIMARY KEY (`{idColumnName}`);", token);
            }
        }

        // Find potential foreign key relationships
        var columns = new List<IReadOnlyDictionary<string, object?>>();
        await foreach (var column in _connection.SelectAsync($"SHOW COLUMNS FROM `{tableName}`;", token))
        {
            columns.Add(column);
        }

        foreach (var column in columns)
        {
            if (token.IsCancellationRequested)
                break;

            var columnName = column["Field"].ToString()!;

            // If column looks like a foreign key (ends with Id or _id)
            if ((columnName.EndsWith("Id", StringComparison.OrdinalIgnoreCase) ||
                 columnName.EndsWith("_id", StringComparison.OrdinalIgnoreCase)) &&
                columnName.Length > 3)
            {
                // Extract potential referenced table name
                string potentialTableName;
                if (columnName.EndsWith("Id", StringComparison.OrdinalIgnoreCase))
                {
                    potentialTableName = columnName.Substring(0, columnName.Length - 2);
                }
                else // _id
                {
                    potentialTableName = columnName.Substring(0, columnName.Length - 3);
                }

                // Check if the table exists
                var tableExists = false;
                var tableCount = 0;
                await foreach (var item in _connection.SelectAsync(
                                   $"SELECT COUNT(*) as count FROM information_schema.tables WHERE " +
                                   $"table_schema = '{Name}' AND table_name = '{potentialTableName}';", token))
                {
                    tableCount = Convert.ToInt32(item["count"]);
                    tableExists = tableCount > 0;
                    break;
                }

                if (tableExists)
                {
                    // Check if constraint already exists
                    var constraintExists = false;
                    var constraintCount = 0;
                    await foreach (var item in _connection.SelectAsync(
                                       $"SELECT COUNT(*) as count FROM information_schema.KEY_COLUMN_USAGE " +
                                       $"WHERE TABLE_SCHEMA = '{Name}' AND TABLE_NAME = '{tableName}' " +
                                       $"AND COLUMN_NAME = '{columnName}' AND REFERENCED_TABLE_NAME IS NOT NULL;",
                                       token))
                    {
                        constraintCount = Convert.ToInt32(item["count"]);
                        constraintExists = constraintCount > 0;
                        break;
                    }

                    if (!constraintExists)
                    {
                        try
                        {
                            // Add foreign key constraint with ON DELETE CASCADE
                            await _connection.UpdateSelectAsync(
                                $"ALTER TABLE `{tableName}` ADD CONSTRAINT `fk_{tableName}_{columnName}` " +
                                $"FOREIGN KEY (`{columnName}`) REFERENCES `{potentialTableName}`(id) " +
                                $"ON DELETE CASCADE ON UPDATE CASCADE;", token);
                        }
                        catch
                        {
                            // If failed to add constraint, just continue
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Processes a specific number of tables from the provided list of table names.
    /// </summary>
    /// <param name="count">The number of table names to process.</param>
    /// <param name="tableNames">The names of the tables to process.</param>
    /// <returns>The number of successfully processed tables.</returns>
    public int ProcessTables(int count, params string[] tableNames)
    {
        if (_connection == null || tableNames == null || count <= 0 || count > tableNames.Length)
            return 0;

        var successCount = 0;

        for (var i = 0; i < count; i++)
        {
            if (string.IsNullOrWhiteSpace(tableNames[i]))
                continue;

            try
            {
                // Check if the table exists
                var tableExists = _connection.Select(
                    $"SELECT COUNT(*) as count FROM information_schema.tables WHERE table_schema = '{Name}' AND table_name = '{tableNames[i]}';");

                if (tableExists.Any() && Convert.ToInt32(tableExists.First()["count"]) > 0)
                {
                    // Process the table - add optimizations
                    OptimizeTable(tableNames[i]);
                    successCount++;
                }
            }
            catch
            {
                // Continue with the next table if an error occurs
                continue;
            }
        }

        return successCount;
    }

    /// <summary>
    /// Asynchronously processes a specific number of tables from the provided list of table names.
    /// </summary>
    /// <param name="count">The number of table names to process.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <param name="tableNames">The names of the tables to process.</param>
    /// <returns>The number of successfully processed tables.</returns>
    public async Task<int> ProcessTablesAsync(int count, CancellationToken token = default, params string[] tableNames)
    {
        if (_connection == null || tableNames == null || count <= 0 || count > tableNames.Length)
            return 0;

        var successCount = 0;

        for (var i = 0; i < count && i < tableNames.Length && !token.IsCancellationRequested; i++)
        {
            if (string.IsNullOrWhiteSpace(tableNames[i]))
                continue;

            try
            {
                // Check if the table exists
                var tableExists = false;
                await foreach (var item in _connection.SelectAsync(
                                   $"SELECT COUNT(*) as count FROM information_schema.tables WHERE table_schema = '{Name}' AND table_name = '{tableNames[i]}';",
                                   token))
                {
                    if (Convert.ToInt32(item["count"]) > 0)
                    {
                        tableExists = true;
                    }

                    break;
                }

                if (tableExists)
                {
                    // Process the table - add optimizations
                    await OptimizeTableAsync(tableNames[i], token);
                    successCount++;
                }
            }
            catch
            {
                // Continue with the next table if an error occurs
                continue;
            }
        }

        return successCount;
    }

    /// <summary>
    /// Creates the database.
    /// </summary>
    /// <param name="checkIfNotExists">Indicates whether to check if the database does not exist before creation.</param>
    /// <returns>The number of affected rows.</returns>
    public int CreateDatabase(bool checkIfNotExists)
    {
        return _connection == null || Name == null ? -1 : CreateDatabase(_connection, Name, checkIfNotExists);
    }

    /// <summary>
    /// Asynchronously creates the database.
    /// </summary>
    /// <param name="checkIfNotExists">Indicates whether to check if the database does not exist before creation.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> CreateDatabaseAsync(bool checkIfNotExists, CancellationToken token = default)
    {
        return _connection == null || Name == null
            ? -1
            : await CreateDatabaseAsync(_connection, Name, checkIfNotExists, token);
    }

    /// <summary>
    /// Drops the database.
    /// </summary>
    /// <param name="checkIfExists">Indicates whether to check if the database exists before dropping it.</param>
    /// <returns>The number of affected rows.</returns>
    public int DropDatabase(bool checkIfExists)
    {
        return _connection == null || Name == null ? -1 : DropDatabase(_connection, Name, checkIfExists);
    }

    /// <summary>
    /// Asynchronously drops the database.
    /// </summary>
    /// <param name="checkIfExists">Indicates whether to check if the database exists before dropping it.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> DropDatabaseAsync(bool checkIfExists, CancellationToken token = default)
    {
        return _connection == null || Name == null
            ? -1
            : await DropDatabaseAsync(_connection, Name, checkIfExists, token);
    }

    /// <summary>
    /// Synchronously selects the specified database.
    /// </summary>
    public void Use() => Use(this, Name);

    /// <summary>
    /// Synchronously selects the specified database.
    /// </summary>
    /// <param name="database">The database to select.</param>
    /// <param name="newDatabaseName">The name of the database to switch to.</param>
    /// <exception cref="ArgumentException">Thrown when newDatabaseName is null or whitespace.</exception>
    public static void Use(Database database, string newDatabaseName)
    {
        if (string.IsNullOrWhiteSpace(newDatabaseName))
            throw new ArgumentException("Database name cannot be null or whitespace.", nameof(newDatabaseName));

        database._connection.UpdateSelect($"USE `{newDatabaseName}`;");
    }

    /// <summary>
    /// Asynchronously selects the specified database.
    /// </summary>
    /// <param name="database">The database to select.</param>
    /// <param name="newDatabaseName">The name of the database to switch to.</param>
    /// <param name="token">A cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentException">Thrown when newDatabaseName is null or whitespace.</exception>
    public static async Task UseAsync(Database database, string newDatabaseName, CancellationToken token = default)
    {
        if (string.IsNullOrWhiteSpace(newDatabaseName))
            throw new ArgumentException("Database name cannot be null or whitespace.", nameof(newDatabaseName));

        await database._connection.UpdateSelectAsync($"USE `{newDatabaseName}`;", token);
    }

    /// <summary>
    /// Asynchronously selects the specified database.
    /// </summary>
    /// <param name="token">A cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UseAsync(CancellationToken token = default)
        => await UseAsync(this, Name, token);

    /// <summary>
    /// Checks if the database exists.
    /// </summary>
    /// <returns><c>true</c> if the database exists; otherwise, <c>false</c>.</returns>
    public bool IsDatabaseExists()
    {
        return _connection != null && Name != null && IsDatabaseExists(_connection, Name);
    }

    /// <summary>
    /// Asynchronously checks if the database exists.
    /// </summary>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns><c>true</c> if the database exists; otherwise, <c>false</c>.</returns>
    public async Task<bool> IsDatabaseExistsAsync(CancellationToken token = default)
    {
        return _connection != null && Name != null && await IsDatabaseExistsAsync(_connection, token, Name);
    }

    /// <summary>
    /// Retrieves all tables from the database.
    /// </summary>
    /// <returns>An enumeration of tables.</returns>
    public IEnumerable<Table> ShowTables()
    {
        if (_connection == null)
            yield break;

        foreach (var t in ShowTables(this))
            yield return t;
    }

    /// <summary>
    /// Asynchronously retrieves all tables from the database.
    /// </summary>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns>An asynchronous enumeration of tables.</returns>
    public async IAsyncEnumerable<Table> ShowTablesAsync(
        [EnumeratorCancellation] CancellationToken token = default)
    {
        if (_connection == null)
            yield break;

        await foreach (var t in ShowTablesAsync(this, token))
            yield return t;
    }

    /// <summary>
    /// Retrieves all tables from a given database.
    /// </summary>
    /// <param name="database">The database to retrieve tables from.</param>
    /// <returns>An enumeration of tables.</returns>
    public static IEnumerable<Table> ShowTables(Database database)
    {
        var connection = (Connection)database;

        foreach (var name in connection.Select(
                     $"SELECT * FROM information_schema.tables WHERE TABLE_SCHEMA = '{database.Name}';"))

            yield return new Table(database, name["TABLE_NAME"]!.ToString()!);
    }

    /// <summary>
    /// Asynchronously retrieves all tables from a given database.
    /// </summary>
    /// <param name="database">The database to retrieve tables from.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns>An asynchronous enumeration of tables.</returns>
    public static async IAsyncEnumerable<Table> ShowTablesAsync(Database database,
        [EnumeratorCancellation] CancellationToken token = default)
    {
        var connection = (Connection)database;

        await foreach (var name in connection.SelectAsync(
                           $"SELECT * FROM information_schema.tables WHERE TABLE_SCHEMA = '{database.Name}';", token))
            yield return new Table(database, name["TABLE_NAME"]!.ToString()!);
    }

    /// <summary>
    /// Retrieves all databases from a given connection.
    /// </summary>
    /// <param name="connection">The connection to retrieve databases from.</param>
    /// <returns>An enumeration of databases.</returns>
    public static IEnumerable<Database> ShowDatabases(Connection connection)
    {
        foreach (var db in connection.SelectColumns("SHOW DATABASES;"))
            yield return new Database(connection, db.Values.First()!.ToString()!);
    }

    /// <summary>
    /// Asynchronously retrieves all databases from a given connection.
    /// </summary>
    /// <param name="connection">The connection to retrieve databases from.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns>An asynchronous enumeration of databases.</returns>
    public static async IAsyncEnumerable<Database> ShowDatabasesAsync(Connection connection,
        [EnumeratorCancellation] CancellationToken token = default)
    {
        await foreach (var db in connection.SelectColumnsAsync("SHOW DATABASES;", token))
            yield return new Database(connection, db.Values.First()!.ToString()!);
    }

    /// <summary>
    /// Checks if specific databases exist on a given connection.
    /// </summary>
    /// <param name="connection">The connection to check.</param>
    /// <param name="names">The names of the databases to check.</param>
    /// <returns><c>true</c> if all specified databases exist; otherwise, <c>false</c>.</returns>
    public static bool IsDatabaseExists(Connection connection, params string[] names)
    {
        if (names.Length == 0)
            return false;

        var existingDatabases = new HashSet<string>();
        foreach (var db in ShowDatabases(connection))
            existingDatabases.Add(db.Name!);

        return names.All(name => existingDatabases.Contains(name));
    }

    /// <summary>
    /// Asynchronously checks if specific databases exist on a given connection.
    /// </summary>
    /// <param name="connection">The connection to check.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <param name="names">The names of the databases to check.</param>
    /// <returns><c>true</c> if all specified databases exist; otherwise, <c>false</c>.</returns>
    public static async Task<bool> IsDatabaseExistsAsync(Connection connection, CancellationToken token,
        params string[] names)
    {
        if (names.Length == 0)
            return false;

        var existingDatabases = new HashSet<string>();
        await foreach (var db in ShowDatabasesAsync(connection, token))
        {
            if (db.Name != null)
                existingDatabases.Add(db.Name);
        }

        return names.All(name => existingDatabases.Contains(name));
    }

    /// <summary>
    /// Creates a database on a given connection.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="name">The name of the database to create.</param>
    /// <param name="checkIfNotExists">Indicates whether to check if the database does not exist before creation.</param>
    /// <returns>The number of affected rows.</returns>
    public static int CreateDatabase(Connection connection, string name, bool checkIfNotExists)
    {
        return connection.UpdateSelect($"CREATE DATABASE {(checkIfNotExists ? "IF NOT EXISTS" : "")} `{name}`");
    }

    /// <summary>
    /// Asynchronously creates a database on a given connection.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="name">The name of the database to create.</param>
    /// <param name="checkIfNotExists">Indicates whether to check if the database does not exist before creation.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns>The number of affected rows.</returns>
    public static async Task<int> CreateDatabaseAsync(Connection connection, string name, bool checkIfNotExists,
        CancellationToken token = default)
    {
        return await connection.UpdateSelectAsync(
            $"CREATE DATABASE {(checkIfNotExists ? "IF NOT EXISTS" : "")} `{name}`", token);
    }

    /// <summary>
    /// Drops a database on a given connection.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="name">The name of the database to drop.</param>
    /// <param name="checkIfExists">Indicates whether to check if the database exists before dropping it.</param>
    /// <returns>The number of affected rows.</returns>
    public static int DropDatabase(Connection connection, string name, bool checkIfExists = true)
    {
        return connection.UpdateSelect($"DROP DATABASE {(checkIfExists ? "IF EXISTS" : "")} `{name}`");
    }

    /// <summary>
    /// Asynchronously drops a database on a given connection.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="name">The name of the database to drop.</param>
    /// <param name="checkIfExists">Indicates whether to check if the database exists before dropping it.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns>The number of affected rows.</returns>
    public static async Task<int> DropDatabaseAsync(Connection connection, string name, bool checkIfExists,
        CancellationToken token)
    {
        return await connection.UpdateSelectAsync($"DROP DATABASE {(checkIfExists ? "IF EXISTS" : "")} `{name}`",
            token);
    }

    /// <summary>
    /// Opens an existing database or creates it if it doesn't exist.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="name">The name of the database.</param>
    /// <returns>The opened or created database.</returns>
    public static Database OpenOrCreate(Connection connection, string name)
    {
        var db = new Database(connection, name);

        if (db.IsDatabaseExists())
            return db;

        db.CreateDatabase(false);

        return db;
    }

    /// <summary>
    /// Asynchronously opens an existing database or creates it if it doesn't exist.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="name">The name of the database.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns>The opened or created database.</returns>
    public static async Task<Database> OpenOrCreateAsync(Connection connection, string name,
        CancellationToken token = default)
    {
        var db = new Database(connection, name);

        if (await db.IsDatabaseExistsAsync(token))
            return db;

        await db.CreateDatabaseAsync(false, token);

        return db;
    }

    #region OPS

    /// <summary>
    /// Implicitly converts a <see cref="Database"/> to a <see cref="Connection"/>.
    /// </summary>
    /// <param name="db">The database to convert.</param>
    /// <returns>The connection associated with the database.</returns>
    /// <exception cref="NullReferenceException">Thrown when the connection is in dispose state.</exception>
    public static implicit operator Connection(Database db)
    {
        if (ReferenceEquals(db._connection, null))
            throw new NullReferenceException("Connection is in dispose state");

        return db._connection;
    }

    #endregion
}