using SqlHelper.Models;

namespace SqlHelper.Services
{
    public class SelectGenerator
    {
        public enum SelectType
        {
            Default,
            Cliente,
            Item
        }

        private readonly DatabaseModel _database;
        private readonly TableModel _table;

        public SelectGenerator(DatabaseModel database, TableModel table)
        {
            _database = database;
            _table = table;
        }

        public string GenerateSelect(SelectType selectType)
        {
            var columns = MetadataLoader.LoadColumns(_database, _table);

            return string.Empty;
        }
    }
}
