using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Practica.Core;
using System.Data.Common;

namespace Practica.Data
{
    public class ActivityQueryRepository: IActivityQueryRepository
    {
        public ActivityQueryRepository()
        {

        }

        public List<Activity> Find(ActivityFilter filters)
        {
            List<Activity> activities = new List<Activity>();

            using (DataBase dataBase = new DataBase())
            {
                // build sql
                String sql = "SELECT * FROM activity WHERE title like '%@title%'";

                // build parameters
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                if (!String.IsNullOrEmpty(filters.Title))
                {
                    parameters.Add("title", filters.Title);
                }

                // execute sql
                using (DbDataReader reader = dataBase.ExecuteQuery(sql, parameters))
                {
                    while (reader.Read())
                    {
                        activities.Add(ActivityBuilder(reader));
                    }
                }
            }
            return activities;
            
        }

        public Activity ActivityBuilder(DbDataReader reader)
        {
            Activity activity = new Activity();
            activity.Id = (int)reader["Id"];
            activity.Title = reader["Title"].ToString();
            activity.Description = reader["Description"].ToString();
            activity.StartDate = reader.GetDateTime(reader.GetOrdinal("Start_date"));
            activity.EndDate = reader.GetDateTime(reader.GetOrdinal("End_date"));
            return activity;
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
