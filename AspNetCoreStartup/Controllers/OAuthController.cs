using AspNetCoreStartup.Utility; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens; 
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text; 

namespace AspNetCoreStartup.Controllers
{
    [ApiController]
    [Route("LKApi/[controller]/[action]")] 
    public class OAuthController : ControllerBase
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public ActionResult GetAccessToken(string username, string password)
        {
            //这儿在做用户的帐号密码校验。我这儿略过了。
            if (username != "admin" || password != "admin")
                return BadRequest("Invalid Request");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, ""),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenParameter.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(TokenParameter.Issuer, TokenParameter.Audience, claims, expires: DateTime.UtcNow.AddMinutes(TokenParameter.AccessExpiration), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return Ok(token);
        }

        //[HttpGet("{id}")]
        //[Authorize]
        //public async Task<ActionResult<Todo>> GetTodo(Guid id)
        //{
        //    var todo = await context.Todo.FindAsync(id);

        //    if (todo == null)
        //    {
        //        return NotFound();
        //    }

        //    return todo;
        //}
    }
}
