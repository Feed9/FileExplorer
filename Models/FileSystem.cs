using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
    public class FileSystem
    {
        public FileSystem(List<FileSystemElement> elements, string path)
        {
            Elements = elements;
            Path = path;
        }

        public List<FileSystemElement> Elements { get; init; }
        public string Path { get; init; }
    }
}
