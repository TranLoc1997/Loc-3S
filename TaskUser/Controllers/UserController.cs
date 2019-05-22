using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.User;

namespace TaskUser.Controllers
{
//    [ServiceFilter(typeof(ActionFilter))]
    [Authorize(Roles= "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStoreService _storeService;
        private readonly SharedViewLocalizer<CommonResource> _localizer;
        private readonly SharedViewLocalizer<PasswordResource> _passwordLocalizer;
        private readonly SharedViewLocalizer<UserResource> _userLocalizer;
        public UserController(
          
            IUserService userService,
           
            IStoreService storeService,
            SharedViewLocalizer<CommonResource> localizer,
            SharedViewLocalizer<PasswordResource> passwordLocalizer,
            SharedViewLocalizer<UserResource> userLocalizer
            )
        {
          
            _userService = userService;
            _storeService = storeService;
            _localizer = localizer;
            _passwordLocalizer = passwordLocalizer;
            _userLocalizer = userLocalizer;

        }
     
        /// <summary>
        /// show index  user
        /// </summary>
        /// <returns>index  user</returns>
        public async Task<IActionResult> Index()
        {
            var listUser = await _userService.GetUserListAsync();
            if (listUser==null)
            {
                return NotFound();

            }
            return View(listUser);

        }
        
        /// <summary>
        /// get create  user
        /// </summary>
        /// <returns>view create  user</returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.StoreId = new SelectList(_storeService.GetStore(), "Id", "StoreName");
            return View();
        }
        
        /// <summary>
        /// post create  user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>index  User  else view</returns>
        [HttpPost]
        public async Task<IActionResult> Create(UserViewsModels user)
        {
            if (ModelState.IsValid)
            {
                var addUser = await _userService.AddUserAsync(user);
                if (addUser)
                {
                    
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_AddSuccessfuly").ToString();
                    return RedirectToAction("Index");
                }
                TempData["Failure"] = _localizer.GetLocalizedString("err_AddFailure").ToString();
                ViewBag.StoreId = new SelectList(_storeService.GetStore(), 
                    "Id", "StoreName",user.StoreId);
                return View(user);
                
            }
            ViewBag.StoreId = new SelectList(_storeService.GetStore(), 
                "Id", "StoreName",user.StoreId);
            return View(user);
        }
        
        /// <summary>
        /// get edit of user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>create of user</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                
                return BadRequest();
            }
            var findUser = await _userService.GetIdAsync(id.Value);
            if (findUser ==null)
            {
                
                return BadRequest();
            }
            
            ViewBag.StoreId = new SelectList(_storeService.GetStore(), "Id", "StoreName");
            return View(findUser);
        }

        
        /// <summary>
        /// post edit of user
        /// </summary>
        /// <param name="userParam"></param>
        /// <returns>index of User else view</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewsModels userParam)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.EditUserAsync(userParam);
                if (user)
                {
                    TempData["Successfuly"] = _localizer.GetLocalizedString("msg_EditSuccessfuly").ToString();
                    return RedirectToAction("Index");
                }
                TempData["Failure"] = _localizer.GetLocalizedString("err_EditFailure").ToString();
                return View(userParam);

            }
            ViewBag.StoreId = new SelectList(_storeService.GetStore(), "Id", "StoreName",userParam.StoreId);
            return View(userParam);
        }

        /// <summary>
        /// get edit password 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view _change Password</returns>
        [HttpGet]
        public async Task<IActionResult> EditPassword(int? id)
        {
            if (id == null) return BadRequest();
            var findPassword = await _userService.GetPasswordAsync(id.Value);


            return PartialView("_ChangePassword", findPassword);

        }

        /// <summary>
        /// post edit password
        /// </summary>
        /// <param name="passwordUser"></param>
        /// <returns>index of User else view</returns>
        [HttpPost]
        public async Task<IActionResult> EditPassword(EditViewPassword passwordUser)
        { 
            if (ModelState.IsValid)
            {
                var users =  await _userService.EditPasswordAsync(passwordUser);
                if (users)
                {
                    TempData["EditPasswordSuccessfuly"] = _passwordLocalizer.GetLocalizedString("msg_EditPasswordSuccessfuly").ToString();
                    return PartialView("_ChangePassword",passwordUser);    
                }        
                ViewData["EditPasswordFailure"] = _passwordLocalizer.GetLocalizedString("err_PasswordFailure");
                return PartialView("_ChangePassword",passwordUser);       
            }
            ViewData["EditPasswordFailure"] = _passwordLocalizer.GetLocalizedString("err_PasswordFailure");
            return PartialView("_ChangePassword",passwordUser);
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view index of user</returns>
        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var rmUser=await _userService.Delete(id.Value);
            if (rmUser)
            {
                TempData["Successfuly"] = _localizer.GetLocalizedString("msg_DeleteSuccessfuly").ToString();
                return RedirectToAction("Index");
            }
            TempData["Failure"] = _localizer.GetLocalizedString("err_DeleteFailure").ToString();
            return RedirectToAction("Index");
            
        }
        
    }
    
}