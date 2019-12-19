using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Serializable]
    class SQLConnectionException : Exception
    {
        public SQLConnectionException() { }
        public SQLConnectionException(string _conn) : base(String.Format("Invalid Connection String: {0}", _conn))  {  }
    }
}
