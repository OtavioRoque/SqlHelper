using CommunityToolkit.Mvvm.ComponentModel;

namespace SqlHelper.Models
{
    public class TableModel : ObservableObject
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public string Schema { get; }
        public string Name { get; }
        public long RowCount { get; }


        public TableModel(string schema, string name, long rowCount = 0)
        {
            Schema = schema;
            Name = name;
            RowCount = rowCount;
        }
    }
}
