using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace FileUpLoad.AutofacUtility
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomAutofacModule : Autofac.Module
    {
        /// <summary>
        /// 重写Load方法
        /// </summary>
        /// <param name="containerBuilder"></param>
        protected override void Load(ContainerBuilder containerBuilder)
        {
            #region Autofac 基于配置文件的服务注册
            //JSON格式配置文件
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder(); // 配置文件的读取器 
            configurationBuilder.AddJsonFile("autofac.json");
            IConfigurationRoot root = configurationBuilder.Build();
            // 开始读取配置文件里的内容信息
            ConfigurationModule module = new ConfigurationModule(root);
            // 根据配置文件的内容注册服务
            containerBuilder.RegisterModule(module);

            #endregion
        }
    }
}
