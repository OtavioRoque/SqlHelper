using System.Data;

namespace SqlHelper.Models
{
    public class ColumnModel
    {
        public string Name { get; }
        public SqlDbType DataType { get; }

        public ColumnModel(string name, SqlDbType dataType)
        {
            Name = name;
            DataType = dataType;
        }
    }
}
