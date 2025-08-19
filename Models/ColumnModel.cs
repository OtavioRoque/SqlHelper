using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

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
