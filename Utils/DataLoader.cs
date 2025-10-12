using SqlHelper.Models;
using System.Data;

namespace SqlHelper.Utils
{
    public static class DataLoader
    {
        public static DataTable LoadTableData(DatabaseModel database, TableModel table, int maxRows = 300)
        {
            if (database == null || table == null)
                return new DataTable();
            if (string.IsNullOrWhiteSpace(database.Name) || string.IsNullOrWhiteSpace(table.Schema) || string.IsNullOrWhiteSpace(table.Name))
                return new DataTable();

            string sql = $"SELECT TOP {maxRows} * FROM [{database.Name}].[{table.Schema}].[{table.Name}];";
            return SQL.FillDataTable(sql);
        }
    }
}
