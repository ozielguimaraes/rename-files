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
            var baseDirectory = Console.ReadLine();

            Console.WriteLine("Type the OLD filename(include the extension):");
            var oldFileName = Console.ReadLine();

            Console.WriteLine("Type the NEW filename(include the extension):");
            var newFileName = Console.ReadLine();

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
                var newName = item.Replace(oldFileName, newFileName);
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