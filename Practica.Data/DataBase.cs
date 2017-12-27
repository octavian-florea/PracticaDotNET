using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace Practica.Data
{
    public class DataBase:IDisposable
    {
        private const string TEMPLATE_CONNECTION_STRING_NAME = "practicaTemplateConnectionString";

        internal readonly string _connectionstring = "Server=DESKTOP-28P3VE1;Database=practica;Trusted_Connection=True";

        public DataBase(){ }

        public DbDataReader ExecuteQuery(String query, IDictionary<string,string> parameters = null)
        {
            SqlConnection conn = getOpenConnection();
            try
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var key in parameters.Keys)
                        {
                            command.Parameters.AddWithValue($"@{key}", parameters[key]);
                        }
                    }

                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public int ExecuteUpdate(String query, IDictionary<string, string> parameters = null)
        {
            using (SqlConnection conn = getOpenConnection())
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var key in parameters.Keys)
                        {
                            command.Parameters.AddWithValue($"@{key}", parameters[key]);
                        }
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        public void Dispose()
        {
            
        }

        public SqlConnection getOpenConnection()
        {
            SqlConnection conn = new SqlConnection(_connectionstring);
            conn.Open();
            return conn;
        }

    }
}
