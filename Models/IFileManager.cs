using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
    public interface IFileManager
    {
        List<PhysicalFileProvider> PhysicalFileProviders { get; set; }
        public bool SkipOsDirectory { get; set; }
        public FileSystem CreateFileSystemModel(List<FileSystemElement> elements, string path);
    }
}
