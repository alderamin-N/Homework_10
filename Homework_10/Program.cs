using System.IO;
using System;
using System.Runtime;
using System.Text;

namespace Homework_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Otus";
            string subpath = @"TestDir1";
            string subpath2 = @"TestDir2";
            List<string> filesName = new List<string>();
            DirectoryInfo directory1 = new DirectoryInfo(path);
            if (!directory1.Exists)
            {
                directory1.Create();
            }
            directory1.CreateSubdirectory(subpath);
            directory1.CreateSubdirectory(subpath2);
            if (directory1.Exists)
            {

                DirectoryInfo[] dirs = directory1.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    Console.WriteLine(dir.FullName);
                    for (int i = 1; i <= 10; i++)
                    {
                        string fileName = Path.Combine(dir.FullName, $"File{i}.txt");
                        File.Create(fileName).Close();

                        filesName.Add(fileName);
                    }
                }
                foreach (string filename in filesName)
                {
                    if (File.Exists(filename) )
                    {
                        try 
                        {                          
                            using (FileStream fs = File.OpenWrite(filename))
                            {
                                byte[] info = new UTF8Encoding(true).GetBytes(filename);
                                fs.Write(info);
                                byte[] info2 = new UTF8Encoding(true).GetBytes(" "+ DateTime.Now.ToString());                               
                                fs.Write(info2);
                            }                                                 
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            Console.WriteLine($"Доступ к файлу {filename} запрещен!");
                        }                       
                    }
                }
                foreach (string filename in filesName)
                {
                    if (File.Exists(filename))
                    {                       
                       string text =  File.ReadAllText(filename);
                       DateTime time = File.GetCreationTime(filename);
                        Console.WriteLine($"{filename}: {text} + {time}");
                    }
                }
            }
        }
    }
}