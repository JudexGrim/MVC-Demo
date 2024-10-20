using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MVC.Controllers
{
    public class BaseController : Controller
    {
        protected IConfiguration _configuration;
        
        public JsonResult createresponse( bool IsSuccess,string Message ="",object Data= null)
        {
            return Json(new { IsSuccess, Message, Data}); 
        }

        public string GetBaseURl()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            return baseUrl;
        }
    }
}
