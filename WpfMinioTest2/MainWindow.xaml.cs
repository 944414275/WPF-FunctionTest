using Microsoft.Win32;
using Minio;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _endpoint = "172.16.18.73:9000";//minio服务器地址
        private static string _accessKey = "minioadmin";//授权登录账号
        private static string _secretKey = "minioadmin";//授权登录密码
        private static MinioClient _minioClient;
        private string testStr = "qwertqewrwrwtrreywtrywsdghf";

        public MainWindow()
        {
            InitializeComponent();
            _minioClient = new MinioClient(_endpoint, _accessKey, _secretKey); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region 20210326 komla
            var open = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
            };
            if (open.ShowDialog(this) == false)
            {
                return;
            }
            string filePath = open.FileName;


            #endregion

            #region 20210326 流操作
            //byte[] array = Encoding.ASCII.GetBytes(testStr);
            //MemoryStream stream = new MemoryStream(array);
            #endregion

            #region 20210327 
            //byte[] bs = File.ReadAllBytes("G:\\手机文件\\半身照.jpg");
            //MemoryStream filestream = new MemoryStream(bs);
            #endregion


            try
            {
                _minioClient.PutObjectAsync("company1", "device2", filePath);//20210327 这一步是不是封装了创建桶操作 
                //Dispatcher?.InvokeAsync(async () =>
                //{
                //    await _minioClient.PutObjectAsync("company1", "device2", filestream, filestream.Length, "application/octet-stream");
                //});
            }
            catch (MinioException ex)
            { 
                throw;
            } 
        }

    }
}
