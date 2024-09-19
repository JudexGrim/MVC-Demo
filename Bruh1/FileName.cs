using ProviderLayer.Processors;

namespace MVC
{
    public class FileName
    {
        public async Task Test()
        {
            ClientProcessor Client = new ClientProcessor();

            var client = await Client.GetAll();
            foreach (var i in client)
            {
                Console.WriteLine($"{i.ID} | {i.Type} | {i.Name}");
            }
        }
    }
}
