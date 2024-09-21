using Microsoft.AspNetCore.Mvc;
using ProviderLayer.Processors;
using ViewModels;

namespace MVC.Areas.ClientOperations.Controllers
{
    [Area("ClientOperations")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IProviderProcessor<Client> _clientProvider = new ClientProcessor();

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        public IActionResult Submission()
        {
            return View();
        }

        public async Task<IActionResult> ViewAll() 
        {
            var Clients = await _clientProvider.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submission(Client model)
        {
            if (ModelState.IsValid)
            {
                await _clientProvider.Update(model);
                TempData["SuccessMessage"] = "Your Request was Successfully Processed!";
                return View();
            }
            TempData["FailedMessage"] = "Invalid Info. Try Again.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}

