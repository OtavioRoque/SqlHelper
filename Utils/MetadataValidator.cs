using SqlHelper.Models;

namespace SqlHelper.Utils
{
    public static class MetadataValidator
    {
        public static bool IsValidDatabase(DatabaseModel database)
        {
            return database != null && !string.IsNullOrWhiteSpace(database.Name);
        }

        public static bool IsValidTable(TableModel table)
        {
            return table != null && !string.IsNullOrWhiteSpace(table.Schema) && !string.IsNullOrWhiteSpace(table.Name);
        }
    }
}
