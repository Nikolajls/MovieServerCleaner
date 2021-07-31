using MovieServerCleaner.Models;
using System.IO;
using System.Linq;

namespace MovieServerCleaner.Cleanups
{
    internal class CleanupCheckForRars : BaseCleanup
    {
        private readonly FolderSettings folderSettings;

        public override string OutputType => "Checking for any exisiting rar files";

        public CleanupCheckForRars(FolderSettings folderSettings) : base(CleanupType.CheckForRars)
        {
            this.folderSettings = folderSettings;
        }

        public override bool Clean()
        {
            var enumerable = new FileSystemEnumerable(new DirectoryInfo(folderSettings.PathWorkingFolder), "*.*", SearchOption.AllDirectories, 2).ToList();
            return !enumerable.OfType<FileInfo>().Any(x => x.Extension.ToLower().StartsWith(".r"));
        }
    }
}