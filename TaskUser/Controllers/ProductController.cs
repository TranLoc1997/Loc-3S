﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.Product;

namespace TaskUser.Controllers
{
//    [ServiceFilter(typeof(ActionFilter))]
//    [Authorize(Roles= "Admin") ]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly SharedViewLocalizer<CommonResource> _localizer;
        public ProductController(IProductService productService,
            IBrandService brandService,
            ICategoryService categoryService,
            SharedViewLocalizer<CommonResource> localizer,
            SharedViewLocalizer<ProductResource> productLocalizer)
        {
            
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _localizer = localizer;
            

        }
        
        
        /// <summary>
        /// show index product    
        /// </summary>
        /// <returns>view index of product</returns>
        public async Task<IActionResult> Index()
        {
            var listStore = await _productService.GetProductListAsync();
            return View(listStore);
        }
        
        /// <summary>
        /// get create  product
        /// </summary>
        /// <returns>view create of product</returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.BrandId = new SelectList(_brandService.Getbrand(), "Id", "BrandName");  
            ViewBag.CategoryId = new SelectList(_categoryService.GetCategory(), "Id", "CategoryName");  
            return View();
        }
        
        /// <summary>
        /// post create of product
        /// </summary>
        /// <param name="product">ProductViewsModels</param>
        /// <returns>return view index of product</returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewsModels product)
        {
            if (ModelState.IsValid)
            {
                var addProduct = await _productService.AddProductAsync(product);
                if (addProduct)
                { 
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_AddSuccessfuly").ToString();
                    return RedirectToAction("Index");
                }
                ViewData["Failure"] = _localizer.GetLocalizedString("err_AddFailure");
                ViewBag.CategoryId = new SelectList(_categoryService.GetCategory(), "Id", "CategoryName",product.CategoryId);  
                ViewBag.BrandId = new SelectList(_brandService.Getbrand(), "Id", "BrandName",product.BrandId);
                Log.Error("Add Product error ");
                return View(product);
            }
            ViewBag.CategoryId = new SelectList(_categoryService.GetCategory(), "Id", "CategoryName",product.CategoryId);  
            ViewBag.BrandId = new SelectList(_brandService.Getbrand(), "Id", "BrandName",product.BrandId);
            Log.Error("Add Product error ");
            return View(product);
        }
        
        /// <summary>
        /// get edit product
        /// </summary>
        /// <param name="id">ProductViewsModels</param>
        /// <returns>view edit of product</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            ViewBag.BrandId = new SelectList(_brandService.Getbrand(), "Id", "BrandName");  
            ViewBag.CategoryId = new SelectList(_categoryService.GetCategory(), "Id", "CategoryName");  
            var getProduct = await _productService.GetIdProductAsync(id.Value);
            return View(getProduct);
        }
        
        /// <summary>
        /// post edit product
        /// </summary>
        /// <param name="editProduct">ProductViewsModels</param>
        /// <returns>view index of product</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewsModels editProduct)
        {
           
            if (ModelState.IsValid)
            {
                var product= await _productService.EditProductAsync(editProduct);
                if (product)
                {
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_EditSuccessfuly").ToString();
                    return RedirectToAction("Index");  
                }
                ViewData["Failure"] = _localizer.GetLocalizedString("err_EditFailure");
                ViewBag.CategoryId = new SelectList(_categoryService.GetCategory(), 
                    "Id", "CategoryName",editProduct.CategoryId);  
                ViewBag.BrandId = new SelectList(_brandService.Getbrand(), 
                    "Id", "BrandName",editProduct.BrandId);
                Log.Error("Edit Product error ");
                return View(editProduct);
            }
            ViewBag.CategoryId = new SelectList(_categoryService.GetCategory(), 
                "Id", "CategoryName",editProduct.CategoryId);  
            ViewBag.BrandId = new SelectList(_brandService.Getbrand(), 
                "Id", "BrandName",editProduct.BrandId);
            Log.Error("Edit Product error ");
            return View(editProduct);
        }
        
        /// <summary>
        /// get delete  product
        /// </summary>
        /// <param name="id">ProductViewsModels</param>
        /// <returns>delete  product</returns>
        [Authorize(Roles= "Admin") ]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var rmProduct=await _productService.Delete(id.Value);
            if (rmProduct)
            {
                TempData["Successfuly"] = _localizer.GetLocalizedString("msg_DeleteSuccessfuly").ToString();
                return RedirectToAction("Index");
            }
            TempData["Failure"] = _localizer.GetLocalizedString("err_DeleteFailure").ToString();
            Log.Error("Delete Product error ");
            return RedirectToAction("Index");
        }
    }
}