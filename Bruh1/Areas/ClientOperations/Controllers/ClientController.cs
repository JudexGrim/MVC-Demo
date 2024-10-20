using Microsoft.AspNetCore.Mvc;
using MVC.Attributes;
using MVC.Controllers;
using ProviderLayer.Processors;
using ViewModels;

namespace MVC.Areas.ClientOperations.Controllers
{
    [Area("ClientOperations")]
    [OOAuthorize]
    public class ClientController : BaseController
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index() 
        {
            using ClientProcessor clientProvider = new ClientProcessor(_configuration);
            var queryReturn = await clientProvider.GetAll();
            var clientModels = queryReturn.Item1;
            int maxID = (int)queryReturn.ReturnData;
            maxID++;
            ViewBag.maxID = maxID;
            return View(clientModels);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submission(Client model)
        {
            if (ModelState.IsValid)
            {
                using ClientProcessor clientProvider = new ClientProcessor(_configuration);
                var updateReturn = await clientProvider.Update(model);
                bool isSuccess = updateReturn.success;
                int maxID = (int)updateReturn.ReturnData;

                return createresponse(isSuccess, "Done.");
            }
            return createresponse(false, "Form Invalid");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                using ClientProcessor clientProvider = new ClientProcessor(_configuration);
                bool isSuccess = await clientProvider.Delete(id);
                return createresponse(isSuccess, "Item Deleted");
            }
            else return createresponse(false, "Delete Form Invalid.");
        }

        [HttpPost]
        public IActionResult ClientSlice(Client model)
        {
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}

