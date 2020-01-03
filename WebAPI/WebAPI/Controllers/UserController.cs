using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        User[] users;
        string _con = Connection.GetConnection();

        public UserController()
        {
            users = GetAllUsers().ToArray();

            if (users == null)
            {
                Debug.WriteLine("DEBUG DATA");

                users = new User[] {
                new User { UserId = 0, Username = "John", Password = "LemonJuice" },
                new User { UserId = 1, Username = "Marge", Password = "notSimpson" },
                new User {UserId = 2, Username = "Carl", Password = "MurderLlama"},
                new User {UserId = 3, Username = "Timmy", Password = "T-t-timmy"},
                new User {UserId = 4, Username = "Bill", Password = "Portculis"},
                new User {UserId = 5, Username = "Steve", Password = "Jobless"},
                new User {UserId = 99, Username = "DEBUGDATA", Password = "Jobless"}
            };
            }
        }
        public UserController(User[] _users)
        {
            users = _users;
        }

        public UserController(User u)
        {
            users = new User[] { u };
        }

        #region TESTDBCON
        /// <summary>
        /// Dummy, for testing DB connection.
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetCreate()
        {
            string conString = "Server=10.108.233.52;" +
                "Initial Catalog=VortexDB;" +
                "User Id=sqladmin;" +
                "Password =Pa$$w0rd;";

            using (DAL.UserRepository repo = new DAL.UserRepository(conString))
            {
                repo.Create(UserConverter.ConvertFrom(users[0]));
            }
            return Ok(users[2]);
        }
        #endregion

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            User[] toReturn;

            using (DAL.UserRepository repo = new DAL.UserRepository(_con)) { toReturn = UserConverter.ConvertTo(repo.GetAll()); }

            return toReturn;
        }

        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            User u = null;

            using (DAL.UserRepository repo = new DAL.UserRepository(_con)) { u = UserConverter.ConvertTo(repo.GetById(id)); }

            if (u == null)
                return NotFound();

            return Ok(u);
        }

        [HttpPost]
        public IHttpActionResult CreateUser(User u)
        {
            using (DAL.UserRepository repo = new DAL.UserRepository(_con))
            {
                repo.Create(UserConverter.ConvertFrom_NoID(u));
            }
            return Ok();
        }


        public IHttpActionResult AuthenticateUser(User u)
        {
            bool validated = false;

            using (DAL.UserRepository repo = new DAL.UserRepository(_con))
            {
                validated = repo.AuthenticateUser(UserConverter.ConvertFrom_NoID(u));
            }

            if (validated)
                return Ok();
            return BadRequest();
        }
    }
}