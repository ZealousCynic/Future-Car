using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : SQLRepository<User>
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

        public override void Create(User entity)
        {
            CmdText = $"INSERT INTO [User] (Username, Password) VALUES ('{entity.Username}', '{entity.Password}')";

            Cmd.Connection = Connection;
            Cmd.CommandText = CmdText;
            
            //TEMPORARY
            Cmd.CommandType = System.Data.CommandType.Text;

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
