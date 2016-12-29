using System;
using System.IO;
using System.Linq;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupRenamingFiles : BaseCleanup
    {
        public CleanupRenamingFiles() : base(CleanupType.RenamingFiles)
        {
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(Settings.GetInstance().PathMovieFlyttes), "*.*", SearchOption.AllDirectories, 1).ToList();
            var casted = enumerable.OfType<FileInfo>().ToList();
            var grouped = casted.GroupBy(c => c.DirectoryName);
            foreach (var d in grouped)
            {
                var moviePath = d.Key;
                var movieName = new DirectoryInfo(moviePath).Name;
                foreach (var fileName in d)
                {
                    var newPath = Path.Combine(moviePath, movieName + fileName.Extension);
                    if (fileName.FullName == newPath) continue;
                    Console.WriteLine($"Renaming {fileName.FullName}-{newPath}");
                    File.Move(fileName.FullName, newPath);
                }
                ;
            }
            return true;
        }
    }
}