using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace AspNetCoreJWTTest3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] 
    [Authorize]
    public class GetController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "fengcaihong";
        }
    }
}
