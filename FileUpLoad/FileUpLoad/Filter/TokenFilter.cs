using FileUpLoad.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using static FileUpLoad.Utility.Enum;

namespace FileUpLoad.Filter
{
    /// <summary>
    /// Token验证过滤器
    /// </summary>
    public class TokenFilter : Attribute, IActionFilter
    {
        private readonly ITokenHelper _tokenHelper;

        /// <summary>
        /// 通过依赖注入得到数据访问层实例
        /// </summary>
        /// <param name="tokenHelper"></param>
        public TokenFilter(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        /// <summary>
        /// 请求接口时进行拦截处理
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var ret = new ResultModel();
            try
            {
                //获取请求头中的Token
                var tokenobj = context.HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(tokenobj))
                {
                    ret.Code = ResultCodeEnum.ApiUnauthorized;
                    ret.Msg = "接口未授权";
                    context.Result = new JsonResult(ret);
                    return;
                }

                //读取配置文件中的秘钥
                var secretKey = ConfigurationManager.GetJwtSettings("SecretKey");
                string token = tokenobj.Split(" ")[1].ToString();//剔除Bearer 
                string userId = "";
                string mobile = "";//用户手机号
                //验证jwt,同时取出来jwt里边的用户ID
                TokenType tokenType = _tokenHelper.ValiTokenState(token, secretKey
                    , a => a["iss"] == "webapi.cn" && a["aud"] == "webapi"
                    , action =>
                    {
                        userId = action["id"];
                        mobile = action["phone_number"];
                    });
                if (tokenType == TokenType.FormError)
                {
                    ret.Code = ResultCodeEnum.ApiUnauthorized;
                    ret.Msg = "登录失效,请重新登录！";//token非法
                    context.Result = new JsonResult(ret);
                    return;
                }
                if (tokenType == TokenType.Fail)
                {
                    ret.Code = ResultCodeEnum.ApiUnauthorized;
                    ret.Msg = "用户信息验证失败！";//token验证失败
                    context.Result = new JsonResult(ret);
                    return;
                }
                if (tokenType == TokenType.Expired)
                {
                    ret.Code = ResultCodeEnum.ApiUnauthorized;
                    ret.Msg = "登录失效,请重新登录！";
                    context.Result = new JsonResult(ret);
                    return;
                }
                if (string.IsNullOrEmpty(userId))
                {
                    //获取用户编号失败时，阻止用户继续访问接口
                    ret.Code = ResultCodeEnum.Error;
                    ret.Msg = "用户信息丢失";
                    context.Result = new JsonResult(ret);
                    return;
                }

                //自定义代码逻辑，  取出token中的 用户编号 进行 用户合法性验证即可
                //。。。。。。。
            }
            catch (Exception ex)
            {
                ret.Code = ResultCodeEnum.Error;
                ret.Msg = "请求来源非法" + ex.Message.ToString();
                context.Result = new JsonResult(ret);
                return;
            }
        }
    }
}
