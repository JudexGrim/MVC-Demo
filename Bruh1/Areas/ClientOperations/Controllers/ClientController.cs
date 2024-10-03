using Microsoft.AspNetCore.Mvc;
using MVC.Controllers;
using ProviderLayer.Processors;
using ViewModels;

namespace MVC.Areas.ClientOperations.Controllers
{
    [Area("ClientOperations")]
    public class ClientController : BaseController
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IProviderProcessor<Client> _clientProvider = new ClientProcessor();

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index() 
        {
            var queryReturn = await _clientProvider.GetAll();
            var clientModels = queryReturn.Item1;
            int maxID = queryReturn.maxID;
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
                var updateReturn = await _clientProvider.Update(model);
                bool isSuccess = updateReturn.success;
                int maxID = updateReturn.ID;

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
                bool isSuccess = await _clientProvider.Delete(id);
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

