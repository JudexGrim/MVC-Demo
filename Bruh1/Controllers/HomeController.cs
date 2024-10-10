using Microsoft.AspNetCore.Mvc;
using ProviderLayer.Processors;
using System.Diagnostics;
using ViewModels;

namespace MVC.Controllers
{
 
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ConfirmDelete(int id)
        {
            if (ModelState.IsValid)
                ViewBag.id = id;
            return PartialView();
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public IActionResult LoginFailed()
        {
            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
