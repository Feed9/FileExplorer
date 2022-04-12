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

        public async Task<IActionResult> Index(string path = "")
        {
            var provider = _fileManager.PhysicalFileProviders.FirstOrDefault();
            string fixedPath = string.Empty;
            string pathString = provider.Root;

            if (path != null && path != string.Empty)
            {
                fixedPath = path.Replace(provider.Root, string.Empty);
                pathString = path;
            }

            var directories = provider.GetDirectoryContents(fixedPath);

            List<SystemDirectory> directoriesList = directories.Where(element => element.IsDirectory)
                 .Select(e => new SystemDirectory(e.PhysicalPath, e.Name, e.LastModified.UtcDateTime,
                 0,e.IsDirectory, @"\icons\folder.svg")).ToList();

            List<SystemFile> filesList = directories.Where(element => element.IsDirectory == false)
                 .Select(e => new SystemFile(e.PhysicalPath, e.Name, e.LastModified.UtcDateTime,
                 e.Length, e.IsDirectory, @"\icons\file-earmark.svg")).ToList();

            await directoriesList.CalculateSizeForDirectoriesAsync();

            List<FileSystemElement> elements = new();
            elements.AddRange(directoriesList);
            elements.AddRange(filesList);


            FileSystem fileSystem = _fileManager.CreateFileSystemModel(elements, pathString);
            ViewBag.Path = path;
            return View(fileSystem);
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
