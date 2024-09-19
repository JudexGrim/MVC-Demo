using DataAccessLayer;
using EntityModels;
using CoreLib;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessProcessors
{
    public class ItemBusiness : Disposer,IBusiness<Item>
    {
        public async Task<IEnumerable<Item>> GetAll()
        {
            using (DAL DB = new DAL())
            {
                return await DB.ExecQuery<Item>("ViewAllItems");
            }
        }

        public async Task<int> Update(Item parameters)
        {
            using DAL DB = new DAL();
            DB._params = new DynamicParameters(parameters);
            DB._params.Add("@ID", parameters.ID);
            DB._params.Add("@Name", parameters.Name);
            DB._params.Add("@Price", parameters.Price);
            return await DB.ExecNonQuery("Items_CreateEdit");
            
        }

        public async Task<T> GetItemStock<T>(int id)
        {
            using (DAL DB = new DAL())
            {
                return await DB.ExecScalar<T>($"GetITemStock @ItemID = {id}");
            }
        }
    }
}
