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
        VortexSecurity.Salt salty = new VortexSecurity.Salt();

        public UserController()
        {
            users = new User[] {
                new User { UserId = 0, Username = "John", Password = "LemonJuice" },
                new User { UserId = 1, Username = "Marge", Password = "notSimpson" },
                new User {UserId = 2, Username = "Carl", Password = "MurderLlama"},
                new User {UserId = 3, Username = "Timmy", Password = "T-t-timmy"},
                new User {UserId = 4, Username = "Bill", Password = "Portculis"},
                new User {UserId = 5, Username = "Steve", Password = "Jobless"}
            };
        }
        public UserController(User[] _users)
        {
            users = _users;
        }

        public UserController(User u)
        {
            users = new User[] { u };
        }

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }

        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            User u = users.FirstOrDefault(delegate (User _u) { return _u.UserId == id; });

            if (u == null)
                return NotFound();

            string salt = salty.Hash(users[id].Username, users[id].Password);

            Debug.WriteLine(salt);
            Debug.WriteLine(salt.Length);

            return Ok(u);
        }

        [HttpPost]
        public IHttpActionResult CreateUser(User u)
        {
            using (DAL.UserRepository repo = new DAL.UserRepository())
            {
                repo.Create(UserConverter.ConvertFrom_NoID(u));
            }
            return Ok();
        }

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

        public IHttpActionResult AuthenticateUser(User u)
        {
            bool validated = false;

            if (validated)
                return Ok();
            return BadRequest();
        }
    }
}
