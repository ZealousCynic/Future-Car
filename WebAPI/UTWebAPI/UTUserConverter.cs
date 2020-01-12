using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UTWebAPI
{
    public class UTUserConverter
    {
        [Theory]
        [InlineData(0, "Bob", "Askepot")]
        void UTConvertFromShouldSucceed(int id, string username, string password)
        {
            WebAPI.Models.User u = new WebAPI.Models.User();

            u.UserId = id;
            u.Username = username;
            u.Password = password;

            var result = WebAPI.UserConverter.ConvertFrom(u);

            Assert.IsType<DAL.User>(result);
        }
    }
}
