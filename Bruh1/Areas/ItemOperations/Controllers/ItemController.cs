﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Attributes;
using MVC.Controllers;
using ProviderLayer.Processors;
using System.Reflection;
using System.Runtime.CompilerServices;
using ViewModels;

namespace MVC.Areas.ItemOperations.Controllers
{
    [Area("ItemOperations")]
    [OOAuthorizeAttribute]
    public class ItemController : BaseController
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IProviderProcessor<Item> _itemProvider = new ItemProcessor();

        public ItemController(ILogger<ItemController> logger)
        {
            _logger = logger;

        }
         
        public async Task<IActionResult> Index()
        {
            var queryResult = await _itemProvider.GetAll();
            var Models = queryResult.Item1;
            var maxID = (int)queryResult.ReturnData;
            maxID++; 
            ViewBag.maxID = maxID;
            return View(Models);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
            public async Task<IActionResult> Submission(Item model)
            {
                if (ModelState.IsValid)
                {
                    var update = await _itemProvider.Update(model);
                    var maxID = update.ReturnData;
                

                    return createresponse(update.success, "Good", new {maxID, model});
                }

                return createresponse(false,"Something went wrong");
            }

        [HttpPost] 
        public IActionResult ItemSlice(Item model)
        {
            return PartialView(model);
        }
         
        public async Task<IActionResult> CreateBar()
        {
            var queryResult = await _itemProvider.GetAll();
            int maxID = (int)queryResult.ReturnData;
            maxID++;
            ViewBag.maxID = maxID;
            return PartialView();
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                bool isSucess = await _itemProvider.Delete(id);
                return createresponse(isSucess, "Item Deleted Successfully.");
            }
            return createresponse(false, "Data Validation Failed.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }


    }
}
