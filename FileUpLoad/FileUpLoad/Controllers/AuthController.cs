using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FileUpLoad.Filter;
using FileUpLoad.Model;
using FileUpLoad.Utility;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static FileUpLoad.Utility.Enum;

namespace FileUpLoad.Controllers
{
    /// <summary>
    /// 模拟登录授权、Token验证、Token状态验证
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestAuthController : ControllerBase
    {
        //获取JwtSettings对象信息
        private JwtSettings _jwtSettings;
        private readonly ITokenHelper _tokenHelper = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_jwtSettingsAccesser"></param>
        /// <param name="tokenHelper"></param>
        public TestAuthController(IOptions<JwtSettings> _jwtSettingsAccesser
            , ITokenHelper tokenHelper)
        {
            _jwtSettings = _jwtSettingsAccesser.Value;
            _tokenHelper = tokenHelper;
        }

        /// <summary>
        /// 登录授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<LoginRedisResult> TestLogin()
        {
            var result = new ResultModel<LoginRedisResult>();
            //测试自己创建的对象
            var user = new AccountModel
            {
                Phone = "13512345678",
                Password = "e10adc3949ba59abbe56e057f20f883e"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var authTime = DateTime.Now;//授权时间
            var expiresAt = authTime.AddDays(30);//过期时间
            var tokenDescripor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(JwtClaimTypes.Audience,_jwtSettings.Audience),
                    new Claim(JwtClaimTypes.Issuer,_jwtSettings.Issuer),
                    new Claim(JwtClaimTypes.Name, user.Phone),
                    new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                    new Claim(JwtClaimTypes.PhoneNumber, user.Phone)
                }),
                Expires = expiresAt,
                //对称秘钥SymmetricSecurityKey
                //签名证书(秘钥，加密算法)SecurityAlgorithms
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescripor);
            var tokenString = tokenHandler.WriteToken(token);
            var resultMod = new LoginRedisResult
            {
                AccessToken = tokenString,
                TokenType = "Bearer",
                LoginMod = new UserInfoModel
                {
                    Id = user.Id.ToString(),
                    Name = user.Phone,
                    Phone = user.Phone,
                    AutTime = DateTimeExtend.ConvertToTimeStamp(authTime),
                    ExpiresAt = DateTimeExtend.ConvertToTimeStamp(expiresAt)
                }
            };
            return result.Success("登录成功", resultMod);
        }

        /// <summary>
        ///  获取登录用户信息
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(TokenFilter))]
        [HttpPost]
        public ResultModel<string> TestGetUserInfo()
        {
            var result = new ResultModel<string>();
            var userId = HttpContext.User.FindFirst("id").Value;
            return result.Success("OK", userId);
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="tokenStr">token</param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<bool> TestValiToken(string tokenStr)
        {
            var result = new ResultModel<bool>() { Msg = "Token验证失败" };
            var secretKey = ConfigurationManager.GetJwtSettings("SecretKey");
            bool isvilidate = _tokenHelper.ValiToken(tokenStr, secretKey);
            if (isvilidate)
                return result.Success("Token验证成功", true);
            return result;
        }

        /// <summary>
        /// 验证Token 带返回状态
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<bool> TestValiTokenState(string tokenStr)
        {
            var result = new ResultModel<bool>();
            string loginID = "";
            var secretKey = ConfigurationManager.GetJwtSettings("SecretKey");
            TokenType tokenType = _tokenHelper.ValiTokenState(tokenStr, secretKey, a => a["iss"] == "webapi.cn" && a["aud"] == "webapi", action => { loginID = action["id"]; });
            if (tokenType == TokenType.Fail)
                return result.Error("token验证失败", false);
            if (tokenType == TokenType.Expired)
                return result.Error("token已经过期", false);
            return result.Success("访问成功", true); ;
        } 
    }
}