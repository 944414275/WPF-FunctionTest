using System.Diagnostics; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 

namespace FileUpLoad.Controllers
{
    /// <summary>
    /// Home
    /// </summary>
    public class HomeController:Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Redirect($"https://{HttpContext.Request.Host}/swagger");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return Content(Activity.Current?.Id??HttpContext.TraceIdentifier);
        }
    }
}
