using AspNetCoreJWTTest3.Model;

namespace AspNetCoreJWTTest3.Interface
{
    public interface IJWTService
    {
        /// <summary>
        /// 根据验证通过后的用户以及角色生成Token,以达到角色控制的作用
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        //string GetToken(string userName, string role);
        string GetToken(User user);
    }
}
