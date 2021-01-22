using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfMinioTest2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _endpoint = "172.16.18.73:9000";//minio服务器地址
        private static string _accessKey = "minioadmin";//授权登录账号
        private static string _secretKey = "minioadmin";//授权登录密码
        private static MinioClient _minioClient;

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
