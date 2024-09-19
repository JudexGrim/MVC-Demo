using System;
using DataAccessLayer;
using EntityModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;
using System.Data;

namespace BusinessLayer.BusinessProcessors
{
    public class ClientBusiness : Disposer, IBusiness<Client>
    {
        public async Task<IEnumerable<Client>> GetAll()
        {

            using DAL DB = new DAL();
            return await DB.ExecQuery<Client>("ViewAllClients");

        }

        public async Task<int> Update(Client parameters)        
        {
            using DAL DB = new DAL();
            DB._params = new Dapper.DynamicParameters(parameters);
            DB._params.Add("@ID", parameters.ID);
            DB._params.Add("@Type", parameters.Type);
            DB._params.Add("@Name", parameters.Name);
            return await DB.ExecNonQuery("Clients_CreateEdit");
            
        }
    }
}
