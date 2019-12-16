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
    /// Used to enccrypt and decrypt text using RSACryptoServiceProvider
    /// </summary>
    class SecureText
    {
        private string privatePath, publicPath, privateKey;
        private int keysize;

        public SecureText()
        {
            string privatePath = @".Private\";
            string publicPath = @".Public\";
            string privateKey = privatePath + "Decryptionkey.xml";
            string publicKey = publicPath + "Encryptionkey.xml";
            keysize = 4096;

            if (File.Exists(privateKey) == false)
            {
                using (var rsa = new RSACryptoServiceProvider(keysize))
                {
                    rsa.PersistKeyInCsp = false;

                    File.WriteAllText(publicKey, rsa.ToXmlString(false));
                    File.WriteAllText(privateKey, rsa.ToXmlString(true));
                }
            }
        }

        /// <summary>
        /// Decryping encrypted text, using the private decryption key.
        /// </summary>
        /// <param name="encryptedText">Encrypted text to decrypt</param>
        /// <returns>Decrypted text as string</returns>
        public string Decrypt(string encryptedText)
        {
            if (File.Exists(privateKey) == false)
            {
                throw new Exception("Trying to decode without a decryption key. Object not initialized.");
            }

            string xmlString = File.ReadAllText(privateKey); // private key data
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keysize))
            {
                rsa.FromXmlString(xmlString);
                decryptedBytes = rsa.Decrypt(encryptedBytes, true);
            }
            string decryptedString = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedString;
        }

        /// <summary>
        /// Encrypting text to receiver using receivers public encryption key
        /// </summary>
        /// <param name="messageString">Plaintext to encrypt</param>
        /// <param name="receiverID">Receiver ID, so receivers public key can be found</param>
        /// <returns>text encrypted with receivers public RSA key</returns>
        public string Encrypt(string messageString, string receiverID)
        {
            string publicKey = publicPath + receiverID + ".xml";
            if (File.Exists(publicKey) == false)
            {
                throw new Exception("Cannot encrypt for ID: " + receiverID + " - public key not known.");
            }

            string xmlString = File.ReadAllText(publicKey);
            byte[] messageBytes;
            byte[] encryptedByte;
            string encryptedString;
            messageBytes = Encoding.UTF8.GetBytes(messageString);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keysize))
            {
                rsa.FromXmlString(xmlString);
                encryptedByte = rsa.Encrypt(messageBytes, true);
            }
            encryptedString = Convert.ToBase64String(encryptedByte);
            return encryptedString;
        }
    }
}
