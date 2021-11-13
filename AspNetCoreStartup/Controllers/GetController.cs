using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreStartup.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("LKApi/[controller]/[action]")]
    public class GetController : ControllerBase
    {
        /// <summary>
        /// 没问题
        /// </summary> 
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public string Get(string name)
        {
            return "fengbi"+name;
        }

        /// <summary>
        /// 没问题
        /// </summary> 
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetTwo()
        {
            return "fengbi";
        }
    }
}
