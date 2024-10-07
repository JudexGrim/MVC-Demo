using ProviderLayer;
using CoreLib;
using ViewModels;
using ProviderLayer.Processors;
using Microsoft.IdentityModel.Tokens;
using CoreLib.Security.Cryptography;
namespace Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var A = "asdasd";
            var encryptedString = A.AesEncrypt();    
            var decryptedString = encryptedString.Decrypt<string>(); 
            Console.WriteLine($"Before: {A}");
            Console.WriteLine($"Encrypted: {encryptedString}");
            Console.WriteLine($"Unencrypted: {decryptedString}");
        }
    }
}
