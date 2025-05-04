using System.Text;

namespace Yannick.Database.Mysql;

/// <summary>
/// Represents a column within a MySQL database.
/// </summary>
public class Column
{
    private readonly Connection _connection;
    public readonly Database Database;
    public readonly Table Table;
    private int? _bitLength;
    private string? _comment;
    private DataTyp _DataTyp;
    private string? _defaultValue;
    private List<string>? _enumValues;
    private string? _extra;
    private bool _isAutoIncrement = false;
    private bool _isNullable = true;
    private bool _isPrimaryKey = false;
    private int? _length;

    private string _name;
    private int? _precision;
    private int? _scale;
    private List<string>? _setValues;

    /// <summary>
    /// Initializes a new instance of the <see cref="Column"/> class.
    /// </summary>
    /// <param name="table">The table that contains this column.</param>
    /// <param name="name">The name of the column.</param>
    public Column(Table table, string name)
    {
        if (table == null)
            throw new ArgumentNullException(nameof(table));
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Column name cannot be null or empty", nameof(name));

        Table = table;
        Database = table;
        _connection = table;
        _name = name;
    }

    /// <summary>
    /// Gets or sets the name of the column.
    /// </summary>
    public string Name
    {
        get => _name;
        set => _name = value; // You can call Update() explicitly if needed
    }

    /// <summary>
    /// Gets or sets the data type of the column.
    /// </summary>
    public DataTyp DataTyp
    {
        get => _DataTyp;
        set => _DataTyp = value;
    }

    public int? Length
    {
        get => _length;
        set => _length = value;
    }

    public int? Precision
    {
        get => _precision;
        set => _precision = value;
    }

    public int? Scale
    {
        get => _scale;
        set => _scale = value;
    }

    public bool IsNullable
    {
        get => _isNullable;
        set => _isNullable = value;
    }

    public bool IsPrimaryKey
    {
        get => _isPrimaryKey;
        set => _isPrimaryKey = value;
    }

    public bool IsAutoIncrement
    {
        get => _isAutoIncrement;
        set => _isAutoIncrement = value;
    }

    public string? DefaultValue
    {
        get => _defaultValue;
        set => _defaultValue = value;
    }

    public string? Extra
    {
        get => _extra;
        set => _extra = value;
    }

    public string? Comment
    {
        get => _comment;
        set => _comment = value;
    }

    public List<string>? EnumValues
    {
        get => _enumValues;
        set => _enumValues = value;
    }

    public List<string>? SetValues
    {
        get => _setValues;
        set => _setValues = value;
    }

    public int? BitLength
    {
        get => _bitLength;
        set => _bitLength = value;
    }

    /// <summary>
    /// Loads the column's metadata from the database.
    /// </summary>
    public void Load()
    {
        var row = _connection.Select($"""
                                      
                                                      SELECT *
                                                      FROM information_schema.COLUMNS
                                                      WHERE TABLE_SCHEMA = '{Database.Name}'
                                                          AND TABLE_NAME = '{Table.Name}'
                                                          AND COLUMN_NAME = '{Name}';
                                                  
                                      """).FirstOrDefault();

        if (row == null)
            return;

        DataTyp = ParseDataTyp(row["DATA_TYPE"]?.ToString());
        Length = row["CHARACTER_MAXIMUM_LENGTH"] as int?;
        Precision = row["NUMERIC_PRECISION"] as int?;
        Scale = row["NUMERIC_SCALE"] as int?;
        IsNullable = row["IS_NULLABLE"]?.ToString() == "YES";
        DefaultValue = row["COLUMN_DEFAULT"]?.ToString();
        Comment = row["COLUMN_COMMENT"]?.ToString();
        Extra = row["EXTRA"]?.ToString();
    }

    /// <summary>
    /// Removes the column from the database.
    /// </summary>
    public bool Remove() => _connection.UpdateSelect($"ALTER TABLE `{Table.Name}` DROP COLUMN `{Name}`") > 0;

    /// <summary>
    /// Creates the column in the database.
    /// </summary>
    public bool Create() =>
        _connection.UpdateSelect($"ALTER TABLE `{Table.Name}` ADD COLUMN {GetColumnDefinition()}") > 0;

    /// <summary>
    /// Loads a column from the specified table.
    /// </summary>
    /// <param name="table">The table from which to load the column.</param>
    /// <param name="columnName">The name of the column to load.</param>
    /// <returns>The loaded column.</returns>
    public static Column Load(Table table, string columnName)
    {
        var column = new Column(table, columnName);
        column.Load();
        return column;
    }

    /// <summary>
    /// Updates the column in the database.
    /// </summary>
    public bool Update() =>
        _connection.UpdateSelect($"ALTER TABLE `{Table.Name}` MODIFY COLUMN {GetColumnDefinition()}") > 0;

    private string GetColumnDefinition()
    {
        var definition = new StringBuilder();
        definition.Append($"`{Name}` {GetSqlDataTyp()}");

        if (!IsNullable)
        {
            definition.Append(" NOT NULL");
        }

        if (DefaultValue != null)
        {
            definition.Append($" DEFAULT '{DefaultValue}'");
        }

        if (!string.IsNullOrEmpty(Comment))
        {
            definition.Append($" COMMENT '{Comment}'");
        }

        if (IsAutoIncrement)
        {
            definition.Append(" AUTO_INCREMENT");
        }

        return definition.ToString();
    }

    private string GetSqlDataTyp()
    {
        var sqlType = DataTyp.ToString();

        switch (DataTyp)
        {
            case DataTyp.VARCHAR:
            case DataTyp.CHAR:
                sqlType += $"({Length ?? 255})";
                break;
            case DataTyp.DECIMAL:
            case DataTyp.NUMERIC:
                sqlType += $"({Precision ?? 10},{Scale ?? 0})";
                break;
            case DataTyp.ENUM:
            case DataTyp.SET:
                var values = (DataTyp == DataTyp.ENUM ? EnumValues : SetValues) ?? new List<string>();
                sqlType += $"('{string.Join("','", values)}')";
                break;
            case DataTyp.BIT:
                sqlType += $"({BitLength ?? 1})";
                break;
            default:
                if (Length.HasValue)
                {
                    sqlType += $"({Length})";
                }

                break;
        }

        return sqlType;
    }

    private static DataTyp ParseDataTyp(string? DataTyp)
    {
        if (string.IsNullOrEmpty(DataTyp))
            throw new ArgumentException("Data type cannot be null or empty.");

        if (Enum.TryParse(DataTyp, true, out DataTyp result))
        {
            return result;
        }
        else
        {
            throw new ArgumentException($"Unknown data type: {DataTyp}");
        }
    }
}