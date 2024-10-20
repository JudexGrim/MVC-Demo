using BusinessLayer.BusinessProcessors;
using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ViewModels;

namespace ProviderLayer.Processors
{
    public class UserProcessor : Disposer, IProviderProcessor<User>
    { 
        private IConfiguration _configuration;

        public UserProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<(IEnumerable<User>, object ReturnData)> GetAll()
        {
            using UserBusiness userBusiness = new UserBusiness(_configuration);
            var result = await userBusiness.GetAll();
            var userViewModels = from r in result.Item1
                                 select new User
                                 {
                                     ID = r.ID,
                                     Username = r.Username,
                                     Email = r.Email,
                                     Password = r.Password
                                 };
            return (userViewModels, result.ReturnData);
        }

        public async Task<(bool success, object ReturnData)> Update(User parameters)
        {
            var entityUser = new EntityModels.User 
            {
                ID= parameters.ID,
                Username = parameters.Username,
                Email = parameters.Email,
                Password = parameters.Password,
            };
            using UserBusiness userBusiness = new UserBusiness(_configuration);
            var result = await userBusiness.Update(entityUser);
           
            return (result.success, (int)result.ReturnData);
        }

        public async Task<(bool success, int ID)> Create(User parameters)
        {
            var entityUser = new EntityModels.User
            {
                ID = parameters.ID,
                Username = parameters.Username,
                Email = parameters.Email,
                Password = parameters.Password,
            };
            using UserBusiness userBusiness = new UserBusiness(_configuration);
            var result = await userBusiness.Create(entityUser);

            return (result.success, result.maxID);
        }

        public async Task<bool> Delete(int id)
        {
            using UserBusiness userBusiness = new UserBusiness(_configuration);
            return await userBusiness.Delete(id) !=0;
        }

        public async Task<User> FetchUser(string user)
        {
            using UserBusiness userBusiness=new UserBusiness(_configuration);
            var result = await userBusiness.FetchUser(user);
            var UserViewModel = new User
            {
                ID = result.ID,
                Username = result.Username,
                Email = result.Email,
                Password = result.Password
            };
            return (UserViewModel);
        }
    }
}
