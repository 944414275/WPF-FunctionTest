using System.Diagnostics; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 

namespace FileUpLoad.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return Redirect($"https://{HttpContext.Request.Host}/swagger");
        }

        public IActionResult Error()
        {
            return Content(Activity.Current?.Id??HttpContext.TraceIdentifier);
        }
    }
}
