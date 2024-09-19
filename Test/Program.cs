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

            var items = await ITem.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine($"{item.ID} | {item.Name} | {item.Price}");
            }
           
        }
    }
}
