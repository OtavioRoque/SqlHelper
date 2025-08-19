using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelper.Models
{
    public enum SqlDataType
    {
        BigInt,
        Int,
        Bit,
        Decimal,
        Float,
        DateTime,
        Date,
        Text, 
        Varchar
    }

    public class ColumnModel
    {
        public string Name { get; }
        public SqlDataType DataType { get; }

        public ColumnModel(string name, SqlDataType dataType)
        {
            Name = name;
            DataType = dataType;
        }
    }
}
