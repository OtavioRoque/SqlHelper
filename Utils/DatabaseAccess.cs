using System.Data;
using Microsoft.Data.SqlClient;
using SqlHelper.Config;

#pragma warning disable CS8601
#pragma warning disable CS8603

namespace SqlHelper.Utils
{
    public static class DatabaseAccess
    {
        private static readonly string connectionString = ConfigLoader.ObterConnectionString("ConexaoLocal");

        public static DataTable GetDataTable(string sql)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(sql, conn);
                using var adapter = new SqlDataAdapter(cmd);

                var tabela = new DataTable();
                adapter.Fill(tabela);
                return tabela;
            }
            catch (Exception ex)
            {
                throw LancarExcecaoComandoSql(sql, ex);
            }
        }

        public static string GetScalar(string sql)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(sql, conn);

                conn.Open();
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw LancarExcecaoComandoSql(sql, ex);
            }
        }

        public static int Execute(string sql)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(sql, conn);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw LancarExcecaoComandoSql(sql, ex);
            }
        }

        private static Exception LancarExcecaoComandoSql(string comandoSql, Exception exception)
        {
            string mensagem = $"Erro ao executar comando SQL: {comandoSql}. Detalhes: {exception.Message}";
            return new Exception(mensagem, exception);
        }
    }
}
