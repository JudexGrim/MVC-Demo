using DataAccessLayer;
using EntityModels;
using CoreLib;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLayer.BusinessProcessors
{
    public class ItemBusiness : Disposer, IBusiness<Item>
    {
        public async Task<(IEnumerable<Item>, object ReturnData)> GetAll()
        {
            using (DAL DB = new DAL())
            {
                DB._params = new DynamicParameters();
                DB._params.Add("@maxID", dbType:DbType.Int32, direction:ParameterDirection.Output);

                var queryResult = await DB.ExecQuery<Item>("ViewAllItems");
                var maxID = DB._params.Get<int>("@maxID");

                return (queryResult, maxID);
            }
        }

        public async Task<(bool success, object ReturnData)> Update(Item parameters)
        {
            using DAL DB = new DAL();
            DB._params = new DynamicParameters(parameters);
            DB._params.Add("@ID", parameters.ID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            DB._params.Add("@Name", parameters.Name);
            DB._params.Add("@Price", parameters.Price);
            bool rows = await DB.ExecNonQuery("Items_CreateEdit") !=0;
            int ID = DB._params.Get<int>("@ID");
            return (rows, ID);
        }

        public async Task<T> GetItemStock<T>(int id)
        {
            using (DAL DB = new DAL())
            {
                return await DB.ExecScalar<T>($"GetITemStock @ItemID = {id}");
            }
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
            return await DB.ExecNonQuery($"EXEC Item_Delete @ID = {id}");
        }
    }
}
