using MovieServerCleaner.Models;
using System;
using System.IO;
using System.Linq;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupMoveSubtitles : BaseCleanup
    {
        private readonly FolderSettings folderSettings;

        public override string OutputType => "Moving remaining subtitles";
        public CleanupMoveSubtitles(FolderSettings folderSettings) : base(CleanupType.MoveSubtitles)
        {
            this.folderSettings = folderSettings;
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(folderSettings.PathWorkingFolder), "*.*", SearchOption.AllDirectories, 2).ToList();
            var dirs = enumerable.OfType<DirectoryInfo>().ToList();
            var casted = enumerable.OfType<FileInfo>().ToList();
            foreach (var dir in dirs)
            {
                var dirNameLower = dir.Name.ToLower();
                if (!Settings.Instance.SubDirNames.Contains(dirNameLower)) continue;
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
            return s.Replace(folderSettings.PathWorkingFolder, "");
        }
    }
}