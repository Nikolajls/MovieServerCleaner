using MovieServerCleaner.Models;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupRenameFolders : BaseCleanup
    {
        private readonly FolderSettings folderSettings;

        public override string OutputType => "Renaming moviefolders";
        public CleanupRenameFolders(FolderSettings folderSettings) : base(CleanupType.RenameFolders)
        {
            this.folderSettings = folderSettings;
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(folderSettings.PathWorkingFolder), "*.*", SearchOption.AllDirectories, 0).ToList();
            var movieDirs = enumerable.OfType<DirectoryInfo>().ToList();
            var Reg = new Regex(@"^(.+?)(\d{4})(.+)$", RegexOptions.IgnoreCase);
            foreach (var moviedir in movieDirs)
            {
                var dirname = moviedir.Name;
                var matches = Reg.Split(dirname);
                if (matches.Length != 5)
                {
                    ConsoleEx.WriteLine($"!'{dirname}' Didn't match regex", ConsoleColor.Cyan);
                    continue;
                }
                var newName = matches[1].Replace(".", " ");
                Console.WriteLine($"Renaming '{dirname}' to '{newName}'");
                var newPath = Path.Combine(folderSettings.PathWorkingFolder, newName);
                moviedir.MoveTo(newPath);
            }
            return true;
        }
    }
}