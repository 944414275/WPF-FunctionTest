using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreJWTTest3.Model
{
    public class User
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// 账号
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string rool { get; set; }
    }
}
