using CommunityToolkit.Mvvm.ComponentModel;

namespace SqlHelper.Models
{
    public class TableModel : ObservableObject
    {
        public DatabaseModel Database { get; }
        public string Schema { get; }
        public string Name { get; }
        public long RowCount { get; }

        public TableModel(DatabaseModel database, string schema, string name, long rowCount = 0)
        {
            Database = database;
            Schema = schema;
            Name = name;
            RowCount = rowCount;
        }
    }
}
