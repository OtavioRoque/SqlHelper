using SqlHelper.Models;

namespace SqlHelper.Services
{
    /// <summary>
    /// Validates the metadata.
    /// </summary>
    public static class MetadataValidator
    {
        /// <summary>
        /// Checks whether the database is not null and has a valid name.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the database is valid; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsValidDatabase(DatabaseModel database)
        {
            return database != null && !string.IsNullOrWhiteSpace(database.Name);
        }

        /// <summary>
        /// Checks whether the table is not null and has a valid schema and name.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the table is valid; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsValidTable(TableModel table)
        {
            return table != null && !string.IsNullOrWhiteSpace(table.Schema) && !string.IsNullOrWhiteSpace(table.Name);
        }
    }
}
