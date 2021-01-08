using System;
using System.IO;
using System.Text.Json;


namespace ConJsonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new Person[10];
            for (var i = 0; i < 10; i++)
            {
                var data = new Person
                {
                    Name = "Changwei_" + i,
                    Age = i + 20
                }; 
                list[i] = data; 
            }

            var jsonSerializer = new JsonSerializer();
            var stringWriter = new StringWriter();
            var jsonWriter = new JsonTextWriter(stringWriter);
            jsonSerializer.Serialize(jsonWriter, list);

            Console.WriteLine(stringWriter.ToString());

            var jsonArray = JArray.FromObject(list);
            Console.WriteLine(jsonArray.ToString());
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
