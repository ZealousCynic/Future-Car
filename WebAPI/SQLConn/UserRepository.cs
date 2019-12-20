﻿using System;
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
            CmdText = "AuthenticateUser";

            Cmd.Connection = Connection;
            Cmd.CommandText = CmdText;

            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter authorized = new SqlParameter("@Authorized", System.Data.SqlDbType.Bit);
            authorized.Direction = System.Data.ParameterDirection.Output;

            Cmd.Parameters.AddRange(
                new[] {
                    new SqlParameter("@Username", entity.Username),
                    new SqlParameter("@Password", entity.Password),
                    authorized
                    }
                );

            Cmd.Connection.Open();
            Cmd.ExecuteNonQuery();
            if (authorized.Value != null)
                return (bool)authorized.Value;
            return false;
        }

        public override void Create(User entity)
        {
            CmdText = "InsertUser";

            Cmd.Connection = Connection;
            Cmd.CommandText = CmdText;

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
            CmdText = "GetAllUsers";

            Cmd.Connection = Connection;
            Cmd.CommandText = CmdText;

            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Connection.Open();
            SqlDataReader reader = Cmd.ExecuteReader();

            List<User> toReturn = new List<User>();

            if(reader.HasRows)                
                while(reader.Read())
                {
                    toReturn.Add(new User
                    {
                        ID = (int)reader.GetValue(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2)
                    });
                }
            return toReturn.ToArray();
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
