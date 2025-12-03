using System.Data;
using SqlHelper.Config;
using Microsoft.Data.SqlClient;
using System.Windows;

#pragma warning disable CS8601
#pragma warning disable CS8603

namespace SqlHelper.Utils
{
    /// <summary>
    /// Contains utility methods for database access.
    /// Useful for avoiding the need to manually open a SqlConnection.
    /// </summary>
    /// <remarks>
    /// Use with the alias SQL.MethodName().
    /// </remarks>
    public static class SqlExecutor
    {
        private static readonly string _connectionString = ConfigLoader.GetConnectionString();

        /// <summary>
        /// Executes a SQL query and loads the result into a <see cref="DataTable"/>.
        /// </summary>
        /// <remarks>
        /// Useful when you need a <see cref="DataTable"/> for data binding.
        /// </remarks>
        /// <param name="sql">The SQL query to execute.</param>
        /// <returns>
        /// A <see cref="DataTable"/> containing the query results, or <c>null</c> if an error occurs.
        /// </returns>
        public static DataTable FillDataTable(string sql)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(sql, conn);
                using var adapter = new SqlDataAdapter(cmd);

                var tabela = new DataTable();
                adapter.Fill(tabela);
                return tabela;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Executes a SQL query and processes each row using the provided callback.
        /// </summary>
        /// <remarks>
        /// Use this when you don't need data binding and want a more performant alternative to <see cref="FillDataTable"/>.
        /// </remarks>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="processador">
        /// A callback that receives a <see cref="SqlDataReader"/> positioned on each row returned by the query.
        /// </param>
        public static void ExecuteReader(string sql, Action<SqlDataReader> processador)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(sql, conn);

                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    processador(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Executes a SQL query and retrieves the first column of the first row in the result set.
        /// </summary>
        /// <returns>
        /// A string representation of the returned value, or an empty string if an error occurs.
        /// </returns>
        public static string ExecuteScalar(string sql)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(sql, conn);

                conn.Open();
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Executes a SQL command such as INSERT, UPDATE, DELETE, or any non-query statement.
        /// </summary>
        /// <returns>
        /// The number of affected rows, or -1 if an exception occurs.
        /// </returns>
        public static int ExecuteNonQuery(string sql)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(sql, conn);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
    }
}
