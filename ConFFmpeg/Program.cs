using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConFFmpeg
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }

        private static async Task Run()
        {
            //Queue filesToConvert = new Queue(GetFilesToConvert("C:\\movies"));
            //await Console.Out.WriteLineAsync($"Find {filesToConvert.Count()} files to convert.");
        }

        private static IEnumerable GetFilesToConvert(string directoryPath)
        {
            //Return all files excluding mp4 because I want convert it to mp4
            return new DirectoryInfo(directoryPath).GetFiles().Where(x => x.Extension != ".mp4");
        }

        private static async Task RunConversion(Queue filesToConvert)
        {
            //while (filesToConvert.TryDequeue(out FileInfo fileToConvert))
            //{
            //    //Save file to the same location with changed extension
            //    string outputFileName = Path.ChangeExtension(fileToConvert.FullName, ".mp4");
            //    await Conversion.ToMp4(fileToConvert.FullName, outputFileName).Start();
            //    await Console.Out.WriteLineAsync($"Finished converion file [{fileToConvert.Name}]");
            //}
        }
    }
}
