using AspNetCoreJWTTest2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreJWTTest2.Interface
{
    public interface ITokenHelper
    {
        public Token CreateToken(User user);
    }
}
