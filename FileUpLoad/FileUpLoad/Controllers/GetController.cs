using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FileUpLoad.Controllers
{
    /// <summary>
    /// [Authorize]
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class GetController : ControllerBase
    {
        /// <summary>
        /// 没问题
        /// </summary> 
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string Get(string name)
        {
            return "fengcaihong" + name;
        }

        /// <summary>
        /// 没问题
        /// </summary> 
        /// <param ></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string GetTwo()
        {
            return "fengcaihong2";
        }
    }
}
