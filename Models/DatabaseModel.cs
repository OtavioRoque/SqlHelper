namespace SqlHelper.Models
{
    internal class DatabaseModel
    {
        public string Name { get; }
        public List<TableModel> Tables { get; } = new List<TableModel>();

        public DatabaseModel(string name)
        {
            Name = name;
        }

        //public bool AddTable(TableInfo table)
        //{

        //}

        private bool TablesExists(string tableName)
        {
            return Tables.Any(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
