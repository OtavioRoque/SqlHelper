namespace SqlHelper.Models
{
    internal class DatabaseInfo
    {
        public string Name { get; }
        public List<TableInfo> Tables { get; } = new List<TableInfo>();

        public DatabaseInfo(string name)
        {
            Name = name;
        }

        public bool AddTable(TableInfo table)
        {

        }

        private bool TablesExists(string tableName)
        {
            return Tables.Any(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
