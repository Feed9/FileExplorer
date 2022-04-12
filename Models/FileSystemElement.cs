using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
    public abstract class FileSystemElement
    {
        public FileSystemElement(string path, string title, DateTime lastModified, long? size,bool? isDirectory,string iconPath)
        {
            Path = path;
            Title = title;
            LastModified = lastModified;
            Size = size ?? 0;
            IsDirectory = isDirectory ?? false;
            IconPath = iconPath ?? "";
        }
        public string Path { get; init; }
        public string Title { get; init; }
        public string Type { get; init; }
        public bool IsDirectory { get; set; } = false;
        public DateTime LastModified { get; init; }
        public  long Size { get; set; }
        public  string IconPath { get; set; }

    }
}
