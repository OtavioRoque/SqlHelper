namespace SqlHelper.Models
{
    internal class DatabaseModel
    {
        public string Name { get; }
        public IReadOnlyList<TableModel> Tables => _tables;

        private List<TableModel> _tables = new List<TableModel>();

        public DatabaseModel(string name)
        {
            Name = name;
        }

        public void AddTable(TableModel table)
        {
            if (TableExists(table.Name))
                return;

            _tables.Add(table);
        }

        private bool TableExists(string tableName)
        {
            return _tables.Any(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
