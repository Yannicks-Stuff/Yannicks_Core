using System.Collections;
using System.Numerics;

namespace Yannick.Database.Mysql;

public enum DataTyp
{
    BIT,
    TINYINT,
    SMALLINT,
    MEDIUMINT,
    INT,
    INTEGER,
    BIGINT,
    FLOAT,
    DOUBLE,
    REAL,
    DECIMAL,
    DEC,
    NUMERIC,
    DATE,
    DATETIME,
    TIMESTAMP,
    TIME,
    YEAR,
    CHAR,
    VARCHAR,
    BINARY,
    VARBINARY,
    BLOB,
    MEDIUMBLOB,
    LONGBLOB,
    MEDIUMTEXT,
    LONGTEXT,
    TEXT,
    ENUM,
    SET
}

public static class DataTypE
{
    /// <summary>
    /// Determines if the provided object is valid for the specified MySQL data type.
    /// </summary>
    /// <param name="typ">The MySQL data type to check against.</param>
    /// <param name="o">The object to validate.</param>
    /// <returns>true if the object is valid for the data type; otherwise, false.</returns>
    public static bool IsValid(this DataTyp typ, object o)
    {
        switch (typ)
        {
            case DataTyp.BIT:
                return byte.TryParse(o.ToString(), out _);
            case DataTyp.TINYINT:
                return byte.TryParse(o.ToString(), out _) || sbyte.TryParse(o.ToString(), out _);
            case DataTyp.SMALLINT:
                return ushort.TryParse(o.ToString(), out _) || short.TryParse(o.ToString(), out _);
            case DataTyp.MEDIUMINT:
                return int.TryParse(o.ToString(), out var s) && s is >= -8388608 and <= 8388607 ||
                       s is >= 0 and <= 16777215;
            case DataTyp.INT:
            case DataTyp.INTEGER:
                return int.TryParse(o.ToString(), out _) || uint.TryParse(o.ToString(), out _);
            case DataTyp.BIGINT:
                return BigInteger.TryParse(o.ToString(), out _);
            case DataTyp.FLOAT:
                return float.TryParse(o.ToString(), out _);
            case DataTyp.REAL:
            case DataTyp.DOUBLE:
                return float.TryParse(o.ToString(), out _);
            case DataTyp.DECIMAL:
            case DataTyp.DEC:
            case DataTyp.NUMERIC:
                return decimal.TryParse(o.ToString(), out _);
            case DataTyp.DATE:
            case DataTyp.DATETIME:
            case DataTyp.TIMESTAMP:
                return DateTime.TryParse(o.ToString(), out _);
            case DataTyp.TIME:
                return TimeSpan.TryParse(o.ToString(), out _);
            case DataTyp.YEAR:
                return int.TryParse(o.ToString(), out var year) && year is >= 1901 and <= 2155;
            case DataTyp.CHAR:
            case DataTyp.VARCHAR:
            case DataTyp.MEDIUMTEXT:
            case DataTyp.LONGTEXT:
            case DataTyp.TEXT:
                return o is string;
            case DataTyp.BINARY:
            case DataTyp.VARBINARY:
            case DataTyp.BLOB:
            case DataTyp.MEDIUMBLOB:
            case DataTyp.LONGBLOB:
                return o is byte[] or IList<byte>;
            case DataTyp.ENUM:
            case DataTyp.SET:
                return o is string { Length: > 0 } or IList or Array;
            default:
                throw new ArgumentOutOfRangeException(nameof(typ), typ, null);
        }
    }

    /// <summary>
    /// Determines if the provided size (and optionally decimal precision) is valid for the specified MySQL data type.
    /// </summary>
    /// <param name="typ">The MySQL data type to check against.</param>
    /// <param name="size">The size to validate.</param>
    /// <param name="d">The optional decimal precision to validate. Relevant for decimal types.</param>
    /// <returns>true if the size (and decimal precision) is valid for the data type; otherwise, false.</returns>
    public static bool IsValidSize(this DataTyp typ, ulong size, int? d = null)
    {
        switch (typ)
        {
            case DataTyp.BIT:
                return size is >= 1 and <= 64;
            case DataTyp.TINYINT:
                return size == 4;
            case DataTyp.SMALLINT:
                return size == 6;
            case DataTyp.MEDIUMINT:
                return size == 9;
            case DataTyp.INT:
            case DataTyp.INTEGER:
                return size == 11;
            case DataTyp.BIGINT:
                return size == 20;
            case DataTyp.FLOAT:
            case DataTyp.REAL:
                if (d.HasValue)
                {
                    return size is >= 1 and <= 23 && d.Value is >= 0 and <= 23;
                }

                return size is >= 1 and <= 23;
            case DataTyp.DOUBLE:
                if (d.HasValue)
                {
                    return size is >= 1 and <= 53 && d.Value is >= 0 and <= 53;
                }

                return size is >= 1 and <= 53;
            case DataTyp.DECIMAL:
            case DataTyp.DEC:
            case DataTyp.NUMERIC:
                if (d.HasValue)
                {
                    return size is >= 1 and <= 65 && d.Value is >= 0 and <= 30;
                }

                return size is >= 1 and <= 65;
            case DataTyp.CHAR:
            case DataTyp.BINARY:
                return size <= 255;
            case DataTyp.VARCHAR:
            case DataTyp.VARBINARY:
                return size <= 65535;
            case DataTyp.BLOB:
            case DataTyp.TEXT:
                return size <= 65535;
            case DataTyp.MEDIUMBLOB:
            case DataTyp.MEDIUMTEXT:
                return size <= 16777215;
            case DataTyp.LONGBLOB:
            case DataTyp.LONGTEXT:
                return size <= 4294967295;
            case DataTyp.DATE:
            case DataTyp.DATETIME:
            case DataTyp.TIMESTAMP:
            case DataTyp.TIME:
            case DataTyp.YEAR:
                break;
            case DataTyp.ENUM:
                return size <= 65535;
            case DataTyp.SET:
                return size <= 64;
            default:
                throw new ArgumentOutOfRangeException(nameof(typ), typ, null);
        }

        return false;
    }
}