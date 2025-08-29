namespace SqlHelper.Models
{
    public class TableModel
    {
        private List<ColumnModel> _columns = new List<ColumnModel>();

        public string Schema { get; }
        public string Name { get; }
        public long RowCount { get; }
        public bool IsChecked { get; set; }
        public IReadOnlyList<ColumnModel> Columns => _columns;

        public TableModel(string schema, string name, long rowCount = 0)
        {
            Schema = schema;
            Name = name;
            RowCount = rowCount;
        }

        public void AddColumn(ColumnModel column)
        {
            if (!ColumnExists(column.Name))
                _columns.Add(column);
        }

        private bool ColumnExists(string columnName)
        {
            return _columns.Any(t => t.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
