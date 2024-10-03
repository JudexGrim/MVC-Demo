using ProviderLayer;
using CoreLib;
using ViewModels;
using ProviderLayer.Processors;
namespace Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ClientProcessor Client = new ClientProcessor();
            ItemProcessor Item = new ItemProcessor();

            await Client.Update(new Client { ID = 2, Name = "bruhbruh", Type = "Seller" });
           
        }
    }
}
