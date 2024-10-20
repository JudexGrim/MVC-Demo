
using CoreLib;
using Dapper;
using DataAccessLayer;
using EntityModels.Bills;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.BusinessProcessors
{
    public class BillBusiness :Disposer,  IBusiness<BillHeader>
    {
        private readonly string _connectionString;

        public BillBusiness(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<(IEnumerable<BillHeader>, object ReturnData)> GetAll()
        {
            using (DAL DB = new DAL(_connectionString))
            {
                DB._params = new DynamicParameters();
                DB._params.Add("@maxID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                

                var queryResult = await DB.ExecQuery<BillHeader>("ViewBillHeaders");
                int maxID = DB._params.Get<int>("@maxID");
                

                return (queryResult, maxID);
            }
        }

        public async Task<(bool success, object ReturnData)> Update(BillHeader parameters)
        {
            using DAL DB = new DAL(_connectionString);
            DB._params = new DynamicParameters();

            DB._params.Add("@ID", parameters.ID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            DB._params.Add("@ClientID", parameters.ClientID);
            DB._params.Add("@Type", parameters.Type);
            DB._params.Add("@BillDate", parameters.BillDate);
            DB._params.Add("@CreatedTime", parameters.CreatedTime);

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("HeaderID", typeof(int));
            dt.Columns.Add("ItemID", typeof(int));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("Price", typeof(decimal));

            foreach (var item in parameters.Details)
            {
               dt.Rows.Add(item.ID, item.HeaderID, item.ItemID, item.Amount, item.Price);
            }

            DB._params.Add("@DetailList", dt.AsTableValuedParameter("BillDetailTVP"));


            var success = await DB.ExecNonQuery("Bill_CreateEdit") != 0;

            int maxID;
            int maxDetailID;

            try
            {
                maxID = DB._params.Get<int>("@maxID");
                maxDetailID = DB._params.Get<int>("@maxDetailID");
            }
            catch(Exception)
            {
                maxID = 0;
                maxDetailID = 0;
            }
            return (success, new { maxID = maxID, maxDetailID = maxDetailID });
        }

        public async Task<int> Delete(int id)
        {
            using DAL DB = new DAL(_connectionString);
            return await DB.ExecNonQuery($"EXEC BillHeader_Delete @ID = {id}");
        }


        public async Task<(IEnumerable<BillDetail>, object ReturnData)> GetDetails()
        {
            using (DAL DB = new DAL(_connectionString))
            {
                DB._params = new DynamicParameters();
                DB._params.Add("@maxID", dbType: DbType.Int32, direction: ParameterDirection.Output);


                var queryResult = await DB.ExecQuery<BillDetail>("ViewBillDetails");
                int maxID = DB._params.Get<int>("@maxID");


                return (queryResult, maxID);
            }
        }
    }
}
