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
        public FileSystem FileSystem { get; set; }
        public List<PhysicalFileProvider> PhysicalFileProvider { get; set; }

        public FileManager()
        {
            DriveInfos = DriveInfo.GetDrives();
            PhysicalFileProvider = new(); 
            PhysicalFileProvider.Add(GetPhysicalFileProvider(DriveInfos.FirstOrDefault().RootDirectory.FullName));
           // PhysicalFileProvider.Add(GetPhysicalFileProvider("C:"));
        }      



        public PhysicalFileProvider GetPhysicalFileProvider(string path)
        {
            return new PhysicalFileProvider(path);
        }
    }
}
