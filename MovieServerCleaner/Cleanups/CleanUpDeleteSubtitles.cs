using System;
using System.IO;
using System.Linq;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupDeleteSubtitles : BaseCleanup
    {
        public CleanupDeleteSubtitles() : base(CleanupType.DeleteSubtitles)
        {
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(Settings.GetInstance().PathMovieFlyttes), "*.*", SearchOption.AllDirectories, 2).ToList();
            var casted = enumerable.OfType<FileInfo>().Where(x => x.Directory != null).ToList();
            foreach (var file in casted)
            {
                var name = file.Name.ToUpper();
                var extension = file.Extension.ToLower();
                if (extension != ".srt") continue;
                if (Settings.GetInstance().AllowedSubtitles.Contains(name)) continue;
                if (name.Length >= 8) continue;
                ConsoleEx.WriteLine($"Deleting subtitle '{file.Directory?.Parent?.Name}\\{file.Name}'", ConsoleColor.DarkRed);
                File.Delete(file.FullName);
            }
            return true;
        }
    }
}