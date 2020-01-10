using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(Models.User u)
        {
            bool validated = false;
            using(DAL.UserRepository repo = new DAL.UserRepository())
            {
                validated = repo.AuthenticateUser(UserConverter.ConvertFrom(u));
            }

            if (u.Username == "Martin")
                return Content((HttpStatusCode)418, u);
            if (validated)
                return Ok(validated);
            return NotFound();
        }
    }
}