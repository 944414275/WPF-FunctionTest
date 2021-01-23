using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreStartup.Utility
{
    public class TokenParameter
    {
        public const string Issuer = "深度码农";//颁发者        
        public const string Audience = "深度码农";//接收者        
        public const string Secret = "1234567812345678";//签名秘钥        
        public const int AccessExpiration = 30;//AccessToken过期时间（分钟）
    }
}
