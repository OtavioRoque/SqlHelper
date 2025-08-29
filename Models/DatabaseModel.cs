using System.Collections.ObjectModel;

namespace SqlHelper.Models
{
    public class DatabaseModel
    {
        private ObservableCollection<TableModel> _tables = new ObservableCollection<TableModel>();

        public string Name { get; }
        public ReadOnlyObservableCollection<TableModel> Tables { get; }

        public DatabaseModel(string name)
        {
            Name = name;
            Tables = new ReadOnlyObservableCollection<TableModel>(_tables);
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
