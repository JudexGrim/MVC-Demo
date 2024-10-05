using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    internal class RandomGenHelper : Disposer
    {
        public string GenRandomString(int length)
        {
            byte[] randomBytes = new byte[length];
            RandomNumberGenerator.Fill(randomBytes);

            var keyBuilder = new StringBuilder(length);
            foreach (var b in randomBytes)
            {
                char randomChar = (char)(b % 95 + 32);
                keyBuilder.Append(randomChar);
            }
            return keyBuilder.ToString();
        }
    }
}
