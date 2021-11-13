using System;
using Newtonsoft.Json;
using RestSharp;

namespace ConRestSharp1
{
    class Program
    {
        static void Main(string[] args)
        {

            #region postman
            var client = new RestClient("https://localhost:5001/WeatherForecast");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            #endregion

            ////方法二、使用第三方RestSharp
            //var client = new RestClient("https://172.16.18.73:5001/api/UploadFlawPic/GetTest");
            //#region Get
            //var requestGet = new RestRequest("Default", Method.GET);
            ////requestGet.AddUrlSegment("name", "王二麻子");
            //IRestResponse response = client.Execute(requestGet);
            //var contentGet = response.Content;
            //Console.WriteLine("GET方式获取结果：" + contentGet);
            //#endregion


            #region Post
            //var requestPost = new RestRequest("PersonInfoQuery/Info", Method.POST);
            //Info info = new Info();
            //info.ID = 1;
            //info.Name = "张三";
            //var json = JsonConvert.SerializeObject(info);
            //requestPost.AddParameter("application/json", json, ParameterType.RequestBody);
            //IRestResponse responsePost = client.Execute(requestPost);
            //var contentPost = responsePost.Content;
            //Console.WriteLine("POST方式获取结果：" + contentPost);
            //Console.Read();
            #endregion



            //string content = "fengbi"; //要发送的内容
            //string contentType = "application/json;charset=UTF-8"; //Content-Type
            //try
            //{
            //    var client = new RestClient("https://172.16.18.73:5001/api/UploadFlawPic/UploadFlowPic");

            //    var request = new RestRequest(Method.POST);
            //    request.Timeout = 10000;
            //    //request.AddHeader("token", token);
            //    request.AddParameter(contentType, content, ParameterType.RequestBody);

            //    IRestResponse response = client.Execute(request);
            //    var str= response.Content; //返回的结果
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("连接服务器出错：\r\n" + ex.Message); 
            //}
        }
    }

    [Serializable]
    public class Info
    {
        public int ID { get; set; }
        public string Name { get; set; }
    } 
}
