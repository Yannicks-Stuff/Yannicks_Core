using System.Runtime.CompilerServices;

namespace Yannick.Database.Mysql;

/// <summary>
/// Represents a MySQL database, providing methods to interact and manage it.
/// </summary>
public sealed class Database : IDisposable, IAsyncDisposable
{
    private readonly Connection? _connection;

    public readonly string? Name;

    private Database()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="name">The name of the database.</param>
    public Database(Connection connection, string name)
    {
        _connection = connection;
        Name = name;
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
    /// <param name="checkIfNotExists">Indicates whether to check if the database exists before dropping it.</param>
    /// <returns>The number of affected rows.</returns>
    public int DropDatabase(bool checkIfNotExists)
    {
        return _connection == null || Name == null ? -1 : DropDatabase(_connection, Name, checkIfNotExists);
    }

    /// <summary>
    /// Asynchronously drops the database.
    /// </summary>
    /// <param name="checkIfNotExists">Indicates whether to check if the database exists before dropping it.</param>
    /// <param name="token">The cancellation token to use.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> DropDatabaseAsync(bool checkIfNotExists, CancellationToken token = default)
    {
        return _connection == null || Name == null
            ? -1
            : await DropDatabaseAsync(_connection, Name, checkIfNotExists, token);
    }

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

        foreach (var db in ShowDatabases(connection))
            if (!names.Any(e => e.Equals(db.Name)))
                return false;

        return true;
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

        await foreach (var db in ShowDatabasesAsync(connection, token))
            if (!names.Any(e => e.Equals(db.Name)))
                return false;

        return true;
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
    public static implicit operator Connection(Database db)
    {
        if (ReferenceEquals(db._connection, null))
            throw new NullReferenceException("Connection is in dispose state");

        return db._connection;
    }

    #endregion
}