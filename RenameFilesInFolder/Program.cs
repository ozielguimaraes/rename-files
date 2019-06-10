using System;
using System.IO;
using System.Linq;

namespace RenameFilesInFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type the repository of the files:");
            //var baseDirectory = Console.ReadLine();
            var baseDirectory = @"C:\Users\opaulasi\Downloads\bundle";

            Console.WriteLine("Type the OLD filename:");
            //var oldFileName = Console.ReadLine();
            var oldFileName = "screen";

            Console.WriteLine("Type the NEW filename:");
            //var newFileName = Console.ReadLine();
            var newFileName = "login-background";

            RenameFile(baseDirectory, oldFileName, newFileName);

            var folders = Directory.GetDirectories(baseDirectory);
            foreach (var item in folders)
            {
                RenameFile(item, oldFileName, newFileName);
            }

            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }

        private static void RenameFile(string fullName, string oldFileName, string newFileName)
        {
            var files = Directory.GetFiles(fullName).Where(x => x.ToLower().Contains(oldFileName.ToLower())).ToList();
            foreach (var item in files)
            {
                var paths = item.Split('\\');
                var fileNames = paths[paths.Length - 1].Split('.');
                var name = fileNames[fileNames.Length - 2];
                if (name != oldFileName) return;

                var extension = fileNames[fileNames.Length - 1];

                var index = item.IndexOf($"{name}.{extension}");
                var newName = item.Replace($"{name}.{extension}", $"{newFileName}.{extension}");
                File.Move(item, newName);
            }
            RenameFilesInFolder(fullName, oldFileName, newFileName);
        }

        private static void RenameFilesInFolder(string fullName, string oldFileName, string newFileName)
        {
            var folders = Directory.GetDirectories(fullName);
            foreach (var item in folders)
            {
                RenameFile(item, oldFileName, newFileName);
            }
        }
    }
}