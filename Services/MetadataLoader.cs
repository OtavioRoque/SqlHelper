using SqlHelper.Models;
using System.Collections.ObjectModel;
using System.Data;

namespace SqlHelper.Services
{
    /// <summary>
    /// Contém métodos utilitários para carregar metadados do banco de dados, como bancos, tabelas e colunas.
    /// </summary>
    public static class MetadataLoader
    {
        /// <summary>
        /// Busca o nome de todos os bancos de dados disponíveis no servidor.
        /// </summary>
        /// <returns>
        /// Uma coleção de objetos <see cref="DatabaseModel"/> representando os bancos de dados encontrados.
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
        /// Carrega as tabelas de um banco de dados específico, incluindo o esquema e contagem de linhas.
        /// </summary>
        public static void LoadTables(ObservableCollection<TableModel> tables, DatabaseModel database)
        {
            if (!MetadataValidator.IsValidDatabase(database))
                return;

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

            tables.Clear();

            foreach (DataRow dr in dtTables.Rows)
            {
                string schema = dr["SchemaName"].ToString() ?? string.Empty;
                string name = dr["TableName"].ToString() ?? string.Empty;
                long rowCount = PH.ToLong(dr["RowCount"].ToString() ?? string.Empty);

                tables.Add(new TableModel(schema, name, rowCount));
            }
        }

        /// <summary>
        /// Carrega o nome e o tipo das colunas de uma tabela específica.
        /// </summary>
        public static void LoadColumns(ObservableCollection<ColumnModel> columns, DatabaseModel database, TableModel table)
        {
            if (!MetadataValidator.IsValidDatabase(database) || !MetadataValidator.IsValidTable(table))
                return;

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

            columns.Clear();

            foreach (DataRow dr in dtColumns.Rows)
            {
                string name = dr["COLUMN_NAME"].ToString() ?? string.Empty;
                SqlDbType dataType = PH.ToSqlDbType(dr["DATA_TYPE"].ToString() ?? string.Empty);

                columns.Add(new ColumnModel(name, dataType));
            }
        }
    }
}
