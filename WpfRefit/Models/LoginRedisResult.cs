using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRefit.Models
{
    public class LoginRedisResult
    {
        /// <summary>
        /// 用户登录token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// tokenType:Bearer
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// 用户登陆成功后存储的信息
        /// </summary>
        public UserInfoModel LoginMod { get; set; }
    }
}
