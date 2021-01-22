using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minio;
using Minio.DataModel;
using System;
using System.Threading.Tasks;

namespace ConMinIO
{
    class Program
    {
        static void Main(string[] args)
        {
            var minio = new MinioClient("172.16.18.73:9000", "minioadmin", "minioadmin");
             
            ListObjects_Test1(minio,"company1","device2",2, false).Wait();

            PutObject_Test1(minio, "company1", "device3").Wait();
            Console.ReadLine();
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
    }
}
