using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
    public class SystemDirectory : FileSystemElement
    {
        const string EXCEPTION_DIRECTORY = @"C:\Windows";
        public SystemDirectory(string path, string title, DateTime lastModified, long size, bool isDirectory, string iconPath)
            : base(path, title, lastModified, size, isDirectory, iconPath)
        {

        }

        public void CalculateDirectorySize(bool skipOsDirectory)
        {

            if (Path == EXCEPTION_DIRECTORY && skipOsDirectory) return;


            try
            {
                var files = new DirectoryInfo(Path).GetFiles("*.*", new EnumerationOptions()
                {
                    IgnoreInaccessible = true,
                    RecurseSubdirectories = true
                });

                Size = (files.Sum(e => e.Length)) / 1024;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }

}
