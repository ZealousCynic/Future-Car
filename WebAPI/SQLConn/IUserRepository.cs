using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IUserRepository<T>
        where T : BaseEntity
    {
        bool AuthenticateUser(T entity);
    }
}
