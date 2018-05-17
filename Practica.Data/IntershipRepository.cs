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

        public Internship Get(int id)
        {
            Internship internship =  null;

            using (DataBase dataBase = new DataBase())
            {
                // build sql
                String sql = "SELECT * FROM activity WHERE id='@id'";

                // build parameters
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    {"id", id.ToString()}
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
            Internship intership = new Internship();
            intership.Id = (int)reader["Id"];
            intership.Title = reader["Title"].ToString();
            intership.Description = reader["Description"].ToString();
            intership.StartDate = reader.GetDateTime(reader.GetOrdinal("Start_date"));
            intership.EndDate = reader.GetDateTime(reader.GetOrdinal("End_date"));
            return intership;
        }

        public void Add(Internship internship)
        {
            using (DataBase dataBase = new DataBase())
            {
                string sql = "";
                // build sql insert or update
                if (Get(internship.Id) == null)
                {
                    sql = "INSERT INTO activity (id, title, description, start_date, end_date) VALUES (@id, @title, @description, @startDate, @endDate)";
                }
                else
                {
                    sql = "UPDATE activity SET title=@title, description=@description, start_date=@startDate, end_date=@endDate WHERE id=@id ";
                }

                // build parameters
                Dictionary<string, string> parameters = new Dictionary<string, string>
                    {
                         {"id", internship.Id.ToString()},
                         {"title", internship.Title },
                         {"description", internship.Description }
                        // {"startDate", internship.StartDate.ToString("yyyy-MM-dd") },
                        // {"endDate", internship.EndDate.ToString("yyyy-MM-dd") },
                    };

                // execute sql
                dataBase.ExecuteUpdate(sql, parameters);
            }
        }

        public bool Remove(int id)
        {
            using (DataBase dataBase = new DataBase())
            {
                // build sql
                String sql = "DELETE FROM activity WHERE id='@id'";

                // build parameters
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    {"id", id.ToString()}
                };

                // execute sql
                int count = dataBase.ExecuteUpdate(sql, parameters);
                if(count>0) return true;
                else return false;
            }
        }

    }
}
