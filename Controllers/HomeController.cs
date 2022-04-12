using FileExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileManager _fileManager;

        public HomeController(ILogger<HomeController> logger, IFileManager fileManager)
        {
            _logger = logger;
            _fileManager = fileManager;
        }

        public IActionResult Index(string? path = "")
        {
            var provider = _fileManager.PhysicalFileProvider.FirstOrDefault();
            string fixedPath;
            string pathString;
            if (path == null)
            {
                fixedPath = "";
                pathString = provider.Root;
            }
            else
            {
                fixedPath = path.Replace(provider.Root, "");
                pathString = path == string.Empty ? provider.Root : path;
            }

            var directories = provider.GetDirectoryContents(fixedPath);

            IEnumerable<FileSystemElement> directoriesList = directories.Where(element => element.IsDirectory)
                 .Select(e => new SystemDirectory(e.PhysicalPath, e.Name, e.LastModified.UtcDateTime, null, e.IsDirectory, @"\icons\folder.svg"));

            IEnumerable<FileSystemElement> filesList = directories.Where(element => element.IsDirectory == false)
                 .Select(e => new SystemFile(e.PhysicalPath, e.Name, e.LastModified.UtcDateTime, e.Length, e.IsDirectory, @"\icons\file-earmark.svg"));

            List<FileSystemElement> elements = new();
            elements.AddRange(filesList);
            elements.AddRange(directoriesList);

            FileSystem fileSystem = new(elements, pathString);
            return View(fileSystem);
        }
        public async Task<IActionResult> Folder(int id)
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
