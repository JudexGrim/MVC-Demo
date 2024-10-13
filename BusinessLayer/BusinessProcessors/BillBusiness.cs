
using Dapper;
using DataAccessLayer;
using EntityModels.Bills;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessProcessors
{
    class BillBusiness : IBusiness<BillHeader>
    {
        
        public async Task<(IEnumerable<BillHeader>, int maxID)> GetAll()
        {
            using (DAL DB = new DAL())
            {
                DB._params = new DynamicParameters();
                DB._params.Add("@maxID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var queryResult = await DB.ExecQuery<BillHeader>("ViewAllItems");
                var maxID = DB._params.Get<int>("@maxID");

                return (queryResult, maxID);
            }
        }

        public async Task<(bool success, int ID)> Update(BillHeader parameters)
        {
            using DAL DB = new DAL();
            DB._params = new DynamicParameters(parameters);
            DB._params.Add("@ID", parameters.ID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            DB._params.Add("@ClientID", parameters.ClientID);
            DB._params.Add("@BillDate", parameters.BillDate);
            DB._params.Add("@CreatedTime", parameters.BillDate);

            foreach (var item in parameters.Details)
            {
                DB._params.Add("@DetailID", item.ID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                DB._params.Add("@ItemID", item.ItemID);
                DB._params.Add("@HeaderID", item.HeaderID);
                DB._params.Add("@Price", item.Price);
                DB._params.Add("@Total", item.Amount);
                await DB.ExecNonQuery("BillDetail_CreateEdit");
            }
            
            bool rows = await DB.ExecNonQuery("Items_CreateEdit") != 0;
            int ID = DB._params.Get<int>("@ID");
            return (rows, ID);
        }

        public async Task<int> Delete(int id)
        {
            using DAL DB = new DAL();
            return await DB.ExecNonQuery($"EXEC Item_Delete @ID = {id}");
        }
    }
}
