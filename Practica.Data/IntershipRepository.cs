using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Practica.Core;
using System.Data.Common;

namespace Practica.Data
{
    public class IntershipRepository : IIntershipRepository
    {

        public Internship Get(string id)
        {
            Internship internship =  null;

            using (DataBase dataBase = new DataBase())
            {
                // build sql
                String sql = "SELECT * FROM activity WHERE id='@id'";

                // build parameters
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    {"id", id}
                };

                // execute sql
                using (DbDataReader reader = dataBase.ExecuteQuery(sql, parameters))
                {
                    if (reader.Read())
                    {
                        internship = IntershipBuilder(reader);
                    }
                }
            }
            return internship;
        }

        public Internship IntershipBuilder(DbDataReader reader)
        {
            string id = reader["Id"].ToString();
            string title = reader["Title"].ToString();
            string description = reader["Description"].ToString();
            DateTime startDate = reader.GetDateTime(reader.GetOrdinal("Start_date"));
            DateTime endDate = reader.GetDateTime(reader.GetOrdinal("End_date"));
            return new Internship(id, title, description, startDate, endDate);
        }

        public void Add(Internship internship)
        {
            using (DataBase dataBase = new DataBase())
            {
                // build sql
                String sql = "INSERT INTO activity (id, title, description, startDate, endDate) VALUES (@id, @title, @description, @startDate, @endDate)";

                // build parameters
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    {"id", internship.Id},
                    {"title", internship.Title },
                    {"description", internship.Description },
                    {"startDate", internship.StartDate.ToString("yyyy-MM-dd") },
                    {"endDate", internship.EndDate.ToString("yyyy-MM-dd") },

                };

                // execute sql
                dataBase.ExecuteUpdate(sql, parameters);
            }
        }

        public bool Remove(string id)
        {
            using (DataBase dataBase = new DataBase())
            {
                // build sql
                String sql = "DELETE FROM activity WHERE id='@id'";

                // build parameters
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    {"id", id}
                };

                // execute sql
                int count = dataBase.ExecuteUpdate(sql, parameters);
                if(count>0) return true;
                else return false;
            }
        }

    }
}
