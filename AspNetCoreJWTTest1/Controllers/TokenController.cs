﻿using AspNetCoreJWTTest1.Model;
using Microsoft.AspNetCore.Mvc; 

namespace AspNetCoreJWTTest1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        //private ITokenHelper tokenHelper = null;
        //public TokenController(ITokenHelper _tokenHelper)
        //{
        //    tokenHelper = _tokenHelper;
        //}

        //[HttpGet]
        //public IActionResult Get(string code, string pwd)
        //{
        //    User user = TemporaryData.GetUser(code);
        //    if (null != user && user.Password.Equals(pwd))
        //    {
        //        return Ok(tokenHelper.CreateToken(user));
        //    }
        //    return BadRequest();
        //}
    }
}
