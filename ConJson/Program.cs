using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace ConJson
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonText = " {\"companyID\":\"15\",\"employees\":[{\"firstName\":\"Bill\",\"lastName\":\"Gates\"},{\"firstName\":\"George\",\"lastName\":\"Bush\"}],\"manager\":[{\"salary\":\"6000\",\"age\":\"23\"},{\"salary\":\"8000\",\"age\":\"26\"}]} ";
            string jsonText1 = "{\"pn\":\"123456789\",\"mac\":\"abcdefg\",\"date\":\"2020-12-16\"}";
             
            Console.WriteLine(jsonText);

            Test rb = JsonConvert.DeserializeObject<Test>(jsonText1);

            Console.WriteLine(rb.pn);
            Console.WriteLine(rb.mac);
            Console.WriteLine(rb.date);

            //RootObject rb = JsonConvert.DeserializeObject<RootObject>(jsonText);

            //Console.WriteLine(rb.companyID);

            //Console.WriteLine(rb.employees[0].firstName);

            //foreach (Manager ep in rb.manager)
            //{
            //    Console.WriteLine(ep.age);
            //}

            Console.ReadKey(); 
        }

        /// <summary>
        /// 反序列化 字符串到对象
        /// </summary>
        /// <param name="obj">泛型对象</param>
        /// <param name="str">要转换为对象的字符串</param>
        /// <returns>反序列化出来的对象</returns>
        public static T Desrialize<T>(string str)
        {
            T obj;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                byte[] buffer = Convert.FromBase64String(str);
                MemoryStream stream = new MemoryStream(buffer);
                obj = (T)formatter.Deserialize(stream);
                stream.Flush();
                stream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("反序列化失败,原因:" + ex.Message);
            }
            return obj;
        }
    }

    public class Test
    {
        public string pn { get; set; }
        public string mac { get; set; }
        public string date { get; set; }
    }

    public class Employees
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
     
    public class Manager
    {
        public string salary { get; set; }
        public string age { get; set; }
    }

    public class RootObject
    {
        public string companyID { get; set; }


        public List<Employees> employees { get; set; }
        public List<Manager> manager { get; set; }
    }
}
