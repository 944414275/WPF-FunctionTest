using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConMinIO
{
    class Program
    {
        //private static string testStr = "qwertqewrwrwtrreywtrywsdghf";
        //FileStream fileStream = new FileStream("F:\\data\\rujia\\citanji\\20210331\\132616544640166034", FileMode.OpenOrCreate, System.IO.FileAccess.Read);
        //static FileStream fileStream = new FileStream("G:\\手机文件\\半身照1.jpg", FileMode.OpenOrCreate, System.IO.FileAccess.Read);
        //static string filePath = "G:\\手机文件\\半身照1.jpg";
        //static string filePath1 = "G:\\手机文件\\明朝那些事\\明朝那些事儿_001.mp3";
        static string filePath2 = "F:\\data\\rujia002\\citanji002\\20210427\\111115.png";
        static string filePath3 = "F:\\data\\rujia002\\citanji002\\20210427\\112.png";
        static FileStream fileStream = new FileStream("F:\\Media\\泰安特检院\\手机录屏.mp4", FileMode.OpenOrCreate, System.IO.FileAccess.Read);



        static void Main(string[] args)
        {
            var minio = new MinioClient("172.16.18.73:9000", "minioadmin", "minioadmin"); 
            Program.Run(minio).Wait(); 
            //ListObjects_Test1(minio,"company1","device2",2, false).Wait(); 
            //PutObject_Test1(minio, "company1", "device3").Wait();
            Console.ReadLine();
        }

        // File uploader task.
        private async static Task Run(MinioClient minio)
        {
            var location = "us-east-1";
            var companyName = "company15";//一级目录桶名 
            string deviceName = "device1"; //二级目录设备名
            string dateName = DateTime.Now.ToString("yyyyMMdd");//三级目录时间
            //string pictureName = "111113.zip";
            //string pictureName1 = "111115.jpg";
            string pictureName2 = "111116.mp4";


            //上面这几个目录都是app上传的，下面组合起来
            string routeStr = string.Format("{0}/{1}/{2}", deviceName, dateName, pictureName2);
            //string fullRoute = "";
            //int lev = 0;
            //string fullRoute = ObjectExistsAsync(companyName, routeStr);

            //var objectName = "golden-oldies/"+ "dataStr";//第二步改存储文件名，最后一级是文件名，前面是路由文件夹
            //var filePath = "F:\\文档\\202103\\图像识别\\20210201090237.avi";
            //var contentType = "application/x-jpg";
            //var contentType = "image/jpeg";
            //var contentType = "image/png";
            var contentType = "video/mp4";


            //string contentType1 = "application/zip";

            //byte[] bs = File.ReadAllBytes("F:\\文档\\202103\\图像识别\\20210201090237.avi");
            //MemoryStream filestream = new MemoryStream(bs);

            //byte[] array = Encoding.ASCII.GetBytes(testStr);
            //MemoryStream stream = new MemoryStream(array);

            try
            {
                // Make a bucket on the server, if not already present.
                bool found = await minio.BucketExistsAsync(companyName);
                if (!found)
                {
                    await minio.MakeBucketAsync(companyName, location);
                }
                // Upload a file to bucket.
                //await minio.PutObjectAsync(companyName, routeStr, filePath, contentType);//图片类型
                //await minio.PutObjectAsync(companyName, routeStr, filePath1, contentType1);//MP3类型
                //await minio.PutObjectAsync(companyName, routeStr, filePath);
                await minio.PutObjectAsync(companyName, routeStr, fileStream, fileStream.Length, contentType);//20210428 此方法不加contentType也可以
                //await minio.PutObjectAsync(companyName, routeStr, filePath3, contentType);
                 
                Console.WriteLine("Successfully uploaded " + routeStr);
            }
            catch (MinioException e)
            {
                Console.WriteLine("File Upload Error: {0}", e.Message);
            }
        }


        /// <summary>
        /// Test ListObjectAsync function
        /// </summary>
        /// <param name="_minio"></param>
        /// <returns></returns>
        public async static Task ListObjects_Test1(MinioClient minio, string bucketName, string prefix, int numObjects, bool recursive = true)
        {
            int count = 0;
            IObservable<Item> observable = minio.ListObjectsAsync(bucketName, prefix, recursive);
            
            IDisposable subscription = observable.Subscribe(
                    item =>
                    {
                        if(item.Key.StartsWith(prefix))
                        {
                            count += 1;
                            Console.WriteLine($"count: {count}");
                        } 

                    }, 
                    ex => Console.WriteLine($"OnError: {ex}"),
                    () => Console.WriteLine($"Listed all objects in bucket {bucketName}\n"));

            //observable.Subscribe(
            //    item =>
            //    {
            //        Assert.IsTrue(item.Key.StartsWith(prefix));
            //    }
            //    );
            //foreach (Item item in )
            //{
            //    // Ignore
            //    continue;
            //}

            //IDisposable subscription = observable.Subscribe(
            //    item =>
            //    {
            //        Assert.IsTrue(item.Key.StartsWith(prefix));
            //        count += 1;
            //    },
            //    ex => throw ex,
            //    () =>
            //    {
            //        Assert.AreEqual(count, numObjects);
            //    });
        }
        
        public async static Task PutObject_Test1(MinioClient _minio, string bucketName, string objectName)
        {
            ObjectStat statObject = await _minio.StatObjectAsync(bucketName, objectName); 
            Assert.IsNotNull(statObject);
            Assert.AreEqual(statObject.ObjectName, objectName); 
        }

        /// <summary>
        /// 判断路径是否存在，并创建完整目录并返回
        /// </summary>
        /// <param name="routeName">相对路径</param>
        /// <returns></returns>
        public static bool ObjectExistsAsync(string bucketName, string routeName)
        {
            string routePrx = "F:\\data\\"+ bucketName;
            string[] routeArray = routeName.Split('/');//分级路径

            //做个循环一级级判断路径
            foreach(string route in routeArray)
            {
                //StringBuilder routeP = new StringBuilder("F:\\data");
                routePrx+="\\"+ route;
                if (!Directory.Exists(routePrx))//不存在该文件夹
                    break;
                //{
                //    //创建目录并终止遍历返回
                //    routePrx = routePrx;
                //    break;
                //} 
            } 
            //return routePrx;
            //string encodeRoute = string.Format(@"{0}/{1}/{2}", routePrx, bucketName, routeArray[0]);
            //第一步，传进来相对路径，
            //首先判断第一级的目录是否存在，如果存在在判断第二季目录是否存在，
            //try
            //{
            //    // Set a variable to the My Documents path.
            //    //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //    //List<string> dirs = new List<string>(Directory.EnumerateDirectories(encodeRoute));

            //    //foreach (var dir in dirs)
            //    //{
            //    //    Console.WriteLine($"{dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1)}");
            //    //}
            //    //Console.WriteLine($"{dirs.Count} directories found.");
            //    //if(Directory.EnumerateDirectories(encodeRoute))

            //    //if (Directory.Exists(encodeRoute))//判断路径是否存在
            //    //    return true;
            //    //else return false;
            //}
            //catch (Exception)
            //{

            //    throw;

            //}

            return true;
        }

        public async static Task ExistsAsync(MinioClient minio, string oneLev,string twoLev,string threeLev)
        {
            StringBuilder routePrx = new StringBuilder("F:\\data");
            routePrx.AppendFormat("{0}");
            var location = "us-east-1";
            bool found = await minio.BucketExistsAsync(oneLev);
            if (!found)
            {
                await minio.MakeBucketAsync(oneLev, location);
                //直接返回，一级目录都不存在，其他的更不存在了
            }
            //如果一级目录存在，则判断设备层存不存在
            //if ()


        }
    }
}
