using System;
using System.Collections.Generic;
using System.IO;

namespace ConDirectory
{
    class Program
    {
        static void Main(string[] args)
        {

            string s = @"F:\data";
            string[] dirs = Directory.GetDirectories(s);
            List<string> list = new List<string>();
            foreach (string item in dirs)
            {
                list.Add(Path.GetFileNameWithoutExtension(item));
            }
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }


            //string str = "";
            //for (int i = 0; i < 3; i++)
            //{
            //    str = "第"+ i.ToString()+"级" + "\\新建文件夹";
            //}
            //System.IO.Directory.CreateDirectory(@"F:\" + str);

            //string s = @"F:\第2级\新建文件夹";
            //string s = "F:\\第2级\\新建文件夹";
            //listDirectory(s);


            //string[] dirs = Directory.GetDirectories(s);
            //List<string> list = new List<string>();
            //foreach (string item in dirs)
            //{
            //    list.Add(Path.GetFileNameWithoutExtension(item));
            //}
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.ToString());
            //    //MessageBox.Show(item.ToString());
            //}

            Console.ReadLine(); 
        }

        private static void listDirectory(string path)
        {
            DirectoryInfo theFolder = new DirectoryInfo(@path);

            //遍历文件
            foreach (FileInfo NextFile in theFolder.GetFiles())
            {
                Console.WriteLine(path + NextFile.Name + "\r\n");
                //richTextBox1.AppendText(path + NextFile.Name + "\r\n");//文件路径
            }

            //遍历文件夹
            foreach (DirectoryInfo NextFolder in theFolder.GetDirectories())
            {
                listDirectory(NextFolder.FullName);
            }
        }

        /// <summary>
        /// 获取目录文件夹下的所有子目录
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="filePattern"></param>
        /// <returns></returns>
        public static List<string> FindSubDirectories(string directory, int maxCount)
        {
            List<string> subDirectories = new List<string>();
            if (string.IsNullOrEmpty(directory))
            {
                return subDirectories;
            }
            if (maxCount <= 0)
            {
                return subDirectories;
            }
            string[] directories = Directory.GetDirectories(directory);
            foreach (string subDirectory in directories)
            {
                if (subDirectories.Count == maxCount)
                {
                    break;
                }
                subDirectories.Add(subDirectory);
            }
            return subDirectories;
        } 
    } 
}
