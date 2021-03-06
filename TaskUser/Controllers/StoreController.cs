﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TaskUser.Filters;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.Store;

namespace TaskUser.Controllers
{
//    [ServiceFilter(typeof(ActionFilter))]  
    [Authorize]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly SharedViewLocalizer<CommonResource> _localizer;
        public StoreController(IStoreService storeService,SharedViewLocalizer<CommonResource> localizer)
        {
            _storeService = storeService;
            _localizer = localizer;
           

        }
        
        /// <summary>
        /// show index of store
        /// </summary>
        /// <returns>return view</returns>
        public async Task<IActionResult> Index()
        {
            var listStore = await _storeService.GetStoreListAsync();
            return View(listStore);

        }
        
        /// <summary>
        /// get create  store
        /// </summary>
        /// <returns>view create of store</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        /// <summary>
        /// post create  store
        /// </summary>
        /// <param name="store">StoreViewModels</param>
        /// <returns>index of store else view</returns>
        [HttpPost]
        public async Task<IActionResult> Create(StoreViewModels store)
        {
            if (ModelState.IsValid)
            {
                var addStore = await _storeService.AddStoreAsync(store);
                if (addStore)
                {
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_AddSuccessfuly").ToString();
                    return RedirectToAction("Index");
                }
                ViewData["Failure"] = _localizer.GetLocalizedString("err_AddFailure");
                Log.Error("Add store error ");
                return View(store);
            }
            Log.Error("Add store error ");
            ViewData["Failure"] = _localizer.GetLocalizedString("err_AddFailure");
            return View(store);
        }
        
        /// <summary>
        /// get edit  store
        /// </summary>
        /// <param name="id">StoreViewModels</param>
        /// <returns>view edit of stroe</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int?id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var getstore =await _storeService.GetIdStoreAsync(id.Value);
           
            return View(getstore);
        }
        
        /// <summary>
        /// post edit  store
        /// </summary>
        /// <param name="editStore">StoreViewModels</param>
        /// <returns>index of store else view</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(StoreViewModels editStore)
        {
           
            if (ModelState.IsValid)
            {
                var store= await _storeService.EditStoreAsync(editStore);
                if (store)
                {
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_EditSuccessfuly").ToString();
                    return RedirectToAction("Index");
                }
                ViewData["Failure"] = _localizer.GetLocalizedString("err_EditFailure");
                Log.Error("Edit store error");
                return View(editStore);
            }
            Log.Error("Edit store error");
            return View(editStore);
        }
        
        /// <summary>
        /// delete  store
        /// </summary>
        /// <param name="id">StoreViewModels</param>
        /// <returns>index</returns>
        [Authorize(Roles= "Admin") ]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var rmStore=await _storeService.Delete(id.Value);
            if (rmStore)
            {
                TempData["Successfuly"] = _localizer.GetLocalizedString("msg_DeleteSuccessfuly").ToString();
                return RedirectToAction("Index"); 
            }
            TempData["Failure"] = _localizer.GetLocalizedString("err_DeleteFailure").ToString();
            Log.Error("Delete store error");
            return RedirectToAction("Index");
            
        }
    }
}