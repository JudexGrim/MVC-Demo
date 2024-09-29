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
            // var model = await _clientProvider.GetAll();
            var model = new ViewModels.Bills.Bill();
            return View(model);
        }

        public IActionResult Submission()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Submission(Client model)
        {
            if (ModelState.IsValid)
            {
                await _clientProvider.Update(model);
                return View();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}

