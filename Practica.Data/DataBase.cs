using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace Practica.Data
{
    public class DataBase
    {
        private const string TEMPLATE_CONNECTION_STRING_NAME = "practicaTemplateConnectionString";

        internal readonly string _connectionstring;

        private SqlConnection conn;

        public DataBase()
        {
            _connectionstring = "Server=DESKTOP-28P3VE1;Database=practica;Trusted_Connection=True";
            conn = new SqlConnection(_connectionstring);
            conn.Open();
        }

        public void runQuery(String query)
        {
            SqlCommand command = new SqlCommand(query, conn);
            
        }
    }
}
