using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base
{
    public static class CryptHelper
    {
        public static string GenerateSalt(string userName, string password, int saltLength = 12)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var b64 = Convert.ToBase64String(new UTF8Encoding().GetBytes(userName + password));

                var s = GetMd5Hash(md5Hash, userName + password + b64);

                return s.Length >= saltLength ? s.Substring(0, saltLength) : s;
            }
        }

        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {                
                return GetMd5Hash(md5Hash, input);                
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string[] GetUserFromHttpHeader(string header)
        {
            var authStr = header.Trim();
            if (authStr.IndexOf("Basic", 0) != 0)
            {
                return null;
            }            

            authStr = authStr.Trim();

            string encodedCredentials = authStr.Substring(6);

            byte[] decodedBytes =
            Convert.FromBase64String(encodedCredentials);
            string s = new UTF8Encoding().GetString(decodedBytes);

            return s.Split(new char[] { ':' });
        }
    }
}
