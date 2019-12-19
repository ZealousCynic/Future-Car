using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : SQLRepository<User>, IUserRepository<User>
    {

        public UserRepository()
        {

        }

        public UserRepository(string _conn) : base(_conn)
        {

        }

        public UserRepository(string _conn, string username, System.Security.SecureString password) : base(_conn, username, password)
        {

        }

        public bool AuthenticateUser(User entity)
        {
<<<<<<< HEAD
            throw new NotImplementedException();
=======
            CmdText = "AuthenticateUser";
            Cmd.Connection = Connection;
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.AddRange(
                new[] {
                    new SqlParameter("Username", entity.Username),
                    new SqlParameter("Password", entity.Password)
                    }
                );

            SqlDataReader reader = Cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                return (bool)reader.GetValue(0);
            }

            Debug.WriteLine("Default false");
            return false;
>>>>>>> 9003e6b56410fc0aace0e988a2e9b66889f69988
        }

        public override void Create(User entity)
        {
            CmdText = "InsertUser";

            Cmd.Connection = Connection;
            Cmd.CommandText = CmdText;

            //TEMPORARY
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Cmd.Parameters.AddRange(
                new[] {
                    new SqlParameter("Username", entity.Username),
                    new SqlParameter("Password", entity.Password)
                    }
                );

            Cmd.Connection.Open();
            Cmd.ExecuteNonQuery();
        }

        public override void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public override User[] GetAll()
        {
            throw new NotImplementedException();
        }

        public override User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
