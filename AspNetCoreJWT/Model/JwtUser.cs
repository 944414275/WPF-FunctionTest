using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreJWT
{
    public static class JwtUser
    {
        /// <summary>
        /// 解析jwt 获取当前用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns>用户信息</returns>
        public static LoginUserModel GetRequestUser(this HttpRequest request)
        {
            var authorization = request.Headers["Authorization"].ToString();
            var auth = authorization.Split(" ")[1];
            var jwtArr = auth.Split('.');
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[1]));
            //解析Claims
            var reqUser = new LoginUserModel
            {
                userid = dic["userid"],
                username = dic["username"],
                realname = dic["realname"],
                roles = dic["roles"],
                permissions = dic["permissions"],
                normalPermissions = dic["normalPermissions"],
            };
            return reqUser;
        }
    }
}
