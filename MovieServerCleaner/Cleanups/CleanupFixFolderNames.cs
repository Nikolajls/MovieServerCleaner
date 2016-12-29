using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupRenameFolders : BaseCleanup
    {
        public CleanupRenameFolders() : base(CleanupType.RenameFolders)
        {
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(Settings.GetInstance().PathMovieFlyttes), "*.*", SearchOption.AllDirectories, 0).ToList();
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
                var newPath = Path.Combine(Settings.GetInstance().PathMovieFlyttes, newName);
                moviedir.MoveTo(newPath);
            }
            return true;
        }
    }
}