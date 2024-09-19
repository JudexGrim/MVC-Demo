using Microsoft.AspNetCore.Mvc;
using ProviderLayer.Processors;
using ViewModels;

namespace MVC.Areas.ItemOperations.Controllers
{
    [Area("ItemOperations")]
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IProviderProcessor<Item> _itemProvider = new ItemProcessor();

        public ItemController(ILogger<ItemController> logger)
        {
            _logger = logger;
        }

        public IActionResult Submission()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submission(Item model)
        {
            if (ModelState.IsValid)
            {
                await _itemProvider.Update(model);
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
