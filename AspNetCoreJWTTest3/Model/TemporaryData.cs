using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreJWTTest3.Model
{
    /// <summary>
    /// 虚拟数据，模拟从数据库或缓存中读取用户
    /// </summary>
    public class TemporaryData
    {
        private static List<User> Users = new List<User>() { new User { Code = "001", Name = "hong", Password = "111111" ,rool="Admin"}, new User { Code = "002", Name = "yangya", Password = "222222", rool = "guster" } };

        public static User GetUser(string code)
        {
            return Users.FirstOrDefault(m => m.Code.Equals(code));
        }
    }
}
