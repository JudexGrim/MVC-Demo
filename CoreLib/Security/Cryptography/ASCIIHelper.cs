using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreLib.Security.Cryptography
{
    public static class ASCIIHelper
    {

        public static byte[] Encode<T>(T input)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            var inputString = JsonSerializer.Serialize(input);

            return encoding.GetBytes(inputString);
        }


        public static T Decode<T>(byte[] input)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            var decodedString = encoding.GetString(input);

            return JsonSerializer.Deserialize<T>(decodedString);
        }
    }
}