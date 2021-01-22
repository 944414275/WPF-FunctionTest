using GalaSoft.MvvmLight.Messaging;
using Minio;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using WpfMinioTest1.Helper;
using WpfMinioTest1.ViewModel;
using Minio.Exceptions;
using Microsoft.Win32;
using Minio.DataModel;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfMinioTest1
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
        private static string levelOne = "";
        private static string levelTwo = DateTime.Now.ToShortDateString();
        string s = @"F:\data\company\device1";
        public MainWindow()
        {
            InitializeComponent();

            bool b=SubDirectoriesExistsAsync(s);

        }

        /// <summary>
        /// 初始化客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        ViewModelLocator.FileUploadViewModel.UploadProcess = "0.00%";
                        return;
                    }
                    ViewModelLocator.FileUploadViewModel.PartNumber = obj;
                    ViewModelLocator.FileUploadViewModel.UploadProcess =
                        $"{(float)ViewModelLocator.FileUploadViewModel.PartNumber / ViewModelLocator.FileUploadViewModel.TotalParts:P2}";//计算文件上传进度
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
                    await Run(_minioClient, "company1", open.FileName, ViewModelLocator.FileUploadViewModel.FileName);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task Run(MinioClient minio, string userBucketName, string uploadFilePath, string saveFileName)
        {
            var bucketName = userBucketName;//桶名
            var location = "us-east-1";//地址
            var filePath = uploadFilePath;//上传文件路径
            //var objectName = "device1/"+"20210107/"+saveFileName;//保存文件名
            var objectName = "device3" + "/" +levelTwo + "/" + saveFileName;
            var contentType = ContentTypeHelper.GetContentType(saveFileName.Substring(saveFileName.LastIndexOf('.') + 1));
            var file = new FileInfo(uploadFilePath);

            try
            {
                var found = await minio.BucketExistsAsync(bucketName);//判断是否存在桶名
                if (!found)
                {
                    await minio.MakeBucketAsync(bucketName, location);
                }

                _minioClient.SetTraceOn(new LogHelper());//我们在上传开始的时候，打开日志，通过日志抛出的块编号来计算出当前进度

                ViewModelLocator.FileUploadViewModel.FileSize = file.Length;
                ViewModelLocator.FileUploadViewModel.TotalParts = file.Length / App.MinimumPartSize + 1;//计算出文件总块数

                //20210109 插入内容，如果不存在二级、三级内容及内容，直接创建并插入，如果存在二级、三级及对应内容则不插入，并返回标识
                ObjectStat statObject = await PutObject_Tester(minio, bucketName, objectName, filePath, contentType);
                Assert.IsTrue(statObject != null);
                Assert.IsTrue(statObject.MetaData != null);
                 
                //await minio.PutObjectAsync(bucketName, objectName, filePath, contentType);//上传文件
                //await minio.PutObjectAsync();//上传文件
                 
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

        /// <summary>
        /// 检索文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGetObject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dispatcher?.InvokeAsync(async () =>
                {
                    await GetTask(_minioClient, "company1", "device3",true);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minio"></param>
        /// <returns></returns>
        private static async Task GetTask(MinioClient minio,string bucket,string objName,bool recursive)
        {
            try
            {
                var iObservable = minio.ListObjectsAsync(bucket, objName, recursive);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //ListObjectsArgs listArgs = new ListObjectsArgs()
            //                                        .WithBucket(bucket)
            //                                        .WithPrefix(objName)
            //                                        .WithRecursive(recursive);
            //IObservable<Item> observable = minio.ListObjectsAsync(listArgs);
        }
    
        public async static Task<ObjectStat> PutObject_Tester(MinioClient _minio, string bucketName, string objectName, string contentType, string fileName = null, Dictionary<string, string> metaData = null)
        {
            int count = 0;
            IObservable<Item> observable = _minio.ListObjectsAsync(bucketName, "device3", true);

            IDisposable subscription = observable.Subscribe(
                    item =>
                    { 
                        if (item.Key.StartsWith("device3"))
                        {
                            count += 1;
                            Console.WriteLine($"count: {count}");
                        }
                    },
                    ex => Console.WriteLine($"OnError: {ex}"),
                    () => Console.WriteLine($"Listed all objects in bucket {bucketName}\n"));

             
            ObjectStat statObject = await _minio.StatObjectAsync(bucketName, "device3");
            //await _minio.PutObjectAsync(bucketName,objectName,objectName,contentType);
             
            Assert.IsNotNull(statObject);
            Assert.AreEqual(statObject.ObjectName, objectName);
            //Assert.AreEqual(statObject.Size, file_read_size);
            if (contentType != null)
            {
                Assert.AreEqual(statObject.ContentType, contentType);
            }
            return statObject;
        }
   
        public bool SubDirectoriesExistsAsync(string subDirectoriesName)
        {
            bool b = false;
            try
            {
                if (Directory.Exists(subDirectoriesName))
                {
                    b = true;
                }
                else { b = false; }
            }
            catch (Exception)
            {
                b = false; 
            }
            return b;
        }
    }
}
