using System;
using DataAccessLayer;
using EntityModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;
using System.Data;
using Dapper;

namespace BusinessLayer.BusinessProcessors
{
    public class ClientBusiness : Disposer, IBusiness<Client>
    {
        public async Task<(IEnumerable<Client>, object ReturnData)> GetAll()
        {

            using DAL DB = new DAL();

            DB._params = new DynamicParameters();
            DB._params.Add("@maxID", dbType: DbType.Int32, direction:ParameterDirection.Output);
            var queryResult = await DB.ExecQuery<Client>("ViewAllClients");
            int maxID = DB._params.Get<int>("@maxID");
            return (queryResult, maxID);
        }

        public async Task<(bool success, object ReturnData)> Update(Client parameters)        
        {
            using DAL DB = new DAL();
            DB._params = new DynamicParameters();
            DB._params.Add("@ID", parameters.ID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            DB._params.Add("@Type", parameters.Type);
            DB._params.Add("@Name", parameters.Name);
            bool success = await DB.ExecNonQuery("Clients_CreateEdit") != 0;
            var ID = DB._params.Get<int>("@ID");

            return (success, ID);
            
        }

         public async Task<int> GetMaxID(string tableName)
        {
            using (DAL DB = new DAL())
            {
                return await DB.ExecScalar<int>($"EXEC GetMaxID '{tableName}'");
            }
        }

		public async Task<int> Delete(int id)
		{
			using DAL DB = new DAL();
            return await DB.ExecNonQuery($"EXEC Client_Delete @ID = {id}");
		}
	}
}
