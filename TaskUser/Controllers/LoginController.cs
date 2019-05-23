using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using TaskUser.Models;
using TaskUser.Service;
using TaskUser.ViewsModels.User;

namespace TaskUser.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController
        (
            DataContext context,
            IUserService userService
        )
        
        {
            _userService = userService;
        }
        /// <summary>
        /// get page index login
        /// </summary>
        /// <returns>view login</returns>
        [HttpGet]
        public IActionResult IndexLogin()
        {
            return View();
            
        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="model">LoginViewModel</param>
        /// <returns>view index controller user</returns>
        [HttpPost]
        public async Task<IActionResult> IndexLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            
            {    
                var user = _userService.Login(model.Email, model.PassWord);
                if (user)
                {
                    var name = _userService.GetName(model.Email);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Email),
                        new Claim("FullName", model.Email),
                        new Claim(ClaimTypes.Role, name.Role)
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(claimsIdentity), 
                        authProperties);
                    HttpContext.Session.SetString("name",name.Name);
                    return RedirectToAction("Index", "Store");
                    
                }
            }
            return View(model);

        }
        
        /// <summary>
        /// Logout 
        /// </summary>
        /// <returns>Login xóa session name</returns>
        
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IndexLogin");

        }
        
        /// <summary>
        /// Language
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="returnUrl"></param>
        /// <returns>localredirect</returns>
        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}