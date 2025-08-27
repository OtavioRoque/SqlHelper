namespace SqlHelper.Models
{
    public class TableModel
    {
        private List<ColumnModel> _columns = new List<ColumnModel>();

        public string Schema { get; }
        public string Name { get; }
        public long RowCount { get; set; }
        public bool IsChecked { get; set; }
        public IReadOnlyList<ColumnModel> Columns => _columns;

        public TableModel(string schema, string name)
        {
            Schema = schema;
            Name = name;
        }

        public void AddColumn(ColumnModel column)
        {
            if (ColumnExists(column.Name))
                return;

            _columns.Add(column);
        }

        private bool ColumnExists(string columnName)
        {
            return _columns.Any(t => t.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
