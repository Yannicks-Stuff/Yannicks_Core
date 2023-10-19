using System.Text;

namespace Yannick.Database.Mysql;

/// <summary>
/// Represents a table within a MySQL database.
/// </summary>
public sealed class Table
{
    private readonly Connection? _connection;

    /// <summary>
    /// The database associated with the table.
    /// </summary>
    public readonly Database? Database;

    private Table()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Table"/> class.
    /// </summary>
    /// <param name="database">The associated database.</param>
    /// <param name="name">The name of the table.</param>
    public Table(Database database, string name)
    {
        Database = database;
        _connection = database;
        _name = name;
    }

    private string? _name { get; set; }

    /// <summary>
    /// Gets or sets the name of the table.
    /// </summary>
    public string Name
    {
        get => _name ?? string.Empty;
        set => Rename(value);
    }

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

        await foreach (var t in Database.ShowTablesAsync(token))
            if (names.Any(e => e.Equals(t._name)))
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
            if (names.Any(e => e.Equals(t._name)))
                return true;

        return false;
    }

    #region ALTER

    #endregion

    #region INSERT_INTO

    /// <summary>
    /// Generates the command for inserting data into the table.
    /// </summary>
    /// <param name="db">The database.</param>
    /// <param name="tb">The table.</param>
    /// <param name="data">The data to insert.</param>
    /// <returns>The generated command string.</returns>
    private static string GenerateCommandForInsertInto(Database db, Table tb,
        params Dictionary<string, object>[] data)
    {
        var columns = string.Join(", ", data[0].Keys);
        var values = new StringBuilder();

        foreach (var row in data)
        {
            var rowValues = string.Join(", ", row.Values);
            values.Append($"({rowValues}),");
        }

        values.Length--;

        return $"INSERT INTO `{db.Name}`.`{tb.Name}` ({columns}) VALUES {values}";
    }

    /// <summary>
    /// Asynchronously inserts data into the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <param name="data">The data to insert.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> InsertIntoAsync(CancellationToken token, params Dictionary<string, object>[] data)
    {
        if (data.Length == 0 || _connection == null || _name == null || Database == null) return -1;

        return await _connection.UpdateSelectAsync(GenerateCommandForInsertInto(Database, this, data), token);
    }

    /// <summary>
    /// Inserts data into the table.
    /// </summary>
    /// <param name="data">The data to insert.</param>
    /// <returns>The number of affected rows.</returns>
    public int InsertInto(params Dictionary<string, object>[] data)
    {
        if (_connection == null || _name == null || data.Length == 0 || Database == null)
            return -1;

        return _connection.UpdateSelect(GenerateCommandForInsertInto(Database, this, data));
    }

    #endregion

    #region RENAME

    /// <summary>
    /// Asynchronously renames the table.
    /// </summary>
    /// <param name="to">The new name for the table.</param>
    /// <param name="useChecks">Indicates whether to perform checks before renaming.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>True if the renaming was successful; otherwise, false.</returns>
    private async Task<bool> RenameAsync(string to, bool useChecks = true, CancellationToken token = default)
    {
        if (_connection == null || _name == null || Database == null)
            return false;

        if (useChecks && await IsTableExistsAsync(token, to))
            return false;

        if (await _connection.UpdateSelectAsync(
                $"RENAME TABLE `{Database.Name}`.`{Name}` TO `{Database.Name}`.`{to}`;",
                token) == 0)
            return false;


        _name = to;
        return true;
    }

    /// <summary>
    /// Renames the table.
    /// </summary>
    /// <param name="name">The new name for the table.</param>
    /// <returns>True if the renaming was successful; otherwise, false.</returns>
    private bool Rename(string name)
    {
        var ra = RenameAsync(name, true, CancellationToken.None);
        ra.Wait();
        return ra.Result;
    }

    /// <summary>
    /// Renames the table async
    /// </summary>
    /// <param name="name">The new name for the table.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>True if the renaming was successful; otherwise, false.</returns>
    public async Task<bool> RenameAsync(string name, CancellationToken token = default)
    {
        return await RenameAsync(name, false, token);
    }

    /// <summary>
    /// Renames the table and ignore checks
    /// </summary>
    /// <param name="name">The new name for the table.</param>
    /// <returns>True if the renaming was successful; otherwise, false.</returns>
    public bool RenameIgnoreChecks(string name)
    {
        var ra = RenameAsync(name, false, CancellationToken.None);
        ra.Wait();
        return ra.Result;
    }

    /// <summary>
    /// Renames the table and ignore checks in async
    /// </summary>
    /// <param name="name">The new name for the table.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>True if the renaming was successful; otherwise, false.</returns>
    public async Task<bool> RenameAsyncIgnoreChecks(string name, CancellationToken token = default)
    {
        return await RenameAsync(name, true, token);
    }

    #endregion

    #region COPY

    /// <summary>
    /// Asynchronously copies the table.
    /// </summary>
    /// <param name="useChecks">Indicates whether to perform checks before copying.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>True if the copying was successful; otherwise, false.</returns>
    public async Task<bool> CopyAsync(bool useChecks = true, CancellationToken token = default)
    {
        if (_connection == null || _name == null || Database == null)
            return false;

        if (useChecks && !await Database.IsDatabaseExistsAsync(token))
            return false;

        return await _connection.UpdateSelectAsync(
                   $"CREATE TABLE  `{Database.Name}`.`{_name}` LIKE  `{Database.Name}`.`{_name}`;", token) > 0 &&
               await _connection.UpdateSelectAsync(
                   $"INSERT INTO `{Database.Name}`.`{_name} SELECT * FROM `{Database.Name}`.`{_name}`;", token) > 0;
    }

    /// <summary>
    /// Copies the table.
    /// </summary>
    /// <param name="useChecks">Indicates whether to perform checks before copying.</param>
    /// <returns>True if the copying was successful; otherwise, false.</returns>
    public bool Copy(bool useChecks = true)
    {
        if (_connection == null || _name == null || Database == null)
            return false;

        if (useChecks && !Database.IsDatabaseExists())
            return false;

        return _connection.UpdateSelect(
                   $"CREATE TABLE  `{Database.Name}`.`{_name}` LIKE  `{Database.Name}`.`{_name}`;") > 0 &&
               _connection.UpdateSelect(
                   $"INSERT INTO `{Database.Name}`.`{_name} SELECT * FROM `{Database.Name}`.`{_name}`;") > 0;
    }

    #endregion

    #region DROP

    /// <summary>
    /// Drops the table.
    /// </summary>
    /// <param name="checkIfExists">Indicates whether to check if the table exists before dropping.</param>
    /// <returns>The number of affected rows.</returns>
    public int Drop(bool checkIfExists = false)
    {
        if (_connection == null || _name == null || Database == null)
            return -1;

        var tb = new List<Table>();

        foreach (var t in Database.ShowTables())
            tb.Add(t);

        return checkIfExists switch
        {
            true when tb.Any(e =>
                !tb.Any(e2 => e2._name?.Equals(e._name, StringComparison.OrdinalIgnoreCase) ?? false)) => -1,
            true => _connection.UpdateSelect(
                $"DROP {_name} IF EXISTS {tb.Select(a => $"`{a.Database}`.`{a}`")
                    .Aggregate((a, b) => a + ", " + b)};"),
            _ => _connection.UpdateSelect($"DROP {_name} {tb.Select(a => $"`{a.Database}`.`{a}`")
                .Aggregate((a, b) => a + ", " + b)};")
        };
    }

    /// <summary>
    /// Asynchronously drops the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <param name="checkIfExists">Indicates whether to check if the table exists before dropping.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> DropAsync(CancellationToken token = default, bool checkIfExists = false)
    {
        if (_connection == null || _name == null || Database == null)
            return -1;

        var tb = new List<Table>();

        await foreach (var t in Database.ShowTablesAsync(token: token))
            tb.Add(t);

        return checkIfExists switch
        {
            true when tb.Any(e =>
                !tb.Any(e2 => e2._name?.Equals(e._name, StringComparison.OrdinalIgnoreCase) ?? false)) => -1,
            true => await _connection.UpdateSelectAsync(
                $"DROP {_name} IF EXISTS {tb.Select(a => $"`{a.Database}`.`{a}`")
                    .Aggregate((a, b) => a + ", " + b)};", token),
            _ => await _connection.UpdateSelectAsync($"DROP {_name} {tb.Select(a => $"`{a.Database}`.`{a}`")
                .Aggregate((a, b) => a + ", " + b)};", token)
        };
    }

    #endregion

    #region TRUNCATE

    /// <summary>
    /// Truncates the table.
    /// </summary>
    /// <returns>The number of affected rows.</returns>
    public int Truncate()
    {
        if (_connection == null || _name == null)
            return -1;

        return _connection.UpdateSelect($"TRUNCATE TABLE `{Database}`.`{this}`;");
    }

    /// <summary>
    /// Asynchronously truncates the table.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> TruncateAsync(CancellationToken token = default)
    {
        if (_connection == null || _name == null)
            return -1;

        return await _connection.UpdateSelectAsync($"TRUNCATE TABLE `{Database}`.`{this}`;", token);
    }

    #endregion

    #region OPS

    public static implicit operator Connection(Table t)
    {
        if (ReferenceEquals(t.Database, null))
            throw new NullReferenceException("Connection is in dispose mode");

        return t.Database;
    }

    public static implicit operator string(Table t)
    {
        return t.Name;
    }

    #endregion
}