using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using static FileUpLoad.Utility.Enum;

namespace FileUpLoad.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class TokenHelper : ITokenHelper
    {
        /// <summary>
        /// 验证身份 验证签名的有效性
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// <param name="secretKey">配置文件中取出来的签名秘钥</param>
        /// <param name="validatePayLoad">自定义各类验证； 是否包含那种申明，或者申明的值， </param>
        public bool ValiToken(string encodeJwt, string secretKey, Func<Dictionary<string, string>, bool> validatePayLoad = null)
        {
            var success = true;
            var jwtArr = encodeJwt.Split('.');
            if (jwtArr.Length < 3)//数据格式都不对直接pass
                return false;
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[1]));
            //配置文件中取出来的签名秘钥
            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey));
            //验证签名是否正确（把用户传递的签名部分取出来和服务器生成的签名匹配即可）
            success = success && string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1])))));
            if (!success)
                return success;//签名不正确直接返回
            //其次验证是否在有效期内（也应该必须）
            var now = ToUnixEpochDate(DateTime.UtcNow);
            success = success && (now >= long.Parse(payLoad["nbf"].ToString()) && now < long.Parse(payLoad["exp"].ToString()));

            //不需要自定义验证不传或者传递null即可
            if (validatePayLoad == null)
                return true;
            //再其次 进行自定义的验证
            success = success && validatePayLoad(payLoad);
            return success;
        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// <param name="secretKey"></param>
        /// <param name="validatePayLoad"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public TokenType ValiTokenState(string encodeJwt, string secretKey, Func<Dictionary<string, string>, bool> validatePayLoad, Action<Dictionary<string, string>> action)
        {
            //iss: jwt签发者
            //sub: jwt所面向的用户
            //aud: 接收jwt的一方
            //exp: jwt的过期时间，这个过期时间必须要大于签发时间
            //nbf: 定义在什么时间之前，该jwt都是不可用的
            //iat: jwt的签发时间
            //jti: jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击

            var jwtArr = encodeJwt.Split('.');
            if (jwtArr.Length < 3)//数据格式都不对直接pass
                return TokenType.FormError;
            //var header = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[0]));
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[1]));
            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(secretKey));
            //验证签名是否正确（把用户传递的签名部分取出来和服务器生成的签名匹配即可）
            if (!string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1]))))))
                return TokenType.FormError;
            var now = ToUnixEpochDate(DateTime.UtcNow);
            var nbf = long.Parse(payLoad["nbf"].ToString());
            var exp = long.Parse(payLoad["exp"].ToString());
            if (!(now >= nbf && now < exp))
            {
                action(payLoad);
                return TokenType.Expired;
            }
            //不需要自定义验证不传或者传递null即可
            if (validatePayLoad == null)
            {
                action(payLoad);
                return TokenType.Ok;
            }
            //再其次 进行自定义的验证
            if (!validatePayLoad(payLoad))
                return TokenType.Fail;
            //可能需要获取jwt摘要里边的数据，封装一下方便使用
            action(payLoad);
            return TokenType.Ok;
        }
    }
}
