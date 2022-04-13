using FileExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Controllers
{
    public class FileExplorerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileManager _fileManager;

        public FileExplorerController(ILogger<HomeController> logger, IFileManager fileManager)
        {
            _logger = logger;
            _fileManager = fileManager;
        }
        public async Task<IActionResult> Index(string path = "", SortOrder sortOrder = SortOrder.TitleAsc)
        {
            PhysicalFileProvider provider;

            if (path == null || path.Length < 3)
            {
                provider = _fileManager.PhysicalFileProviders.FirstOrDefault();
            }
            else
            {
                var root = path.Substring(0, 3);
                provider = _fileManager.PhysicalFileProviders.FirstOrDefault(pfv => pfv.Root.Equals(root));
            }

            if (provider == null)
            {
                FileSystem BrokenFileSystem = _fileManager.CreateFileSystemModel(null, path);
                return View(BrokenFileSystem);
            }

            string fixedPath = string.Empty;
            string pathString = provider.Root;

            if (path != null && path != string.Empty)
            {
                fixedPath = path.Replace(provider.Root, string.Empty);
                pathString = path;
            }

            var content = provider.GetDirectoryContents(fixedPath);

            List<SystemDirectory> directoriesList = content.Where(element => element.IsDirectory)
                 .Select(e => new SystemDirectory(e.PhysicalPath, e.Name, e.LastModified.UtcDateTime,
                 0, e.IsDirectory, @"\icons\folder.svg")).ToList();

            await directoriesList.CalculateSizeForDirectoriesAsync(_fileManager.SkipOsDirectory);

            List<SystemFile> filesList = content.Where(element => element.IsDirectory == false)
                 .Select(e => new SystemFile(e.PhysicalPath, e.Name, e.LastModified.UtcDateTime,
                 e.Length, e.IsDirectory, @"\icons\file-earmark.svg")).ToList();

            List<FileSystemElement> elements = new();
            elements.AddRange(directoriesList);
            elements.AddRange(filesList);


            FileSystem fileSystem = _fileManager.CreateFileSystemModel(elements, pathString);
            fileSystem.Sort(sortOrder);

            ViewBag.TitleSort = sortOrder == SortOrder.TitleAsc ? SortOrder.TitleDesc : SortOrder.TitleAsc;
            ViewBag.SizeSort = sortOrder == SortOrder.SizeAsc ? SortOrder.SizeDesc : SortOrder.SizeAsc;
            ViewBag.DateSort = sortOrder == SortOrder.DateAsc ? SortOrder.DateDesc : SortOrder.DateAsc;
            return View(fileSystem);
        }
        public IActionResult Presetting(bool skipOsDirectory)
        {
            _fileManager.SkipOsDirectory = skipOsDirectory;
            return Redirect("~/FileExplorer/Index");
        }
    }
}
