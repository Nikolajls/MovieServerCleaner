using System;
using System.IO;
using System.Linq;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupMoveSubtitles : BaseCleanup
    {
        public CleanupMoveSubtitles() : base(CleanupType.MoveSubtitles)
        {
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(Settings.GetInstance().PathMovieFlyttes), "*.*", SearchOption.AllDirectories, 2).ToList();
            var dirs = enumerable.OfType<DirectoryInfo>().ToList();
            var casted = enumerable.OfType<FileInfo>().ToList();
            foreach (var dir in dirs)
            {
                var dirNameLower = dir.Name.ToLower();
                if (!Settings.GetInstance().SubDirNames.Contains(dirNameLower)) continue;
                var remainingSubtitles = casted.Where(x => x.DirectoryName == dir.FullName).ToList();
                var parentDir = dir.Parent;
                if (parentDir == null) continue;
                ConsoleEx.WriteLine($"Moving files from '{RemoveRootMovieFolder(dir.FullName)}' to '{RemoveRootMovieFolder(parentDir.FullName)}'", ConsoleColor.DarkBlue);
                foreach (var m in remainingSubtitles)
                {
                    var newPath = Path.Combine(parentDir.FullName, m.Name);
                    ConsoleEx.WriteLine(RemoveRootMovieFolder(m.FullName) + "--> " + RemoveRootMovieFolder(newPath), ConsoleColor.DarkBlue);
                    File.Move(m.FullName, newPath);
                }
                Directory.Delete(dir.FullName);
            }
            return true;
        }

        private string RemoveRootMovieFolder(string s)
        {
            return s.Replace(Settings.GetInstance().PathMovieFlyttes, "");
        }
    }
}