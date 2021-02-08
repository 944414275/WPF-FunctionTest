using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreJWTTest3.Model
{
    public class Token
    {
        /// <summary>
        /// token内容
        /// </summary>
        public string TokenContent { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
