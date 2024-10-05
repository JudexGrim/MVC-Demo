using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MVC.Controllers
{
    public class BaseController : Controller
    { 
        public JsonResult createresponse( bool IsSucess,string Message ="",object Data= null)
        {
            return Json(new { IsSucess, Message, Data}); 
        }
    }
}
