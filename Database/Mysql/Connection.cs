using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using Yannick.Network;

namespace Yannick.Database.Mysql;

/// <summary>
/// Represents a connection to a MySQL database.
/// </summary>
public sealed class Connection : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the configuration settings for the MySQL connection.
    /// </summary>
    public readonly MySqlConnectionStringBuilder Configuration;

    private MySqlConnection? _connection;
    private SemaphoreSlim? _semaphoreSlim = new(1, 1);

    /// <summary>
    /// Initializes a new instance of the <see cref="Connection"/> class with default configuration.
    /// </summary>
    public Connection()
    {
        Configuration = new MySqlConnectionStringBuilder
            { Server = "127.0.0.1", Port = 3306 };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Connection"/> class with the specified host and port.
    /// </summary>
    /// <param name="host">The IP address of the MySQL server.</param>
    /// <param name="port">The port number of the MySQL server.</param>
    public Connection(IPAddress? host = null, ushort port = 3306)
    {
        Configuration = new MySqlConnectionStringBuilder
            { Server = (host ?? IPAddress.Parse("127.0.0.1")).ToString(), Port = port };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Connection"/> class with the specified domain and port.
    /// </summary>
    /// <param name="domain">The domain of the MySQL server.</param>
    /// <param name="port">The port number of the MySQL server.</param>
    public Connection(string domain, ushort port = 3306)
    {
        Configuration = new MySqlConnectionStringBuilder
            { Server = domain, Port = port };
    }


    /// <summary>
    /// Asynchronously releases all resources used by the <see cref="Connection"/> object.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        if (_connection != null) await _connection.DisposeAsync();
        _semaphoreSlim?.Dispose();

        _connection = null;
        _semaphoreSlim = null;
        _semaphoreSlim = new SemaphoreSlim(1, 1);
    }

    /// <summary>
    /// Releases all resources used by the <see cref="Connection"/> object.
    /// </summary>
    public void Dispose()
    {
        _connection?.Dispose();
        _connection = null;
        _semaphoreSlim?.Dispose();
        _semaphoreSlim = null;
        _semaphoreSlim = new SemaphoreSlim(1, 1);
    }

    /// <summary>
    /// Authenticates with the MySQL server using the provided user credentials.
    /// </summary>
    /// <param name="usr">The username.</param>
    /// <param name="pw">The password.</param>
    /// <returns>True if authentication is successful, otherwise false.</returns>
    public bool Authenticate(string usr, string pw)
        => Authenticate(usr, pw, out _);

    /// <summary>
    /// Authenticates with the MySQL server using the provided user credentials.
    /// </summary>
    /// <param name="usr">The username.</param>
    /// <param name="pw">The password.</param>
    /// <param name="exception">Output parameter that returns an exception if one occurs during authentication.</param>
    /// <returns>True if authentication is successful, otherwise false.</returns>
    public bool Authenticate(string usr, string pw, out Exception? exception)
    {
        Configuration.UserID = usr;
        Configuration.Password = pw;
        Configuration.SslMode = MySqlSslMode.Preferred;

        _connection?.Dispose();
        _connection = new MySqlConnection(Configuration.ConnectionString);

        try
        {
            _connection.Open();
        }
        catch (Exception e)
        {
            exception = e;
            return false;
        }

        exception = null;
        return true;
    }

    /// <summary>
    /// Asynchronously authenticates with the MySQL server using the provided user credentials.
    /// </summary>
    /// <param name="usr">The username.</param>
    /// <param name="pw">The password.</param>
    /// <returns>True if authentication is successful, otherwise false.</returns>
    public async Task<bool> AuthenticateAsync(string usr, string pw)
    {
        var (state, _) = await AuthenticateAsyncWithOptionalException(usr, pw);
        return state;
    }

    /// <summary>
    /// Asynchronously authenticates with the MySQL server using the provided user credentials and returns any exceptions.
    /// </summary>
    /// <param name="usr">The username.</param>
    /// <param name="pw">The password.</param>
    /// <returns>A tuple containing a boolean indicating the authentication result and an optional exception.</returns>
    public async Task<(bool, Exception?)> AuthenticateAsyncWithOptionalException(string usr, string pw)
    {
        Configuration.UserID = usr;
        Configuration.Password = pw;
        Configuration.SslMode = MySqlSslMode.Preferred;

        if (_connection != null)
            await _connection.DisposeAsync();

        _connection = new MySqlConnection(Configuration.ConnectionString);

        try
        {
            await _connection.OpenAsync();
        }
        catch (Exception e)
        {
            return (false, e);
        }

        return (true, null);
    }

    /// <summary>
    /// Executes an SQL command and returns the number of affected rows.
    /// </summary>
    /// <param name="command">The SQL command to execute.</param>
    /// <returns>The number of affected rows.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int UpdateSelect(string command)
    {
        using var c = new MySqlCommand(command, _connection);
        return c.ExecuteNonQuery();
    }

    /// <summary>
    /// Asynchronously executes an SQL command and returns the number of affected rows.
    /// </summary>
    /// <param name="command">The SQL command to execute.</param>
    /// <param name="token">A token to monitor for cancellation requests.</param>
    /// <returns>The number of affected rows.</returns>
    public async Task<int> UpdateSelectAsync(string command, CancellationToken token = default)
    {
        await _semaphoreSlim!.WaitAsync(token);
        try
        {
            await using var c = new MySqlCommand(command, _connection);
            return await c.ExecuteNonQueryAsync(token);
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    /// <summary>
    /// Retrieves rows from the database as key-value pairs for a given SQL command.
    /// </summary>
    /// <param name="command">The SQL command to execute.</param>
    /// <returns>An enumerable of key-value pairs representing the rows.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<KeyValuePair<string, object?>> SelectRows(string command)
    {
        using var c = new MySqlCommand(command, _connection);
        using var r = c.ExecuteReader();

        if (r == null)
            yield break;

        while (r.Read())
            for (var i = 0; i < r.FieldCount; ++i)
                yield return new KeyValuePair<string, object?>(r.GetName(i),
                    r.IsDBNull(i) ? null : r.GetValue(i));
    }

    /// <summary>
    /// Asynchronously retrieves rows from the database as key-value pairs for a given SQL command.
    /// </summary>
    /// <param name="command">The SQL command to execute.</param>
    /// <param name="token">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous enumerable of key-value pairs representing the rows.</returns>
    public async IAsyncEnumerable<KeyValuePair<string, object?>> SelectRowsAsync(string command,
        [EnumeratorCancellation] CancellationToken token = default)
    {
        await _semaphoreSlim!.WaitAsync(token);
        try
        {
            await using var c = new MySqlCommand(command, _connection);
            {
                await using var r = await c.ExecuteReaderAsync(token);

                if (ReferenceEquals(r, null))
                    yield break;

                while (await r.ReadAsync(token))
                    for (var i = 0; i < r.FieldCount; ++i)
                        yield return new KeyValuePair<string, object?>(r.GetName(i),
                            await r.IsDBNullAsync(i, token) ? null : r.GetValue(i));
            }
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    /// <summary>
    /// Retrieves columns from the database as dictionaries for a given SQL command.
    /// </summary>
    /// <param name="command">The SQL command to execute.</param>
    /// <returns>An enumerable of dictionaries representing the columns.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<IReadOnlyDictionary<string, object?>> SelectColumns(string command)
    {
        using var c = new MySqlCommand(command, _connection);
        using var r = c.ExecuteReader();

        if (r == null)
            yield break;

        while (r.Read())
        {
            var dic = new Dictionary<string, object?>();
            for (var i = 0; i < r.FieldCount; ++i)
                dic.Add(r.GetName(i), r.IsDBNull(i) ? null : r.GetValue(i));

            yield return new Dictionary<string, object?>(dic);
        }
    }

    /// <summary>
    /// Asynchronously retrieves columns from the database as dictionaries for a given SQL command.
    /// </summary>
    /// <param name="command">The SQL command to execute.</param>
    /// <param name="token">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous enumerable of dictionaries representing the columns.</returns>
    public async IAsyncEnumerable<IReadOnlyDictionary<string, object?>> SelectColumnsAsync(string command,
        [EnumeratorCancellation] CancellationToken token = default)
    {
        await _semaphoreSlim!.WaitAsync(token);
        try
        {
            await using var c = new MySqlCommand(command, _connection);
            {
                await using var r = await c.ExecuteReaderAsync(token);

                if (ReferenceEquals(r, null))
                    yield break;

                while (await r.ReadAsync(token))
                {
                    var dic = new Dictionary<string, object?>();
                    for (var i = 0; i < r.FieldCount; ++i)
                        dic.Add(r.GetName(i), r.IsDBNull(i) ? null : r.GetValue(i));

                    yield return dic;
                }
            }
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    /// <summary>
    /// Retrieves data from the database as dictionaries for a given SQL command.
    /// </summary>
    /// <param name="command">The SQL command to execute.</param>
    /// <returns>An enumerable of dictionaries representing the data.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<IReadOnlyDictionary<string, object?>> Select(string command)
    {
        using var c = new MySqlCommand(command, _connection);
        using var r = c.ExecuteReader();

        if (r == null)
            yield break;

        var dic = new Dictionary<string, object?>();
        while (r.Read())
        {
            dic.Clear();
            for (var i = 0; i < r.FieldCount; ++i)
                dic.Add(r.GetName(i), r.IsDBNull(i) ? null : r.GetValue(i));

            yield return dic;
        }
    }

    /// <summary>
    /// Asynchronously retrieves data from the database as dictionaries for a given SQL command.
    /// </summary>
    /// <param name="command">The SQL command to execute.</param>
    /// <param name="token">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous enumerable of dictionaries representing the data.</returns>
    public async IAsyncEnumerable<IReadOnlyDictionary<string, object?>> SelectAsync(string command,
        [EnumeratorCancellation] CancellationToken token = default)
    {
        await _semaphoreSlim!.WaitAsync(token);

        try
        {
            await using var c = new MySqlCommand(command, _connection);
            {
                await using var r = await c.ExecuteReaderAsync(token);

                if (ReferenceEquals(r, null))
                    yield break;

                {
                    while (await r.ReadAsync(token))
                    {
                        var dic = new Dictionary<string, object?>();

                        for (var i = 0; i < r.FieldCount; ++i)
                            dic.Add(r.GetName(i),
                                await r.IsDBNullAsync(i, token) ? null : r.GetValue(i));

                        yield return new Dictionary<string, object?>(dic);
                    }
                }
            }
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    /// <summary>
    /// Opens or creates a new database.
    /// </summary>
    /// <param name="db">The name of the database.</param>
    /// <returns>An instance of the <see cref="Database"/> class.</returns>
    public Database OpenDatabaseOrCreate(string db)
        => Database.OpenOrCreate(this, db);

    /// <summary>
    /// Asynchronously opens or creates a new database.
    /// </summary>
    /// <param name="db">The name of the database.</param>
    /// <param name="token">A token to monitor for cancellation requests.</param>
    /// <returns>An instance of the <see cref="Database"/> class.</returns>
    public async Task<Database> OpenDatabaseOrCreateAsync(string db, CancellationToken token = default)
        => await Database.OpenOrCreateAsync(this, db, token);
}