using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskUser.Models;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.Brand;

namespace TaskUser.Controllers
{
//    [ServiceFilter(typeof(ActionFilter))]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly DataContext _context;
        private readonly SharedViewLocalizer<CommonResource> _localizer;
        private readonly SharedViewLocalizer<BrandResource> _brandLocalizer;
        public BrandController(IBrandService  brandService,SharedViewLocalizer<CommonResource> localizer,SharedViewLocalizer<BrandResource> brandLocalizer,DataContext context)
        {
            _brandService = brandService;
            _brandLocalizer = brandLocalizer;
            _localizer = localizer;
            _context = context;

        }
        
   
        /// <summary>
        /// show index brand
        /// </summary>
        /// <returns>viewbrand</returns>
    
        public async Task<IActionResult> Index()
        {
            var listBrand = await _brandService.GetBranListAsync();
            return View(listBrand);

        }
        

        
        /// <summary>
        /// get create brand
        /// </summary>
        /// <returns>views create Brand</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
       /// <summary>
       /// Post create brand
       /// </summary>
       /// <param name="brand">BrandViewsModels</param>
       /// <returns>index else View</returns>
        [HttpPost]
        public async Task<IActionResult> Create(BrandViewsModels brand)
        {
            if (ModelState.IsValid)
            {
                var addBrand = await _brandService.AddBrandAsync(brand);
                if (addBrand)
                {
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_AddSuccessfuly").ToString();
                    return RedirectToAction("Index");
                }
                ViewData["EditFailure"] = _localizer.GetLocalizedString("err_AddFailure");
                return View(brand);
            }
            return View(brand);
        }
        
        /// <summary>
        /// get edit brand
        /// </summary>
        /// <param name="id">BrandViewModel</param>
        /// <returns>view edit </returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var getBrand = await _brandService.GetIdbrandAsync(id.Value);
           
            return View(getBrand);
        }
        
        /// <summary>
        /// Post Edit Brand
        /// </summary>
        /// <param name="editBrand">BrandViewsModels</param>
        /// <returns>brand index</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(BrandViewsModels editBrand)
        {
            if (ModelState.IsValid)
            {
                var brand = await  _brandService.EditBrandAsync(editBrand);
                if (brand)
                {
                       TempData["Successfuly"] = _localizer.GetLocalizedString("msg_EditSuccessfuly").ToString();
                    return RedirectToAction("Index");
                
                }
                ViewData["EditFailure"] = _localizer.GetLocalizedString("err_EditFailure");
                return View(editBrand);
            }
            return View(editBrand);
        }
        
        /// <summary>
        /// get delete
        /// </summary>
        /// <param name="id">Brand</param>
        /// <returns>index brand</returns>
        [Authorize(Roles= "Admin") ]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            var brand = await _brandService.Delete(id.Value);
            if (brand)
            {
                TempData["Successfuly"] = _localizer.GetLocalizedString("msg_DeleteSuccessfuly").ToString();
                return RedirectToAction("Index");
            }
            TempData["EditFailure"] = _localizer.GetLocalizedString("err_Failure").ToString();
            return RedirectToAction("Index");
        }
    }
}