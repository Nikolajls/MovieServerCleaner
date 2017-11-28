using System;
using System.IO;
using System.Linq;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupCopyNfoFiles : BaseCleanup
    {
        public CleanupCopyNfoFiles() : base(CleanupType.CopyfoFiles)
        {
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(Settings.GetInstance().PathMovieFlyttes), "*.*", SearchOption.AllDirectories, 1).ToList();
            var nfos = enumerable.OfType<FileInfo>().Where(p => p.Extension.ToLower() == ".nfo").ToList();
            foreach (var nfo in nfos)
            {
                var newNfoPath = GetSafeNfoName(nfo, 0);
                ConsoleEx.WriteLine($"Moving '{nfo.Name}' to '{newNfoPath.Replace(Settings.GetInstance().PathWorkingFolder, "")}'", ConsoleColor.DarkRed);
                File.Copy(nfo.FullName, newNfoPath);
            }
            return true;
        }

        public static string GetSafeNfoName(FileInfo nfo, int tryCount)
        {
            var fileName = Path.GetFileNameWithoutExtension(nfo.Name);
            var dstName = fileName;
            if (tryCount > 0)
                dstName = dstName + $" ({tryCount})";
            dstName = dstName + nfo.Extension;
            var newNfoPath = Path.Combine(Settings.GetInstance().PathNfoFolder, dstName);
            if (File.Exists(newNfoPath))
            {
                tryCount++;
                return GetSafeNfoName(nfo, tryCount);
            }
            return newNfoPath;
        }
    }
}