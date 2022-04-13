using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
    public class FileSystem : INavigation
    {
        public FileSystem()
        {
        }

        public FileSystem(List<FileSystemElement> elements, List<string> roots, string path)
        {
            Elements = elements;
            Roots = roots;
            Path = path;
            Up = GoUpPath(path);
        }

        public List<FileSystemElement> Elements { get; set; }
        public List<string> Roots { get; set; }
        public string Path { get; set; }
        public string Up { get; init; }
        private string GoUpPath(string path)
        {
            string previous = path.TrimEnd(new Char[] { '\\' });
            if (-1 == previous.LastIndexOf(@"\"))
            {
                return previous + @"\";
            }

            previous = previous.Remove(previous.LastIndexOf(@"\"));
            return previous + @"\";
        }
        public void Sort(SortOrder sortOrder)
        {
            if (Elements == null) return;

            switch (sortOrder)
            {
                case SortOrder.TitleDesc:
                    Elements = Elements.OrderByDescending(e => e.Title).ToList();
                    break;
                case SortOrder.SizeAsc:
                    Elements = Elements.OrderBy(e => e.Size).ToList();
                    break;
                case SortOrder.SizeDesc:
                    Elements = Elements.OrderByDescending(e => e.Size).ToList();
                    break;
                case SortOrder.DateAsc:
                    Elements = Elements.OrderBy(e => e.LastModified).ToList();
                    break;
                case SortOrder.DateDesc:
                    Elements = Elements.OrderByDescending(e => e.LastModified).ToList();
                    break;
                default:
                    Elements = Elements.OrderBy(e => e.Title).ToList();
                    break;
            }
        }
    }
}
