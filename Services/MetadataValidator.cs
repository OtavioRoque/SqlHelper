using SqlHelper.Models;

namespace SqlHelper.Services
{
    /// <summary>
    /// Consiste a validade dos metadados.
    /// </summary>
    public static class MetadataValidator
    {
        /// <summary>
        /// Verifica se o banco de dados é diferente de null e tem um nome válido.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> se o banco de dados for válido; caso contrário, <see langword="false"/>.
        /// </returns>
        public static bool IsValidDatabase(DatabaseModel database)
        {
            return database != null && !string.IsNullOrWhiteSpace(database.Name);
        }

        /// <summary>
        /// Verifica se a tabela é diferente de null e tem um schema/nome válido.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> se o banco de dados for válido; caso contrário, <see langword="false"/>.
        /// </returns>
        public static bool IsValidTable(TableModel table)
        {
            return table != null && !string.IsNullOrWhiteSpace(table.Schema) && !string.IsNullOrWhiteSpace(table.Name);
        }
    }
}
