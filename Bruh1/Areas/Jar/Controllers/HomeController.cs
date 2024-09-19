using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Jar.Controllers
{
    [Area("Jar")] 
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
