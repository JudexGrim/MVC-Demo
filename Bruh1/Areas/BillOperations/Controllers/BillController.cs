using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.BillOperations.Controllers
{
    public class BillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
