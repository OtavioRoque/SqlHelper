using System.Data;
using Microsoft.Data.SqlClient;

#pragma warning disable CS8601
#pragma warning disable CS8603

namespace SqlHelper.Utils
{
    public static class DatabaseAccess
    {
        private static readonly string connectionString = ConfigLoader.ObterConnectionString("ConexaoLocal");

        /// <summary>
        /// Usar quando precisar de Bindings
        /// </summary>
        /// <returns>
        /// Um DataTable preenchido com os dados retornados pela consulta SQL.
        /// </returns>
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

        public static List<Dictionary<string, object>> GetDataList(string sql)
        {
            var lista = new List<Dictionary<string, object>>();

            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(sql, conn);
                conn.Open();
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var linha = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        linha[reader.GetName(i)] = reader.GetValue(i);
                    }

                    lista.Add(linha);
                }
            }
            catch (Exception ex)
            {
                throw LancarExcecaoComandoSql(sql, ex);
            }

            return lista;
        }


        private static Exception LancarExcecaoComandoSql(string comandoSql, Exception exception)
        {
            string mensagem = $"Erro ao executar comando SQL: {comandoSql}. Detalhes: {exception.Message}";
            return new Exception(mensagem, exception);
        }
    }
}
