using System;
using System.Collections.Generic;
using System.Text;
using Practica.Core;
using System.Data.Common;

namespace Practica.Data
{
    class UserRepository: IUserRepository
    {
        public User Get(string id)
        {
            User user = null;

            using (DataBase dataBase = new DataBase())
            {
                // build sql
                String sql = "SELECT * FROM [user] WHERE id='@id'";

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
                        user = UserBuilder(reader);
                    }
                }
            }

            return user;
        }

        public User UserBuilder(DbDataReader reader)
        {
            string id = reader["Id"].ToString();
            string name = reader["Name"].ToString();
            string email = reader["Email"].ToString();
            string role = reader["Role"].ToString();
            return new User(id, name, email, role);
        }

        public void Add(User user)
        {
            using (DataBase dataBase = new DataBase())
            {
                string sql = "";
                // build sql insert or update
                if (Get(User.Id) == null)
                {
                    sql = "INSERT INTO activity (id, name, email, role) VALUES (@id, @name, @email, @role)";
                }
                else
                {
                    sql = "UPDATE activity SET name=@name, email=@email, role=@role WHERE id=@id ";
                }

                // build parameters
                Dictionary<string, string> parameters = new Dictionary<string, string>
                    {
                         {"id", user.Id},
                         {"name", user.Name },
                         {"email", user.Email },
                         {"role", user.Role }
                    };

                // execute sql
                dataBase.ExecuteUpdate(sql, parameters);
            }
        }

    }
}
