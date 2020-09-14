using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace ConWebAPITest.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        /// <summary>
        /// Print the greetings
        /// </summary>
        /// <param name="name">visitor</param>
        /// <returns>greetings</returns>
        [Route("echo")]
        [HttpGet]
        public IHttpActionResult Echo(string name)
        {
            return Json(new { Name = name, Message = $"Hello, {name}" });
        }
    }
}
