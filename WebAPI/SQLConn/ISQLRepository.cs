using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface ISQLRepository<T> : IRepository<T>
        where T : class
    {
        void Connect();
        void Disconnect();
    }
}
