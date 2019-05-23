using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskUser.Filters;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.Stock;

namespace TaskUser.Controllers
{
//    [ServiceFilter(typeof(ActionFilter))
//     [Authorize(Roles= "Admin")]
    [Authorize]
    public class StockController : Controller
    {
        /// <summary>
        /// Isevice
        /// </summary>
        private readonly IStockService _stockService;
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;
        private readonly SharedViewLocalizer<CommonResource> _localizer;
        private readonly SharedViewLocalizer<StockResource> _stockLocalizer;
        public StockController(IStockService stockService,
            IStoreService storeService,
            IProductService productService,
            SharedViewLocalizer<CommonResource> localizer,
            SharedViewLocalizer<StockResource> stockLocalizer
        )
        {
            _stockService = stockService;
            _storeService = storeService;
            _productService = productService;
            _localizer = localizer;
            _stockLocalizer = stockLocalizer;
        }
        
        
        /// <summary>
        /// show index tock
        /// </summary>
        /// <returns>index of stock</returns>
        public async Task<IActionResult> Index()
        {
            var listStock = await _stockService.GetStockListAsync();
            return View(listStock);
        }
        
        /// <summary>
        /// get create stock
        /// </summary>
        /// <returns>view create of stock</returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.StoreId = new SelectList(_storeService.GetStore(), "Id", "StoreName");
            ViewBag.ProductID = new SelectList(_productService.GetProduct(), "Id", "ProductName");
            return View();
        }
        
        /// <summary>
        /// post create of stock
        /// </summary>
        /// <param name="stock">StockViewModels</param>
        /// <returns>view index of stock else view(stock)</returns>
        [HttpPost]
        public async Task<IActionResult> Create(StockViewModels stock)
        {
            if (ModelState.IsValid)
            {
                var addStock = await _stockService.AddStockAsync(stock);
                if (addStock)
                {
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_AddSuccessfuly").ToString();
                    return RedirectToAction("Index");
                }
                ViewData["Failure"] = _localizer.GetLocalizedString("err_AddFailure");
                ViewBag.StoreId = new SelectList(_storeService.GetStore(), 
                    "Id", "StoreName",stock.StoreId);
                ViewBag.ProductID = new SelectList(_productService.GetProduct(), 
                    "Id", "StoreName",stock.ProductId);
                return View(stock);
            }
            
            ViewBag.StoreId = new SelectList(_storeService.GetStore(), 
                "Id", "StoreName",stock.StoreId);
            ViewBag.ProductID = new SelectList(_productService.GetProduct(), 
                "Id", "StoreName",stock.ProductId);
            return View(stock);
        }
        
        /// <summary>
        /// get edit stock
        /// </summary>
        /// <param name="productId">StockViewModels</param>
        /// <param name="storeId">StockViewModels</param>
        /// <returns>return edit of stock</returns>
        [HttpGet]
        public  async Task<IActionResult> Edit(int?  productId,int? storeId)
        {
            if (productId == null || storeId == null) 
                return BadRequest();
            var getStock = await _stockService.GetIdStockAsync(productId.Value, storeId.Value);
            ViewBag.StoreId = new SelectList(_storeService.GetStore(), "Id", "StoreName");
            ViewBag.ProductID = new SelectList(_productService.GetProduct(), "Id", "ProductName");
           
            return View(getStock);


        }
        
        /// <summary>
        /// post edit stock
        /// </summary>
        /// <param name="editStock">StockViewModels</param>
        /// <returns>view index of edir</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(StockViewModels editStock)
        {
           
            if (ModelState.IsValid)
            {
                var product= await _stockService.EditStockAsync(editStock);
                if (product)
                {
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_EditSuccessfuly").ToString();
                    return RedirectToAction("Index");
                }
                ViewData["Failure"] = _localizer.GetLocalizedString("err_EditFailure");
                ViewBag.StoreId = new SelectList(_storeService.GetStore(), "Id", "StoreName");
                ViewBag.ProductID = new SelectList(_productService.GetProduct(), "Id", "ProductName");
                return View(editStock);
               
            }
            
            ViewBag.StoreId = new SelectList(_storeService.GetStore(), "Id", "StoreName");
            ViewBag.ProductID = new SelectList(_productService.GetProduct(), "Id", "ProductName");
            return View(editStock);
        }
        
        /// <summary>
        /// delete stock
        /// </summary>
        /// <param name="productId">StockViewModels</param>
        /// <param name="storeId">StockViewModels</param>
        /// <returns>index</returns>
        [Authorize(Roles= "Admin") ]
        [HttpGet]
        public async Task<IActionResult> Delete(int? productId,int? storeId)
        {
            if (productId ==null || storeId ==null )
            {
                return BadRequest();
            }
           var rmProduct = await _stockService.Delete(productId.Value,storeId.Value);
            if (rmProduct)
            {
                TempData["Successfuly"] = _localizer.GetLocalizedString("msg_DeleteSuccessfuly").ToString();
                return RedirectToAction("Index");
            }
            TempData["Failure"] = _localizer.GetLocalizedString("err_DeleteFailure").ToString();
            return RedirectToAction("Index");
            
            
           
        }
        
    }
}