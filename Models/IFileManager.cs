using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
   public interface IFileManager
    {
         List<PhysicalFileProvider> PhysicalFileProvider { get; set; }
    }
}
