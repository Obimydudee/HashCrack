using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashCrack.Utils
{
    internal class Hashing
    {
        public static string MD5(string s)
        {
            var provider = System.Security.Cryptography.MD5.Create();
            StringBuilder builder = new StringBuilder();

            foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                builder.Append(b.ToString("x2").ToLower());

            return builder.ToString();
        }

        public static string SHA256(string s)
        {
            byte[] b = Encoding.UTF8.GetBytes(s);
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(b);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString.Replace("-", String.Empty);
        }
    }
}
