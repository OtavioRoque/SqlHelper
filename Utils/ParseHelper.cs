using System.Data;

namespace SqlHelper.Utils
{
    /// <summary>
    /// Contains utility methods for type conversion.
    /// </summary>
    public static class ParseHelper
    {
        /// <summary>
        /// Converts a string to an int.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The converted integer value, or 0 if the conversion fails.</returns>
        public static int ToInt(string s)
        {
            _ = int.TryParse(s, out int result);
            return result;
        }

        /// <summary>
        /// Converts a string to a long.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The converted long value, or 0 if the conversion fails.</returns>
        public static long ToLong(string s)
        {
            _ = long.TryParse(s, out long result);
            return result;
        }

        /// <summary>
        /// Converts a string representing an SQL type to its corresponding <see cref="SqlDbType"/>.
        /// </summary>
        /// <remarks>
        /// It is recommended that the <paramref name="sqlType"/> value come from INFORMATION_SCHEMA.COLUMNS.DATA_TYPE.
        /// </remarks>
        /// <param name="sqlType">The SQL column type.</param>
        /// <returns>The corresponding <see cref="SqlDbType"/>.</returns>
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
