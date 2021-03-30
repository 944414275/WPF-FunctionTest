using Microsoft.Win32;
using Minio;
using Minio.Exceptions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using WpfMinio.Helper;

namespace WpfMinio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _endpoint = "192.168.127.131:9000";//minio服务器地址
        private static string _accessKey = "minioadmin";//授权登录账号
        private static string _secretKey = "minioadmin";//授权登录密码
        private static MinioClient _minioClient;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _minioClient = new MinioClient(_endpoint, _accessKey, _secretKey);
            Messenger.Default.Register<int>(this, "process", obj =>
            {
                try
                {
                    Debug.WriteLine($"当前块编号：{obj}");
                    if (obj == 0)
                    {
                        ViewModelLocator.Instance.FileUploadViewModel.UploadProcess = "0.00%";
                        return;
                    }
                    ViewModelLocator.Instance.FileUploadViewModel.PartNumber = obj;
                    ViewModelLocator.Instance.FileUploadViewModel.UploadProcess =
                        $"{(float)ViewModelLocator.Instance.FileUploadViewModel.PartNumber / ViewModelLocator.Instance.FileUploadViewModel.TotalParts:P2}";//计算文件上传进度
                }
                catch (Exception exception)
                {
                    App.NewNLog.Error($"计算上传进度时出错：{exception}");
                }
            });
        }
        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            var open = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
            };
            if (open.ShowDialog(this) == false)
            {
                return;
            }

            ViewModelLocator.FileUploadViewModel.FileName = open.SafeFileName;
            try
            {
                Dispatcher?.InvokeAsync(async () =>
                {
                    await Run(_minioClient, "test", open.FileName, ViewModelLocator.Instance.FileUploadViewModel.FileName);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 20210310 保存
        /// </summary>
        /// <param name="minio"></param>
        /// <param name="userBucketName"></param>
        /// <param name="uploadFilePath"></param>
        /// <param name="saveFileName"></param>
        /// <returns></returns>
        private static async Task Run(MinioClient minio, string userBucketName, string uploadFilePath, string saveFileName)
        {
            var bucketName = userBucketName;
            var location = "us-east-1";
            var objectName = saveFileName;
            var filePath = uploadFilePath;
            var contentType = ContentTypeHelper.GetContentType(saveFileName.Substring(saveFileName.LastIndexOf('.') + 1));
            var file = new FileInfo(uploadFilePath);

            try
            {
                var found = await minio.BucketExistsAsync(bucketName);
                if (!found)
                {
                    await minio.MakeBucketAsync(bucketName, location);
                }

                _minioClient.SetTraceOn(new LogHelper());//我们在上传开始的时候，打开日志，通过日志抛出的块编号来计算出当前进度

                ViewModelLocator.Instance.FileUploadViewModel.FileSize = file.Length;
                ViewModelLocator.Instance.FileUploadViewModel.TotalParts = file.Length / App.MinimumPartSize + 1;//计算出文件总块数

                //上传文件
                await minio.PutObjectAsync(bucketName, objectName, filePath, contentType);
                Debug.WriteLine("Successfully uploaded " + objectName);

            }
            catch (MinioException e)
            {
                App.NewNLog.Error($"File Upload Error: {e}");
                Debug.WriteLine($"File Upload Error: {e.Message}");
            }
            finally
            {
                _minioClient.SetTraceOff();
            }
        }
    }
}
