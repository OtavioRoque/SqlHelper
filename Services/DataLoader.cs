using SqlHelper.Models;
using System.Data;

namespace SqlHelper.Utils
{
    public static class DataLoader
    {
        /// <summary>
        /// Carrega os dados de uma tabela específica, limitando o número máximo de linhas retornadas.
        /// </summary>
        /// <param name="database">Banco de dados onde a tabela está localizada.</param>
        /// <param name="table">Tabela cujos dados serão carregados.</param>
        /// <param name="maxRows">Número máximo de linhas a serem retornadas (padrão: 300).</param>
        /// <returns>
        /// Um DataTable contendo os dados da tabela.
        /// </returns>
        public static DataTable LoadTableData(DatabaseModel database, TableModel table, int maxRows = 300)
        {
            if (!MetadataValidator.IsValidDatabase(database) || !MetadataValidator.IsValidTable(table))
                return new DataTable();

            string sql = $"SELECT TOP {maxRows} * FROM [{database.Name}].[{table.Schema}].[{table.Name}];";
            return SQL.FillDataTable(sql);
        }
    }
}
