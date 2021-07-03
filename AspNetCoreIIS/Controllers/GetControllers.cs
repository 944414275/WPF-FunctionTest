using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIIS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GetController : ControllerBase
    {
        /// <summary>
        /// GetOne
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public string Get()
        {
            return "fengcaihong";
        }
    }
}
