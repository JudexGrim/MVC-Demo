using Microsoft.AspNetCore.Mvc;

namespace MVC.Helpers
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        
            if (_httpContextAccessor != null)
            {
                CurrentUser.SetUser(_httpContextAccessor.HttpContext.User);
            }
        }
    }
}
