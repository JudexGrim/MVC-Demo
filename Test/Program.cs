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
            var processor = new BillProcessor();

            var result = await processor.GetAll();

            foreach (var item in result.Item1)
            {
                Console.WriteLine($"{item.ID} | {item.ClientID}| {item.Type} | {item.CreatedTime}| {item.BillDate}");
                foreach (var detail in item.Details)
                {
                    Console.WriteLine($"    {detail.ID} | {detail.HeaderID}| {detail.ItemID} | {detail.Amount}| {detail.Price}");
                }
            }

            //await processor.Delete(1);
        }
    }
}
