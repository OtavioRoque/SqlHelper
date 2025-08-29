namespace SqlHelper.Models
{
    public class DatabaseModel
    {
        private List<TableModel> _tables = new List<TableModel>();

        public string Name { get; }
        public IReadOnlyList<TableModel> Tables => _tables;

        public DatabaseModel(string name)
        {
            Name = name;
        }

        public void AddTable(TableModel table)
        {
            if (!TableExists(table.Schema, table.Name))
                _tables.Add(table);
        }

        private bool TableExists(string schema, string tableName)
        {
            return _tables.Any(t =>
                t.Schema.Equals(schema, StringComparison.OrdinalIgnoreCase) &&
                t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
