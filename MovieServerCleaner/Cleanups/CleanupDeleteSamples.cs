using System;
using System.IO;
using System.Linq;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupDeleteSamples : BaseCleanup
    {
        public CleanupDeleteSamples() : base(CleanupType.DeleteSamples)
        {
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(Settings.GetInstance().PathMovieFlyttes), "*.*", SearchOption.AllDirectories, 2).ToList();
            var casted = enumerable.OfType<FileInfo>().Where(fi => fi.Directory != null).ToList();
            foreach (var file in casted)
            {
                var name = file.Name.ToUpper();
                if (!name.Contains("SAMPLE")) continue;
                ConsoleEx.WriteLine($"Delete sample '{file.Directory?.Parent?.Name}\\{file.Name}'", ConsoleColor.DarkRed);
                File.Delete(file.FullName);
            }
            return true;
        }
    }
}