using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpLoad.Model
{


    /// <summary>
    /// 
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// 
        /// </summary>
        public AccountModel()
        {
            Id = new Random().Next(1, 100000);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 登录缓存对象
    /// </summary>
    public class UserInfoModel
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 登录名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 授权时间
        /// </summary>
        public long AutTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public long ExpiresAt { get; set; }
    }
}
