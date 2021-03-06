﻿using AspNetCoreJWTTest2.Interface;
using AspNetCoreJWTTest2.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;
using System.Text; 

namespace AspNetCoreJWTTest2
{
    public class TokenHelper : ITokenHelper
    {
        private IOptions<JWTConfig> _options;
        public TokenHelper(IOptions<JWTConfig> options)
        {
            _options = options;
        }

        public Token CreateToken(User user)
        {
            Claim[] claims = { new Claim(ClaimTypes.NameIdentifier, user.Code), new Claim(ClaimTypes.Name, user.Name) };

            return CreateToken(claims);
        }
        private Token CreateToken(Claim[] claims)
        {
            var now = DateTime.Now; 
            var expires = now.Add(TimeSpan.FromMinutes(_options.Value.AccessTokenExpiresMinutes));
            
            var token = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.IssuerSigningKey)), SecurityAlgorithms.HmacSha256));
            return new Token { TokenContent = new JwtSecurityTokenHandler().WriteToken(token), Expires = expires };
        }
    }
}
