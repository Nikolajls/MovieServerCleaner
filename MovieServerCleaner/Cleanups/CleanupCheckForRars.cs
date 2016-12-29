using System.IO;
using System.Linq;

namespace MovieServerCleaner.Cleanups
{
    internal class CleanupCheckForRars : BaseCleanup
    {
        public CleanupCheckForRars() : base(CleanupType.CheckForRars)
        {
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(Settings.GetInstance().PathMovieFlyttes), "*.*", SearchOption.AllDirectories, 1).ToList();
            return !enumerable.OfType<FileInfo>().Any(x => x.Extension.ToLower().StartsWith(".r"));
        }
    }
}