using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IUserRepository<T>
<<<<<<< HEAD
=======
        where T : BaseEntity
>>>>>>> 9003e6b56410fc0aace0e988a2e9b66889f69988
    {
        bool AuthenticateUser(T entity);
    }
}
