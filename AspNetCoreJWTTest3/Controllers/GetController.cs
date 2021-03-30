using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace AspNetCoreJWTTest3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    
    public class GetController : ControllerBase
    {
        [HttpGet] 
        public string Get()
        {
            return "111111111";
        }

        [HttpGet]
        [Authorize]
        public string GetTwo()
        {
            return "222222222";
        }

    }
}
