using System.Runtime.CompilerServices;
using System.Text;
using MySql.Data.MySqlClient;

namespace Yannick.Database.Mysql;

internal static class MysqlDatabaseReindexer
{
    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message, Exception? ex = null);
    }

    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
        } //=> Console.WriteLine("[INFO] " + message);

        public void Warn(string message)
        {
        } //=> Console.WriteLine("[WARN] " + message);

        public void Error(string message, Exception? ex = null)
        {
            Console.WriteLine("[ERROR] " + message);
            if (ex != null) Console.WriteLine(ex.ToString());
        }
    }

    public class DatabaseReindexer
    {
        private readonly ILogger _logger;
        private readonly string _primaryKeyColumnName;
        private readonly MySqlConnection connection;

        // Maps tableName -> (old_id -> new_id)
        private Dictionary<string, Dictionary<int, int>> _idMappings;

        // referencesFrom[table] = list of FKs that 'table' references (parents)
        private Dictionary<string, List<ForeignKeyInfo>>? _referencesFrom;

        // referencesTo[table] = list of FKs where 'table' is referenced (children)
        private Dictionary<string, List<ForeignKeyInfo>>? _referencesTo;

        public DatabaseReindexer(MySqlConnection connection, string primaryKeyColumnName = "id", ILogger? logger = null)
        {
            this.connection = connection;
            _primaryKeyColumnName = primaryKeyColumnName;
            _logger = logger ?? new ConsoleLogger();
            _idMappings = new Dictionary<string, Dictionary<int, int>>();
        }

        public static void Start(Table connection, string primaryKeyColumnName = "id")
        {
            connection.Database.Use();

            new DatabaseReindexer((Connection)connection, primaryKeyColumnName).NormalizeIndexesFromTable(connection
                .Name);
        }

        /// <summary>
        /// Normalize indexes starting from the specified start table.
        /// This will process only the subgraph of tables connected to startTable.
        /// </summary>
        public void NormalizeIndexesFromTable(string startTable)
        {
            _logger.Info("Starting normalization process...");

            using var transaction = connection.BeginTransaction();
            try
            {
                // Get all tables
                _logger.Info("Retrieving all tables...");
                var allTables = GetAllTables(connection, transaction);

                if (!allTables.Contains(startTable))
                {
                    throw new Exception($"Start table '{startTable}' does not exist in the database.");
                }

                // Get all foreign keys
                _logger.Info("Retrieving foreign key information...");
                var allForeignKeys = GetAllForeignKeys(connection, transaction, allTables);

                // Build reference dictionaries
                BuildReferenceDictionaries(allTables, allForeignKeys);

                // Determine the subgraph of tables that need to be processed
                _logger.Info($"Determining the subgraph of tables related to '{startTable}'...");
                var subgraphTables = GetSubgraphTables(startTable);

                _logger.Info($"Subgraph contains {subgraphTables.Count} tables.");

                // Topologically sort only the subgraph
                _logger.Info("Performing topological sort on the subgraph...");
                var orderedTables = TopologicalSortTables(subgraphTables, _referencesTo);

                if (!orderedTables.Contains(startTable))
                {
                    throw new Exception(
                        $"The start table '{startTable}' could not be found in the topological order. Check your foreign key setup.");
                }

                // Disable foreign key checks
                _logger.Info("Disabling foreign key checks...");
                ExecuteNonQuery(connection, transaction, "SET FOREIGN_KEY_CHECKS=0;");

                // Process tables in topological order
                foreach (var table in orderedTables)
                {
                    _logger.Info($"Processing table '{table}'...");

                    // Update foreign keys referencing previously reindexed tables
                    UpdateTableForeignKeys(connection, transaction, table);

                    // Reindex the primary keys of this table
                    ReindexTablePrimaryKey(connection, transaction, table);
                }

                // Re-enable foreign key checks
                _logger.Info("Re-enabling foreign key checks...");
                ExecuteNonQuery(connection, transaction, "SET FOREIGN_KEY_CHECKS=1;");

                // Commit transaction
                transaction.Commit();
                _logger.Info("Normalization process completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error during reindexing operation. Rolling back changes.", ex);
                transaction.Rollback();
                throw;
            }
        }

        private List<string> GetAllTables(MySqlConnection connection, MySqlTransaction transaction)
        {
            var tables = new List<string>();
            using var cmd = new MySqlCommand("SHOW TABLES;", connection, transaction);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tables.Add(reader.GetString(0));
            }

            return tables;
        }

        private List<ForeignKeyInfo> GetAllForeignKeys(MySqlConnection connection, MySqlTransaction transaction,
            List<string> tables)
        {
            var fks = new List<ForeignKeyInfo>();

            const string query = """
                                 
                                             SELECT 
                                                 KCU.TABLE_NAME AS ReferencingTable,
                                                 KCU.COLUMN_NAME AS ReferencingColumn,
                                                 KCU.REFERENCED_TABLE_NAME AS ReferencedTable,
                                                 KCU.REFERENCED_COLUMN_NAME AS ReferencedColumn,
                                                 RC.UPDATE_RULE,
                                                 RC.DELETE_RULE
                                             FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU
                                             JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC 
                                                 ON KCU.CONSTRAINT_NAME = RC.CONSTRAINT_NAME
                                                 AND KCU.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA
                                             WHERE KCU.REFERENCED_TABLE_NAME IS NOT NULL
                                                   AND KCU.TABLE_SCHEMA = DATABASE();
                                 """;

            using var cmd = new MySqlCommand(query, connection, transaction);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var fk = new ForeignKeyInfo
                {
                    ReferencingTable = reader.GetString("ReferencingTable"),
                    ReferencingColumn = reader.GetString("ReferencingColumn"),
                    ReferencedTable = reader.GetString("ReferencedTable"),
                    ReferencedColumn = reader.GetString("ReferencedColumn"),
                    UpdateRule = reader.GetString("UPDATE_RULE"),
                    DeleteRule = reader.GetString("DELETE_RULE")
                };
                fks.Add(fk);
            }

            return fks;
        }

        private void BuildReferenceDictionaries(List<string> tables, List<ForeignKeyInfo> allFks)
        {
            _referencesTo = new Dictionary<string, List<ForeignKeyInfo>>();
            _referencesFrom = new Dictionary<string, List<ForeignKeyInfo>>();

            foreach (var t in tables)
            {
                _referencesTo[t] = new List<ForeignKeyInfo>();
                _referencesFrom[t] = new List<ForeignKeyInfo>();
            }

            foreach (var fk in allFks)
            {
                // fk.ReferencedTable is the parent, fk.ReferencingTable is the child
                _referencesTo[fk.ReferencedTable].Add(fk);
                _referencesFrom[fk.ReferencingTable].Add(fk);
            }
        }

        /// <summary>
        /// Get the subgraph of tables connected to the startTable.
        /// This includes ancestors (tables that startTable depends on) and descendants (tables that depend on startTable).
        /// We do a bidirectional search: upwards (via referencesFrom to find parents) and downwards (via referencesTo to find children).
        /// </summary>
        private HashSet<string> GetSubgraphTables(string startTable)
        {
            var visited = new HashSet<string>();
            var stack = new Stack<string>();
            stack.Push(startTable);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (!visited.Add(current))
                    continue;

                // Add parents (tables that current references)
                foreach (var fk in _referencesFrom[current])
                {
                    if (!visited.Contains(fk.ReferencedTable))
                        stack.Push(fk.ReferencedTable);
                }

                // Add children (tables that reference current)
                foreach (var fk in _referencesTo[current])
                {
                    if (!visited.Contains(fk.ReferencingTable))
                        stack.Push(fk.ReferencingTable);
                }
            }

            return visited;
        }

        private List<string> TopologicalSortTables(HashSet<string> tables,
            Dictionary<string, List<ForeignKeyInfo>>? referencesTo)
        {
            // Count in-degree only for the subgraph
            var inDegree = new Dictionary<string, int>();
            foreach (var t in tables) inDegree[t] = 0;

            // Build in-degree for subgraph
            foreach (var table in tables)
            {
                foreach (var fk in referencesTo[table])
                {
                    if (tables.Contains(fk.ReferencingTable))
                    {
                        // Child depends on parent
                        inDegree[fk.ReferencingTable]++;
                    }
                }
            }

            var queue = new Queue<string>(inDegree.Where(x => x.Value == 0).Select(x => x.Key));
            var result = new List<string>();

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var fk in _referencesTo[current].Where(fk => tables.Contains(fk.ReferencingTable)))
                {
                    inDegree[fk.ReferencingTable]--;
                    if (inDegree[fk.ReferencingTable] == 0)
                        queue.Enqueue(fk.ReferencingTable);
                }
            }

            // If not all tables are processed, there's a cycle in the subgraph
            if (result.Count != tables.Count)
            {
                throw new Exception(
                    "Cycle detected in foreign key relationships within the subgraph. Cannot topologically sort.");
            }

            return result;
        }

        private void UpdateTableForeignKeys(MySqlConnection connection, MySqlTransaction transaction, string tableName)
        {
            // Update foreign keys in 'tableName' that reference reindexed tables
            foreach (var fk in _referencesFrom[tableName])
            {
                var parentTable = fk.ReferencedTable;

                // If parent table was reindexed, we have a mapping for it
                if (!_idMappings.TryGetValue(parentTable, out var mapping))
                    continue;

                // Use a temporary MEMORY table for mapping
                ExecuteNonQuery(connection, transaction, $@"
                    CREATE TEMPORARY TABLE `{parentTable}_mapping` (
                        old_id INT, 
                        new_id INT,
                        INDEX(old_id)
                    ) ENGINE=MEMORY;
                ");

                // Bulk insert mappings
                BulkInsertMappings(connection, transaction, $"{parentTable}_mapping", mapping);

                // Update the referencing table
                ExecuteNonQuery(connection, transaction, $@"
                    UPDATE `{tableName}` t
                    JOIN `{parentTable}_mapping` m ON t.`{fk.ReferencingColumn}` = m.old_id
                    SET t.`{fk.ReferencingColumn}` = m.new_id;
                ");

                // Drop the temp mapping table
                ExecuteNonQuery(connection, transaction, $"DROP TABLE `{parentTable}_mapping`;");
            }
        }

        private void ReindexTablePrimaryKey(MySqlConnection connection, MySqlTransaction transaction, string tableName)
        {
            // Gather old IDs
            var oldIds = new List<int>();
            using (var cmd = new MySqlCommand(
                       $"SELECT `{_primaryKeyColumnName}` FROM `{tableName}` ORDER BY `{_primaryKeyColumnName}`;",
                       connection, transaction))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    oldIds.Add(reader.GetInt32(0));
                }
            }

            var mapping = new Dictionary<int, int>();
            var newId = 1;
            foreach (var oldId in oldIds)
            {
                mapping[oldId] = newId++;
            }

            if (oldIds.Count == 0)
            {
                // If empty table, just reset auto increment and return
                ExecuteNonQuery(connection, transaction, $"""
                                                          
                                                                          ALTER TABLE `{tableName}` AUTO_INCREMENT = 1;
                                                                      
                                                          """);
                _idMappings[tableName] = mapping;
                return;
            }

            // Use a temp table to apply the mapping
            ExecuteNonQuery(connection, transaction, $@"
            CREATE TEMPORARY TABLE `{tableName}_old_new` (
                old_id INT PRIMARY KEY,
                new_id INT
            ) ENGINE=MEMORY;
        ");

            // Bulk insert mappings
            BulkInsertMappings(connection, transaction, $"{tableName}_old_new", mapping);

            // Update IDs
            ExecuteNonQuery(connection, transaction, $@"
            UPDATE `{tableName}` t
            JOIN `{tableName}_old_new` m ON t.`{_primaryKeyColumnName}` = m.old_id
            SET t.`{_primaryKeyColumnName}` = m.new_id;
        ");

            // Reset AUTO_INCREMENT
            int nextId;
            using (var cmd = new MySqlCommand($"SELECT IFNULL(MAX(`{_primaryKeyColumnName}`),0) + 1 FROM `{tableName}`",
                       connection, transaction))
            {
                nextId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            ExecuteNonQuery(connection, transaction, $"""
                                                          ALTER TABLE `{tableName}` 
                                                          AUTO_INCREMENT = {nextId};
                                                      """);


            // Drop temp table
            ExecuteNonQuery(connection, transaction, $"DROP TABLE `{tableName}_old_new`;");

            // Store mapping
            _idMappings[tableName] = mapping;
        }

        /// <summary>
        /// Performs a bulk insert of the integer mappings into the specified temporary table.
        /// </summary>
        private void BulkInsertMappings(MySqlConnection connection, MySqlTransaction transaction, string tempTableName,
            Dictionary<int, int> mapping)
        {
            if (mapping.Count == 0)
                return;

            // We'll create one large INSERT statement with multiple value tuples
            // For huge mapping sets, consider chunking.
            var sb = new StringBuilder();
            sb.Append($"INSERT INTO `{tempTableName}` (old_id, new_id) VALUES ");

            var first = true;
            foreach (var kvp in mapping)
            {
                if (!first)
                    sb.Append(',');
                sb.Append($"({kvp.Key},{kvp.Value})");
                first = false;
            }

            sb.Append(';');

            ExecuteNonQuery(connection, transaction, sb.ToString());
        }

        private void ExecuteNonQuery(MySqlConnection connection, MySqlTransaction transaction, string sql)
        {
            using var cmd = new MySqlCommand(sql, connection, transaction);
            cmd.ExecuteNonQuery();
        }
    }

    public class ForeignKeyInfo
    {
        public string ReferencingTable { get; set; }
        public string ReferencingColumn { get; set; }
        public string ReferencedTable { get; set; }
        public string ReferencedColumn { get; set; }
        public string UpdateRule { get; set; }
        public string DeleteRule { get; set; }
    }
}