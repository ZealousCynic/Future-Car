using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI
{
    public static class UserConverter
    {
        public static DAL.User ConvertFrom(Models.User u)
        {
            using (VortexSecurity.Salt salty = new VortexSecurity.Salt()) {
                u.Password = salty.Hash(u.Username, u.Password);
            }

            return new DAL.User
            {
                ID = u.UserId,
                Username = u.Username,
                Password = u.Password
            };
        }

        public static DAL.User ConvertFrom_NoID(Models.User u)
        {
            return new DAL.User
            {
                Username = u.Username,
                Password = u.Password
            };
        }

        public static Models.User ConvertTo(DAL.User u)
        {
            return new Models.User
            {
                UserId = u.ID,
                Username = u.Username,
                Password = u.Password
            };
        }
    }
}