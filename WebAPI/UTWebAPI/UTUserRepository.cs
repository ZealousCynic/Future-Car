using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;
using Xunit;

namespace UTWebAPI
{
    public class UTUserRepository
    {
        [Fact]
        public void UTCreateNewUserShouldSucceed()
        {
            WebAPI.Models.User u = new WebAPI.Models.User();
            u.Username = "Bob";
            u.Password = "424242";

            using (DAL.UserRepository repo = new DAL.UserRepository())
            {
                repo.Create(UserConverter.ConvertFrom_NoID(u));
            }
        }

        [Theory]
        [InlineData("INVALIDSTRING")]
        public void UTCreateNewUserShouldFail(string connection)
        {
            WebAPI.Models.User u = new WebAPI.Models.User();
            u.Username = "Bob";
            u.Password = "424242";

            using (DAL.UserRepository repo = new DAL.UserRepository(connection))
            {
                repo.Create(UserConverter.ConvertFrom_NoID(u));
            }
        }
    }
}
