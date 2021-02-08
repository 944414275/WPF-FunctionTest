using AspNetCoreJWTTest3.Interface;
using AspNetCoreJWTTest3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 

namespace AspNetCoreJWTTest3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region 构造函数
        private IJWTService _iJWTService;
        private ILogger<AuthenticationController> _logger; 
        private readonly IConfiguration _iConfiguration;
        public AuthenticationController(ILogger<AuthenticationController> logger, IConfiguration configuration ,IJWTService service)
        {
            _logger = logger;
            _iConfiguration = configuration;
            _iJWTService = service;
        }
        #endregion

        /// <summary>
        /// 实际场景使用Post方法
        /// http://localhost:5000/api/Authentication/Login?name=william&password=123123
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //[Route("Login")]
        [HttpGet]
        public IActionResult Login(string code, string pwd)
        {
            #region https://www.cnblogs.com/wyt007/p/11459547.html
            //User loginUser = TemporaryData.GetUser(code);
            //if (null != loginUser && loginUser.Password.Equals(pwd))
            //{
            //    return Ok(tokenHelper.CreateToken(user));
            //}
            //return BadRequest();
            #endregion

            #region MyRegion
            User loginUser = TemporaryData.GetUser(code);

            //这里应该是需要去连接数据库做数据校验，为了方便所有用户名和密码写死了
            if (null != loginUser && loginUser.Password.Equals(pwd))//应该数据库
            {
                //var _role = loginUser.rool;//可以从数据库获取角色
                string token = this._iJWTService.GetToken(loginUser);
                return new JsonResult(new
                {
                    result = true,
                    token
                });
            }

            return Unauthorized("Not Register!!!");
            #endregion 
        }
    }
}
