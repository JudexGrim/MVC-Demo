using BusinessLayer.BusinessProcessors;
using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProviderLayer.Processors
{
    public class ClientProcessor : Disposer, IProviderProcessor<Client>
    {
        private IConfiguration _configuration;

        public ClientProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<(IEnumerable<Client>, object ReturnData)> GetAll()
        {
            using (ClientBusiness Processor = new ClientBusiness(_configuration))
            {

                var queryResult = await Processor.GetAll();
                var entityClients = queryResult.Item1;
                var viewClients = from r in entityClients
                            select new Client
                            {

                                ID = r.ID,
                                Name = r.Name,
                                Type = r.Type,
                            };
                
                return (viewClients, queryResult.ReturnData);
            }
        }


		public async Task<(bool success, object ReturnData)> Update(Client parameters)
        {
            using ClientBusiness Processor = new ClientBusiness(_configuration);
            var EntityClient = new EntityModels.Client
                                {
                                    ID = parameters.ID,
                                    Name = parameters.Name,
                                    Type = parameters.Type
                                };
            return await Processor.Update(EntityClient);
            
        }

		public async Task<bool> Delete(int id)
		{
			using ClientBusiness Processor = new ClientBusiness(_configuration);
            bool success = await Processor.Delete(id) != 0;
            return success;
		}
	}
}
