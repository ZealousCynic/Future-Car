using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login(Models.User u)
        {
            bool validated = false;
            Models.User user = new Models.User();
            using (DAL.UserRepository repo = new DAL.UserRepository(Connection.GetConnection()))
            {
                var result = repo.AuthenticateUser(UserConverter.ConvertFrom(u));
                validated = result.authorized;

                user.UserId = result.userId;
                user.Username = u.Username;
            }

            if (u.Username == "Martin")
                return Content((HttpStatusCode)418, u);
            if (validated)
                return Request.CreateResponse<Models.User>(HttpStatusCode.OK, user);

            return Request.CreateResponse<string>(HttpStatusCode.NotFound, "User not found");
        }
    }
}