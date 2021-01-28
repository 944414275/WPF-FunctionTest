using Microsoft.Extensions.Configuration;

namespace FileUpLoad.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConfigurationManager
    {
        private static IConfiguration _configuration;

        /// <summary>
        ///  
        /// </summary>
        /// <param name="configuration"></param>
        public static void Init(this IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 获取配置文件ConnectionStrings 属性值
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static string ConnectionStrings(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName);
        }

        /// <summary>
        /// 获取配置文件AppSetting参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AppSettings(string key)
        {
            return _configuration.GetSection("AppSettings")[key];
        }

        /// <summary>
        ///  获取配置文件Jwt参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetJwtSettings(string key)
        {
            return _configuration.GetSection("JwtSettings")[key];
        }

        /// <summary>
        /// 获取配置文件 文件上传 参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetUpFileInfo(string key)
        {
            return _configuration.GetSection("UpFileOptions")[key];
        }

    }
}
