using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreJWTTest3.Model
{
    public class TokenParameter
    {
        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int Expired { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecurityKey { get; set; }
    }
}
