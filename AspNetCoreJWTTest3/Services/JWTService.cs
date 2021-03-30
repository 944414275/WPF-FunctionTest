using AspNetCoreJWTTest3.Interface;
using AspNetCoreJWTTest3.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreJWTTest3.Services
{
    public class JWTService : IJWTService
    {
        private readonly TokenParameter _tokenParameter;
        public JWTService(IConfiguration configuration)
        {
            _tokenParameter = configuration.GetSection("TokenParameter").Get<TokenParameter>();
        }

        /// <summary>
        /// JWT由三部分组成（Header、Payload、Signature）
        /// {Header}.{Payload}.{Signature}
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Token GetToken(User user)
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                //new Claim("NickName","Richard"),
                new Claim("Role",user.rool)//传递其他信息
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.SecurityKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            /**
             *  Claims (Payload)
                Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:
                JWT会被加密，但是这部分内容任何人都可以读取，所以不要存放机密信息

                iss: The issuer of the token，token 是给谁的
                sub: The subject of the token，token 主题
                exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                iat: Issued At。 token 创建时间， Unix 时间戳格式
                jti: JWT ID。针对当前 token 的唯一标识
                除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
             * */

            DateTime dtNow = DateTime.Now;
            var token = new JwtSecurityToken(
                issuer: _tokenParameter.Issuer,
                audience: user.Name,
                claims: claims,
                //notBefore: dtNow,
                expires: dtNow.AddMinutes(1),//1分钟有效期
                signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token { TokenContent=returnToken, Expires = dtNow.AddMinutes(1) }; 
            //return returnToken;
        }
         
        //string IJWTService.GetToken(User user)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
