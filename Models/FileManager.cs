using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
    public class FileManager : IFileManager
    {


        public DriveInfo[] DriveInfos { get; set; }
        public List<PhysicalFileProvider> PhysicalFileProviders { get; set; }

        public FileManager()
        {
            DriveInfos = DriveInfo.GetDrives();
            CreatePhysicalFileProviders();
        }

        public FileSystem CreateFileSystemModel(List<FileSystemElement> elements, string path)
        {
            return new FileSystem(elements, path);
        }
        private void CreatePhysicalFileProviders()
        {
            PhysicalFileProviders = new List<PhysicalFileProvider>();
            foreach (var driver in DriveInfos)
            {
                PhysicalFileProviders.Add(new PhysicalFileProvider(driver.RootDirectory.FullName));
            }
        }
    }
}
