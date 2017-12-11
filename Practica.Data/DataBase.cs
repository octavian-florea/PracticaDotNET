using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace Practica.Data
{
    public class DataBase:IDisposable
    {
        private const string TEMPLATE_CONNECTION_STRING_NAME = "practicaTemplateConnectionString";

        internal readonly string _connectionstring = "Server=DESKTOP-28P3VE1;Database=practica;Trusted_Connection=True";

        private SqlConnection conn;

        public DataBase()
        {
            conn = new SqlConnection(_connectionstring);
            conn.Open();
        }

        public DbDataReader ExecuteQuery(String query)
        {
            SqlCommand command = new SqlCommand(query, conn);
            return  command.ExecuteReader();
        }

        public int ExecuteUpdate(String query)
        {
            SqlCommand command = new SqlCommand(query, conn);
            return command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            conn.Dispose();
        }
    }
}
