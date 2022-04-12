using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
    public static class SystemDirectoryExtensions
    {
        public async static Task CalculateSizeForDirectoriesAsync(this List<SystemDirectory> directoriesList)
        {

            for (int i = 0; i < directoriesList.Count; i++)
            {

               await Task.Run(() => directoriesList[i].CalculateDirectorySize());
            }
        }
    }
}
