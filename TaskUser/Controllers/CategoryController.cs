using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public CategoryController(ICategoryService category ,SharedViewLocalizer<CommonResource> localizer,SharedViewLocalizer<CategoryResource> categoryLocalizer)
        {
            _category = category;
            _localizer = localizer;

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
        /// <returns>view create category</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        /// <summary>
        /// post create category
        /// </summary>
        /// <param name="category">CategoryViewsModels</param>
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
                ViewData["EditFailure"] = _localizer.GetLocalizedString("err_AddFailure");
                return View(category);
            }
            return View(category);
        }
        
        /// <summary>
        /// get edit category
        /// </summary>
        /// <param name="id">CategoryViewsModels</param>
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
        /// <param name="editCategory">CategoryViewsModels</param>
        /// <returns>return index category</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewsModels editCategory)
        {
           
            if (ModelState.IsValid)
            {
                var category = await _category.EditCategoryAsync(editCategory);
                if (category)
                {
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_EditSuccessfuly").ToString();
                    return RedirectToAction("Index");
                }
                ViewData["EditFailure"] = _localizer.GetLocalizedString("err_EditFailure");
                return View(editCategory);
            }
            return View(editCategory);
        }
        
        /// <summary>
        ///  Delete category 
        /// </summary>
        /// <param name="id">CategoryViewsModels</param>
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