using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskUser.Filters;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.Category;

namespace TaskUser.Controllers
{
//    [ServiceFilter(typeof(ActionFilter))]
//    [Authorize(Roles= "Admin") ]
    [Authorize]
    public class CategoryController : Controller
    {
       
        private readonly ICategoryService _category;
        private readonly SharedViewLocalizer<CommonResource> _localizer;
        private readonly SharedViewLocalizer<CategoryResource> _categoryLocalizer;
        public CategoryController(ICategoryService category ,SharedViewLocalizer<CommonResource> localizer,SharedViewLocalizer<CategoryResource> categoryLocalizer)
        {
            _category = category;
            _localizer = localizer;
            _categoryLocalizer = categoryLocalizer;

        }
        
        
        /// <summary>
        /// show category    
        /// </summary>
        /// <returns>index category</returns>
        public async Task<IActionResult> Index()
        {
            var listCateogry = await _category.GetCategoryListAsync();
            return View(listCateogry);

        }
        
        /// <summary>
        /// get create category    
        /// </summary>
        /// <returns>view create category </returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        /// <summary>
        /// post create category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>view create category</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewsModels category)
        {
            if (ModelState.IsValid)
            {
                
                var addCategory = await _category.AddCategoryAsync(category);
                if (addCategory)
                {
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_AddSuccessfuly").ToString();
                    return RedirectToAction("Index");
                }
                TempData["Failure"] = _localizer.GetLocalizedString("err_EditFailure").ToString();
                return View(category);
                
            }
            return View(category);
        }
        
        /// <summary>
        /// get edit category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view edit category</returns>
        [HttpGet]
        public async  Task<IActionResult> Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var getCategory = await _category.GetIdCategoryAsync(id.Value);
           
            return View(getCategory);
        }
        
        /// <summary>
        /// post edit category
        /// </summary>
        /// <param name="editCategory"></param>
        /// <returns>return index category</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewsModels editCategory)
        {
           
            if (ModelState.IsValid){
                
                    
                var category = await _category.EditCategoryAsync(editCategory);
                if (category)
                {
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_EditSuccessfuly").ToString();
                    return RedirectToAction("Index");
                
                }
                TempData["Failure"] = _localizer.GetLocalizedString("err_EditFailure").ToString();
                return View(editCategory);
            }
            return View(editCategory);
        }
        
        /// <summary>
        ///  Delete category 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>index category</returns>
        [Authorize(Roles= "Admin") ]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var rmCategory=await _category.Delete(id.Value);
            if (rmCategory)
            {
                TempData["Successfuly"] = _localizer.GetLocalizedString("msg_DeleteSuccessfuly").ToString();
                return RedirectToAction("Index");
                
            }
            TempData["Failure"] = _localizer.GetLocalizedString("err_DeleteFailure").ToString();
            return RedirectToAction("Index");
            
        }
    }
}