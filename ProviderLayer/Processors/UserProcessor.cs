using BusinessLayer.BusinessProcessors;
using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ProviderLayer.Processors
{
    public class UserProcessor : Disposer, IProviderProcessor<User>
    { 
        public async Task<(IEnumerable<User>, int maxID)> GetAll()
        {
            using UserBusiness userBusiness = new UserBusiness();
            var result = await userBusiness.GetAll();
            var userViewModels = from r in result.Item1
                                 select new User
                                 {
                                     ID = r.ID,
                                     Username = r.Username,
                                     Email = r.Email,
                                     Password = r.Password
                                 };
            return (userViewModels, result.maxID);
        }

        public async Task<(bool success, int ID)> Update(User parameters)
        {
            var entityUser = new EntityModels.User 
            {
                ID= parameters.ID,
                Username = parameters.Username,
                Email = parameters.Email,
                Password = parameters.Password,
            };
            using UserBusiness userBusiness = new UserBusiness();
            var result = await userBusiness.Update(entityUser);
           
            return (result.success, result.ID);
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
            using UserBusiness userBusiness = new UserBusiness();
            var result = await userBusiness.Create(entityUser);

            return (result.success, result.maxID);
        }

        public async Task<bool> Delete(int id)
        {
            using UserBusiness userBusiness = new UserBusiness();
            return await userBusiness.Delete(id) !=0;
        }

        public async Task<User> FetchUser(string user)
        {
            using UserBusiness userBusiness=new UserBusiness();
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
