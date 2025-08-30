using System.Collections.ObjectModel;

namespace SqlHelper.Models
{
    public class DatabaseModel
    {
        public string Name { get; }

        public DatabaseModel(string name)
        {
            Name = name;
        }
    }
}
