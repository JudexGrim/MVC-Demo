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
            AuthenticationProcessor processor = new AuthenticationProcessor();
            UserProcessor userProcessor = new UserProcessor();


            var user = await userProcessor.FetchUser("Ah-Jun");
            Console.WriteLine($"{user.ID} | {user.Username} | {user.Password} | {user.Email}");
            //await processor.AttemptLogin(6);
        }
    }
}
