using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace UTWebAPI
{
    public class UTLoginController
    {
        [Fact]
        void UTLoginShouldSucceed()
        {
            WebAPI.Controllers.LoginController c = new WebAPI.Controllers.LoginController();

            WebAPI.Models.User u = new WebAPI.Models.User();
            u.Username = "";

            //Send this through the encryption first or just do copy paste of hashed result.
            u.Password = "";

            var result = c.Login(u);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        void UTLoginShouldFail()
        {
            WebAPI.Controllers.LoginController c = new WebAPI.Controllers.LoginController();

            WebAPI.Models.User u = new WebAPI.Models.User();
            u.Username = "";

            //Send this through the encryption first or just do copy paste of hashed result.
            u.Password = "";

            var result = c.Login(u);

            Assert.IsType<OkResult>(result);
        }
    }
}
