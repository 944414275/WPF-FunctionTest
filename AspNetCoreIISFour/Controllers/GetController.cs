using Microsoft.AspNetCore.Mvc; 

namespace AspNetCoreIISFour.Controllers
{
    /// <summary>
    ///  
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GetController : ControllerBase
    {
        [HttpGet] 
        public string Get()
        {
            return "fengcaihong";
        }
    }
}
