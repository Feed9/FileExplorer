using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
    interface INavigation
    {
        public string Path { get; set; }        
        public string Up { get;  init; }
    }
}
