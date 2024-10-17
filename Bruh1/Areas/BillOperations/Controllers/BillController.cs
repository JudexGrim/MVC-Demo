using ViewModels.Bills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVC.Areas.ItemOperations.Controllers;
using MVC.Attributes;
using ProviderLayer.Processors;

namespace MVC.Areas.BillOperations.Controllers
{
    [OOAuthorizeAttribute]
    [Area("BillOperations")]
    public class BillController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        public readonly BillProcessor _Processor = new BillProcessor();

        public async Task<IActionResult> Index()
        {
            var result = await _Processor.GetAll();

            int[] idArray = result.ReturnData as int[];

            int maxID = idArray[0];
            int maxDetailID = idArray[1];

            maxID++;
            maxDetailID++;

            ViewBag.maxID = maxID;
            ViewBag.maxDetailID = maxDetailID;

            return View(result.Item1);
        }

        public IActionResult HeaderSlice(BillHeader model)
        {
            return PartialView(model);
        }

        public IActionResult DetailSlice(BillDetail model)
        {
            return PartialView(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
