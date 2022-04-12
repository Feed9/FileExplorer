using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
    public class SystemFile : FileSystemElement
    {
        public SystemFile(string path, string title, DateTime lastModified, long size, bool isDirectory, string iconPath) 
            : base(path, title, lastModified, size, isDirectory, iconPath)
        {
        }        
    }
}
