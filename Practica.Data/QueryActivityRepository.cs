using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Practica.Data
{
    public class QueryActivityRepository
    {
        public QueryActivityRepository()
        {

        }

        public List<String> getActivity()
        {
            List<String> result = new List<string>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=DESKTOP-28P3VE1;Database=practica;Trusted_Connection=True";
                conn.Open();

                SqlCommand command = new SqlCommand("Select * from activity", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add((String)reader[0]);
                    }
                }
            }

            return result;
        }
    }
}
