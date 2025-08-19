using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelper.Models
{
    public class TableModel
    {
        public bool IsChecked { get; set; } = false;
        public string Name { get; }
        public long RowCount { get; }
        public List<ColumnModel> Columns { get; }

        public TableModel(string name, long rowCount, List<ColumnModel> columns)
        {
            Name = name;
            RowCount = rowCount;
            Columns = columns ?? new List<ColumnModel>();
        }
    }
}
