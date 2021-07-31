using MovieServerCleaner.Models;
using System;
using System.IO;
using System.Linq;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupRenamingFiles : BaseCleanup
    {
        private readonly FolderSettings folderSettings;

        public override string OutputType => "Renaming all files in directory";
        public CleanupRenamingFiles(FolderSettings folderSettings) : base(CleanupType.RenamingFiles)
        {
            this.folderSettings = folderSettings;
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(folderSettings.PathWorkingFolder), "*.*", SearchOption.AllDirectories, 1).ToList();
            var casted = enumerable.OfType<FileInfo>().ToList();
            var grouped = casted.GroupBy(c => c.DirectoryName);
            foreach (var d in grouped)
            {
                var moviePath = d.Key;
                var movieName = new DirectoryInfo(moviePath).Name;
                foreach (var fileName in d)
                {
                    var newPath = Path.Combine(moviePath, movieName + fileName.Extension);
                    if (fileName.FullName.ToLower() == newPath.ToLower()) continue;
                    var safeFilename = GetSafeName(new FileInfo(newPath), 0);
                    Console.WriteLine($"Renaming {fileName.FullName}-{safeFilename}");

                    File.Move(fileName.FullName, safeFilename);
                }
                ;
            }
            return true;
        }

        public static string GetSafeName(FileInfo nfo, int tryCount)
        {
            var fileName = Path.GetFileNameWithoutExtension(nfo.Name);
            var directoryName = nfo.Directory?.FullName;
            var dstName = fileName;
            if (tryCount > 0)
                dstName = dstName + $" ({tryCount})";
            dstName = dstName + nfo.Extension;
            var newNfoPath = Path.Combine(directoryName, dstName);
            if (!File.Exists(newNfoPath)) return newNfoPath;
            tryCount++;
            return GetSafeName(nfo, tryCount);
        }
    }
}