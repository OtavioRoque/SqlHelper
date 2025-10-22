using System.Data;

namespace SqlHelper.Utils
{
    /// <summary>
    /// Contém métodos utilitários para conversão de tipos.
    /// </summary>
    public static class ParseHelper
    {
        /// <summary>
        /// Converte uma string em um int.
        /// </summary>
        /// <param name="s">A string a ser convertida.</param>
        /// <returns>O valor inteiro convertido ou 0 em caso de falha.</returns>
        public static int ToInt(string s)
        {
            _ = int.TryParse(s, out int result);
            return result;
        }

        /// <summary>
        /// Converte uma string em um long.
        /// </summary>
        /// <param name="s">A string a ser convertida.</param>
        /// <returns>O valor long convertido ou 0 em caso de falha.</returns>
        public static long ToLong(string s)
        {
            _ = long.TryParse(s, out long result);
            return result;
        }

        /// <summary>
        /// Converte uma string representando um tipo SQL para o correspondente SqlDbType.
        /// </summary>
        /// <remarks>
        /// É recomendado que o parâmetro sqlType seja a informação lida de um INFORMATION_SCHEMA.COLUMNS.DATA_TYPE.
        /// </remarks>
        /// <param name="sqlType">O tipo da coluna.</param>
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
