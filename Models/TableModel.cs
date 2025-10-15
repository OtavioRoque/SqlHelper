using CommunityToolkit.Mvvm.ComponentModel;

namespace SqlHelper.Models
{
    public class TableModel
    {
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
