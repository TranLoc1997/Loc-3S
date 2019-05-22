using Microsoft.AspNetCore.Mvc;

namespace TaskUser.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Lổi 401
        /// </summary>
        /// <returns>View 401</returns>
        [Route("Error/401")]
        public IActionResult Error401()
        {
            return View();
        }
        
        [Route("Error/404")]
        public IActionResult Error404()
        {
            return View();
        }

    }
}