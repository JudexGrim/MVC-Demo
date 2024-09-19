using BusinessLayer.BusinessProcessors;
using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels;
using System.Threading.Tasks;

namespace ProviderLayer.Processors
{
    public class ClientProcessor : Disposer, IProviderProcessor<Client>
    {
        public async Task<IEnumerable<Client>> GetAll()
        {
            using (ClientBusiness Processor = new ClientBusiness())
            {

                var entityClients = await Processor.GetAll();

                var viewClients = from r in entityClients
                            select new Client
                            {

                                ID = r.ID,
                                Name = r.Name,
                                Type = r.Type,
                            };

                return viewClients;
            }
        }

        public async Task<int> Update(Client parameters)
        {
            using ClientBusiness Processor = new ClientBusiness();
            var EntityClient = new EntityModels.Client
                                {
                                    ID = parameters.ID,
                                    Name = parameters.Name,
                                    Type = parameters.Type
                                };
            return await Processor.Update(EntityClient);
            
        }
    }
}
