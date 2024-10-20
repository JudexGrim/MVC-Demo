using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;
using DataAccessLayer;
using System.Data;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using CoreLib;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.BusinessProcessors
{
    public class UserBusiness : Disposer, IBusiness<User>
    {
        private readonly string _connectionString;

        public UserBusiness(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<(IEnumerable<User>, object ReturnData)> GetAll()
        {
            using DAL db = new DAL(_connectionString);
            db._params = new Dapper.DynamicParameters();
            db._params.Add("@maxID",dbType: DbType.Int32, direction: ParameterDirection.Output);
            var result = await db.ExecQuery<User>("ViewAllUsers");
            int maxID =  db._params.Get<int>("@maxID");
            return (result, maxID);
        }

        public async Task<(bool success, object ReturnData)> Update(User parameters)
        {
            using DAL db = new DAL(_connectionString);
            db._params = new Dapper.DynamicParameters(parameters);
            
            db._params.Add("@ID", parameters.ID);
            db._params.Add("@Username", parameters.Username);
            db._params.Add("@Email", parameters.Email);
            db._params.Add("@Password", parameters.Password);
            

            var result = await db.ExecNonQuery("User_Edit");

            bool success = result != 0;
            int maxID = db._params.Get<int>("@ID");
            return (success, maxID);
        }

        public async Task<int> Delete(int id)
        {
            using DAL db = new DAL(_connectionString);
            return await db.ExecNonQuery($"EXEC User_Delete @ID = {id}");
        }

        public async Task<(bool success, int maxID)> Create(User parameters)
        {
            using DAL db = new DAL(_connectionString);
            db._params = new Dapper.DynamicParameters();

            db._params.Add("@ID", parameters.ID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            db._params.Add("@Username", parameters.Username);
            db._params.Add("@Email", parameters.Email);
            db._params.Add("@Password", parameters.Password);

            var result = await db.ExecNonQuery("User_Create");

            bool success = result != 0;
            int maxID = db._params.Get<int>("@ID");

            return (success, maxID);
        }
        
        public async Task<User> FetchUser(string user)
        {
            using DAL db = new DAL(_connectionString);
            var result = await db.ExecQuery<User>($"EXEC Fetch_User @User = '{user}'");
            return result.FirstOrDefault();
        }
    }
}
