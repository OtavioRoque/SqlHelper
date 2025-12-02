using SqlHelper.Models;
using System.Data;

namespace SqlHelper.Services
{
    public static class DataLoader
    {
        /// <summary>
        /// Loads data from a specific table, limiting the maximum number of returned rows.
        /// </summary>
        /// <param name="database">The database where the table is located.</param>
        /// <param name="table">The table to load data from.</param>
        /// <param name="maxRows">The maximum number of rows to return (default: 300).</param>
        /// <returns>
        /// A DataTable containing the table data.
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
