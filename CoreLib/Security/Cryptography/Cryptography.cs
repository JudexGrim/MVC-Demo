using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Numerics;
using System.ComponentModel;

namespace CoreLib.Security.Cryptography
{
    public static class Cryptography
    {
        private static int _iterations = 10000;
        private static int _keySize = 256;
        private static string _hash = "SHA256";
        private static string _password = "lusdbglinsgnew$38393501733493481041pf90hqfb092g350g5h90t2u09t2345%#@%@#%@^2";

        

        public static string AesEncrypt(this object input)
        {
            var encryptedJson = JsonSerializer.Serialize(input);
            return Encrypt<Aes>(encryptedJson.ToString(), _password);
        }

        public static string Encrypt<T>(string input, string password) where T : SymmetricAlgorithm
        {
            UTF8Encoding encoding = new UTF8Encoding();

            // Generate Salt, Hash, and IV
            using RandomGenHelper rng = new RandomGenHelper();
            using Aes aes = Aes.Create();

            string salt = rng.GenRandomString(12);
            byte[] saltBytes = encoding.GetBytes(salt);
            byte[] iv = aes.IV; 

            // Convert input and password to byte arrays
            byte[] inputBytes = encoding.GetBytes(input);
            byte[] passwordBytes = encoding.GetBytes(password);

            // Create Encoder and Writer
            using SymmetricAlgorithm Cipher = typeof(T) == typeof(Aes) ? Aes.Create() : Activator.CreateInstance<T>();
            Cipher.Mode = CipherMode.CBC;
            Cipher.KeySize = _keySize;

            // Generate Derived Encryption Key
            HashAlgorithmName hash = new HashAlgorithmName(_hash);
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(passwordBytes, saltBytes, _iterations, hash);
            var derivedPass = rfc.GetBytes(_keySize / 8);

            // Create Encryptor and MemoryStream
            using ICryptoTransform encryptor = Cipher.CreateEncryptor(derivedPass, iv);
            using MemoryStream to = new MemoryStream();
            using CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write);
    
            writer.Write(inputBytes, 0, inputBytes.Length);
            writer.FlushFinalBlock();
    

            byte[] encrypted = to.ToArray();

            // Combine IV, Salt, and Encrypted Data
            byte[] result = new byte[iv.Length + encrypted.Length + saltBytes.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length); 
            Buffer.BlockCopy(saltBytes, 0, result, iv.Length, saltBytes.Length);
            Buffer.BlockCopy(encrypted, 0, result, iv.Length + saltBytes.Length, encrypted.Length);

            Cipher.Clear();
            return Convert.ToBase64String(result);
        }

        public static T Decrypt<T>(this string input)
        {
            string decryptedJson = Decrypt<Aes>(input, _password);
            return JsonSerializer.Deserialize<T>(decryptedJson);
        }

        public static string Decrypt<T>(string input, string password) where T : SymmetricAlgorithm
        {
            UTF8Encoding encoding = new UTF8Encoding();

            // Convert input from Base64
            byte[] inputBytes = Convert.FromBase64String(input);

            // Extract IV (first 16 bytes) and Salt (next 12 bytes)
            byte[] iv = new byte[16];
            byte[] salt = new byte[12]; 
            Buffer.BlockCopy(inputBytes, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(inputBytes, iv.Length, salt, 0, salt.Length);

            // Extract Encrypted Data
            byte[] encryptedData = new byte[inputBytes.Length - iv.Length - salt.Length]; 
            Buffer.BlockCopy(inputBytes, iv.Length + salt.Length, encryptedData, 0, encryptedData.Length);

            // Convert password to byte array
            byte[] passwordBytes = encoding.GetBytes(password);

            // Create Crypto Reader

            using SymmetricAlgorithm Cipher = typeof(T) == typeof(Aes) ? Aes.Create() : Activator.CreateInstance<T>();
            Cipher.Mode = CipherMode.CBC;
            Cipher.KeySize = _keySize;
            

            // Generate Decryption Key using Salt and Password
            HashAlgorithmName hash = new HashAlgorithmName(_hash);
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(passwordBytes, salt, _iterations, hash);
            var derivedPass = rfc.GetBytes(_keySize / 8);

            byte[] decrypted;
            int decryptedCount;

            try
            {
                using ICryptoTransform decryptor = Cipher.CreateDecryptor(derivedPass, iv);
                using MemoryStream from = new MemoryStream(encryptedData);
                using CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read);
                
                   

                using MemoryStream decryptedStream = new MemoryStream();
                reader.CopyTo(decryptedStream); // Let the stream handle buffer size automatically
                decrypted = decryptedStream.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Decryption failed: {ex.Message}");
                return string.Empty;
            }

            Cipher.Clear();
            return encoding.GetString(decrypted, 0, decrypted.Length);
        }

        public static T ToAnyType<T>(this object txt)
        {

            try
            {
                if (txt is not null)
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    return (T)(converter.ConvertFromInvariantString(txt.ToString()));
                }
                return default(T);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
