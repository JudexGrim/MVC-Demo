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
    public class AuthenticationProcessor : Disposer
    {
        private IConfiguration _configuration;

        public AuthenticationProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<(bool success, string message, User? userModel)> TryLogin(LoginViewModel login)
        {
            using UserBusiness userBusiness = new UserBusiness(_configuration);
            try
            {
                var fetchedUser = await userBusiness.FetchUser(login.User);

                var viewUser = new User 
                {
                    ID = fetchedUser.ID,
                    Email = fetchedUser.Email, 
                    Username = fetchedUser.Username,
                    Password = fetchedUser.Password
                };

                if (viewUser.Password.Equals(login.Password) )
                {
                    return (true, "Successful Login", viewUser);
                }
                else return (false, "The password is Wrong", viewUser);
            }

            catch (Exception ex) { return (false, $"An Error Occured: {ex}", null); }
        }
    }
}
