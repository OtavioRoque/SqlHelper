using System.Data;

namespace SqlHelper.Utils
{
    public static class ParseHelper
    {
        public static int ToInt(string s)
        {
            int.TryParse(s, out int result);
            return result;
        }

        public static long ToLong(string s)
        {
            long.TryParse(s, out long result);
            return result;
        }

        public static SqlDbType ToSqlDbType(string sqlType)
        {
            return sqlType.ToLower() switch
            {
                "int" => SqlDbType.Int,
                "bigint" => SqlDbType.BigInt,
                "smallint" => SqlDbType.SmallInt,
                "tinyint" => SqlDbType.TinyInt,
                "bit" => SqlDbType.Bit,
                "decimal" => SqlDbType.Decimal,
                "numeric" => SqlDbType.Decimal,
                "money" => SqlDbType.Money,
                "smallmoney" => SqlDbType.SmallMoney,
                "float" => SqlDbType.Float,
                "real" => SqlDbType.Real,
                "date" => SqlDbType.Date,
                "datetime" => SqlDbType.DateTime,
                "datetime2" => SqlDbType.DateTime2,
                "smalldatetime" => SqlDbType.SmallDateTime,
                "datetimeoffset" => SqlDbType.DateTimeOffset,
                "time" => SqlDbType.Time,
                "char" => SqlDbType.Char,
                "varchar" => SqlDbType.VarChar,
                "text" => SqlDbType.Text,
                "nchar" => SqlDbType.NChar,
                "nvarchar" => SqlDbType.NVarChar,
                "ntext" => SqlDbType.NText,
                "binary" => SqlDbType.Binary,
                "varbinary" => SqlDbType.VarBinary,
                "image" => SqlDbType.Image,
                "uniqueidentifier" => SqlDbType.UniqueIdentifier,
                "xml" => SqlDbType.Xml,
                _ => SqlDbType.Variant
            };
        }

    }
}
