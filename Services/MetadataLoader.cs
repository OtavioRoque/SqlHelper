using SqlHelper.Models;
using System.Collections.ObjectModel;
using System.Data;

namespace SqlHelper.Services
{
    /// <summary>
    /// Contains utility methods for loading database metadata such as databases, tables, and columns.
    /// </summary>
    public static class MetadataLoader
    {
        /// <summary>
        /// Retrieves the names of all databases available on the server.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="DatabaseModel"/> objects representing the databases found.
        /// </returns>
        public static IEnumerable<DatabaseModel> LoadDatabases()
        {
            var databases = new List<DatabaseModel>();

            string sql = "SELECT name FROM sys.databases";
            var dtDatabases = SQL.FillDataTable(sql);

            foreach (DataRow dr in dtDatabases.Rows)
            {
                string databaseName = dr["name"].ToString() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(databaseName))
                    databases.Add(new(databaseName));
            }

            return databases;
        }

        /// <summary>
        /// Retrieves the schema, name, and row count of all tables in a database.
        /// </summary>
        /// <param name="database">The database where the tables will be queried.</param>
        /// <returns>
        /// A collection of <see cref="TableModel"/> objects representing the tables found.
        /// </returns>
        public static IEnumerable<TableModel> LoadTables(DatabaseModel database)
        {
            var tables = new List<TableModel>();

            if (!MetadataValidator.IsValidDatabase(database))
                return tables;

            string sql = $@"
                SELECT
                    s.name AS SchemaName,
                    t.name AS TableName,
                    SUM(p.rows) AS [RowCount]
                FROM
                    {database.Name}.sys.tables t
                    JOIN {database.Name}.sys.schemas s ON t.schema_id = s.schema_id
                    JOIN {database.Name}.sys.partitions p ON t.object_id = p.object_id
                WHERE
                    p.index_id IN (0,1)
                GROUP BY
                    s.name, t.name
                HAVING
                    SUM(p.rows) > 0
                ORDER BY
                    s.name, t.name";

            var dtTables = SQL.FillDataTable(sql);

            foreach (DataRow dr in dtTables.Rows)
            {
                string schema = dr["SchemaName"].ToString() ?? string.Empty;
                string name = dr["TableName"].ToString() ?? string.Empty;
                long rowCount = PH.ToLong(dr["RowCount"].ToString() ?? string.Empty);

                tables.Add(new(schema, name, rowCount));
            }

            return tables;
        }

        /// <summary>
        /// Retrieves the name and SQL type of all columns in a table.
        /// </summary>
        /// <param name="database">The database that contains the table whose columns will be queried.</param>
        /// <param name="table">The table whose columns will be queried.</param>
        /// <returns>
        /// A collection of <see cref="ColumnModel"/> objects representing the columns found.
        /// </returns>
        public static IEnumerable<ColumnModel> LoadColumns(DatabaseModel database, TableModel table)
        {
            var columns = new List<ColumnModel>();

            if (!MetadataValidator.IsValidDatabase(database) || !MetadataValidator.IsValidTable(table))
                return columns;

            string sql = @$"
                SELECT 
	                COLUMN_NAME,
	                DATA_TYPE
                FROM
	                {database.Name}.INFORMATION_SCHEMA.COLUMNS
                WHERE
	                TABLE_SCHEMA = '{table.Schema}' AND
                    TABLE_NAME = '{table.Name}'";

            var dtColumns = SQL.FillDataTable(sql);

            foreach (DataRow dr in dtColumns.Rows)
            {
                string name = dr["COLUMN_NAME"].ToString() ?? string.Empty;
                SqlDbType dataType = PH.ToSqlDbType(dr["DATA_TYPE"].ToString() ?? string.Empty);

                columns.Add(new(name, dataType));
            }

            return columns;
        }
    }
}
