using System.Windows;

namespace WpfMinioTest1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static NLog.Logger NewNLog;
        public static long MinimumPartSize = 5 * 1024L * 1024L;//单次上传文件请求最大5MB
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            NewNLog = NLog.LogManager.GetLogger("MinIOLoger");
        }
    }
}
