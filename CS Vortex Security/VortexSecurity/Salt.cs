using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VortexSecurity
{
    /// <summary>
    /// Used to salt passwords before transmitting and storage
    /// </summary>
    class Salt : IDisposable
    {
        private string saltKeyFile = ".SaltKey.dat"; // hardcoded to default placement, for simplicity

        /// <summary>
        /// Hashing (salting) a password before transmission and storage
        /// Hasing is using both username and password to salt, to prevent
        /// similiar passwords to end up as similiar hashed passwords.
        /// </summary>
        /// <param name="username">User id</param>
        /// <param name="password">Use password</param>
        /// <returns>Hashed (salted) string</returns>
        public string Hash(string username, string password)
        {
            byte[] maced;
            /* Username and password is both used in the salt, to prevent
             * two similiar passwords to end up with the same
             * salted hash */
            string plainText = username + password;
            byte[] messageBytes = Encoding.UTF8.GetBytes(plainText);
            using (HMAC managedHMACSHA512 = new HMACSHA512())
            {
                managedHMACSHA512.Key = getSaltKey();
                maced = managedHMACSHA512.ComputeHash(messageBytes);
            }
            return Convert.ToBase64String(maced);
        }

        /// <summary>
        /// Retrieves salt key from local file
        /// </summary>
        /// <returns></returns>
        private byte[] getSaltKey()
        {
            if (File.Exists(saltKeyFile) == false)
            {
                GenerateKey();
            }
            return File.ReadAllBytes(saltKeyFile);
        }

        /// <summary>
        /// Generates a new salt key, and saves it in local file
        /// </summary>
        private void GenerateKey()
        {
            byte[] data = new byte[32];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(data);
            }
            File.WriteAllBytes(saltKeyFile, data);
        }

        public void Dispose()
        {
        }
    }
}
