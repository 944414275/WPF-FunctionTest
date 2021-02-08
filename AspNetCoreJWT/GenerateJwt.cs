﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreJWT
{
    /// <summary>
    /// 创建Jwt
    /// </summary>
    public class GenerateJwt
    {
        private readonly JwtConfig _jwtConfig;
        public GenerateJwt(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }
        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="sub">subject</param>
        /// <param name="customClaims">携带的用户信息</param>
        /// <returns></returns>
        public JwtTokenResult GenerateEncodedTokenAsync(string sub, LoginUserModel customClaims)
        {
            //创建用户身份标识，可按需要添加更多信息
            var claims = new List<Claim>
            {
                new Claim("userid", customClaims.userid),
                new Claim("username", customClaims.username),
                new Claim("realname",customClaims.realname),
                new Claim("roles", string.Join(";",customClaims.roles)),
                new Claim("permissions", string.Join(";",customClaims.permissions)),
                new Claim("normalPermissions", string.Join(";",customClaims.normalPermissions)),
                new Claim(JwtRegisteredClaimNames.Sub, sub),
            };
            //创建令牌
            var jwt = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                notBefore: _jwtConfig.NotBefore,
                expires: _jwtConfig.Expiration,
                signingCredentials: _jwtConfig.SigningCredentials);
            string access_token = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            return new JwtTokenResult()
            {
                access_token = access_token,
                expires_in = _jwtConfig.Expired * 60,
                token_type = JwtBearerDefaults.AuthenticationScheme,
                user = customClaims
            };
        }
    }
}
