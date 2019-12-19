using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security;

namespace DAL
{
    public abstract class SQLRepository<T> : Repository<T>, ISQLRepository<T>, IDisposable
        where T : class
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlCredential cred;

        string cmdText;
        string connectionString;
        string defaultConnection = "Server=10.108.233.52;" +
                "Initial Catalog=VortexDB;" +
                "User Id=sqladmin;" +
                "Password =Pa$$w0rd;";

        protected SqlConnection Connection { get { return con; } set { con = value; } }
        protected SqlCommand Cmd { get { return cmd; } set { cmd = value; } }
        protected string CmdText { get { return cmdText; } set { cmdText = value; } }
        protected string ConnectionString { get { return connectionString; } set { connectionString = value; } }
        public string DefaultConnection { get { return defaultConnection; } set { defaultConnection = value; } }
        /// <summary>
        /// Instantiates with a hardcoded, default connection.
        /// </summary>
        public SQLRepository()
        {
            Cmd = new SqlCommand();
            ConnectionString = DefaultConnection;
            Connection = new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// To be used when the connection string is known at instantiation time.
        /// </summary>
        /// <param name="_conn"></param>
        public SQLRepository(string _conn)
        {
            ConnectionString = _conn;
            Connection = new SqlConnection(ConnectionString);
            Cmd = new SqlCommand();
        }

        /// <summary>
        /// Remember to use SecureString for password.
        /// </summary>
        /// <param name="_conn"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public SQLRepository(string _conn, string username, SecureString password) : this(_conn)
        {
            cred = new SqlCredential(username, password);
            Connection.Credential = cred;
        }

        /// <summary>
        /// Password is Secure String.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void SetCredential(string username, SecureString password)
        {
            cred = new SqlCredential(username, password);
        }

        public void Connect()
        {
            try
            {
                if (ConnectionString == null)
                    throw new SQLConnectionException(ConnectionString);


                con = new SqlConnection(ConnectionString);

                if (cred != null)
                    con.Credential = cred;
                
            }
            catch (SQLConnectionException ex)
            {
                throw ex;
            }
        }
        public void Disconnect()
        {
            if(con != null)
            {
                if (con.State != System.Data.ConnectionState.Closed)
                    con.Close();
                con = null;
            }
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}