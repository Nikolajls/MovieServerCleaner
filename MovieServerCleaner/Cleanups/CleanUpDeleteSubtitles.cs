using MovieServerCleaner.Models;
using System;
using System.IO;
using System.Linq;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupDeleteSubtitles : BaseCleanup
    {
        private readonly FolderSettings folderSettings;

        public override string OutputType => "Deleting all irrelevant subtitles";
        public CleanupDeleteSubtitles(FolderSettings folderSettings) : base(CleanupType.DeleteSubtitles)
        {
            this.folderSettings = folderSettings;
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(folderSettings.PathWorkingFolder), "*.*", SearchOption.AllDirectories, 2).ToList();
            var casted = enumerable.OfType<FileInfo>().Where(x => x.Directory != null).ToList();
            foreach (var file in casted)
            {
                var DirectoryName = file.Directory.Name.ToUpper();
                var filename = file.Name.ToUpper().Replace(file.Extension.ToUpper(), "");
                var name = file.Name.ToUpper();
                var extension = file.Extension.ToLower();
                if (extension != ".srt") continue;
                if (DirectoryName == filename) continue;
                if (Settings.Instance.AllowedSubtitles.Contains(name)) continue;
                var nameContainsDK = name.Contains("DANISH") || name.Contains("DANSK") || name.Contains(".DA.") || name.Contains(".DK.");
                if (name.Length >= 8 && nameContainsDK) continue;


                ConsoleEx.WriteLine($"Deleting subtitle '{file.Directory?.Parent?.Name}\\{file.Name}'", ConsoleColor.DarkRed);
                File.Delete(file.FullName);
            }
            return true;
        }
    }
}