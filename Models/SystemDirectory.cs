using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Models
{
    public class SystemDirectory : FileSystemElement
    {

        public SystemDirectory(string path, string title, DateTime lastModified, long? size, bool? isDirectory, string iconPath)
            : base(path, title, lastModified, size, isDirectory, iconPath)
        {
            Size = CalculateDirectorySize(path);
        }

        private long CalculateDirectorySize(string path)
        {
            long size = 0;
            DirectoryInfo dir = new(path);
            try
            {
                foreach (FileInfo fi in dir.GetFiles("*.*", SearchOption.AllDirectories))
                {                    
                    size += fi.Length;
                }
            }
            catch (Exception e)
            {                
                Console.WriteLine(e.Message);
            }
            return size;
        }
    }




    //Directory.GetFiles(@"MainFolderPath", "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length));

    //long dirSize = await Task.Run(() => dirInfo.EnumerateFiles( "*", SearchOption.AllDirectories).Sum(file => file.Length));

    /*
     long size = 0;
    DirectoryInfo dir = new DirectoryInfo(folder);
    foreach (FileInfo fi in dir.GetFiles("*.*", SearchOption.AllDirectories))
    {
       size += fi.Length;
    }*/




    //public static long Id { get; private set; }

    //public List<FileSystemElement> Children { get; }

    //public Directory(string title, DateTime creationTime,string type = "directory") : base(title, creationTime,type)
    //{
    //    Children = new();
    //    Id++; 
    //}

    //public FileSystemElement AddChild(FileSystemElement element)
    //{
    //    Add(element);
    //    return this;
    //}
    //protected override FileSystemElement Add(FileSystemElement element)
    //{
    //    Children.Add(element);
    //    RecalculateFolderSize(element.Size);
    //    return this;
    //}
    //private void RecalculateFolderSize(long size)
    //{
    //    Size += size;
    //}
    //public override void PrintInfo(string info)
    //{
    //    Console.WriteLine($"{info} -Folder: {Title} -Size: {Size} -CreationTime: {CreationTime}");
    //    foreach (var child in Children)
    //    {
    //        child.PrintInfo($"{info}  ");
    //    }
    //}

}
