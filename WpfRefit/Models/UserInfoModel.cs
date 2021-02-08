using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRefit.Models
{
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
