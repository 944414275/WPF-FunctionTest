﻿using Microsoft.AspNetCore.Mvc;
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
            return "fengcaihong"+name;
        }

        /// <summary>
        /// 没问题
        /// </summary> 
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetTwo()
        {
            return "fengcaihong2";
        }
    }
}
