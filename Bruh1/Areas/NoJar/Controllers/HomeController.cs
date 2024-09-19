using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.NoJar.Controllers
{
    [Area("NoJar")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
