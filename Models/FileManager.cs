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



        public List<PhysicalFileProvider> PhysicalFileProviders { get; set; }
        public bool SkipOsDirectory { get; set; } = true;

        public FileManager()
        {
            DriveInfo[] DriveInfos = DriveInfo.GetDrives();
            CreatePhysicalFileProviders(DriveInfos);
        }

        public FileSystem CreateFileSystemModel(List<FileSystemElement> elements, string path)
        {
            List<string> roots = GetDriversNames();
            return new FileSystem(elements, roots, path);
        }
        private List<string> GetDriversNames()
        {
            List<string> roots = new();
            foreach (var di in PhysicalFileProviders)
            {
                roots.Add(di.Root);
            }
            return roots;
        }
        private void CreatePhysicalFileProviders(DriveInfo[] DriveInfos)
        {
            PhysicalFileProviders = new List<PhysicalFileProvider>();
            foreach (var driver in DriveInfos)
            {
                PhysicalFileProviders.Add(new PhysicalFileProvider(driver.RootDirectory.FullName));
            }
        }
    }
}
