using MovieServerCleaner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieServerCleaner.Cleanups
{
    public class CleanupStart
    {
        public bool Cleanup(FolderSettings folderSettings)
        {
            //if (!new CleanupCheckForRars(folderSettings).Run()) return false;
            //if (!new CleanupRenameFolders(folderSettings).Run()) return false;
            //if (!new CleanupDeleteSubtitles(folderSettings).Run()) return false;
            //if (!new CleanupDeleteSamples(folderSettings).Run()) return false;
            //if (!new CleanupMoveSubtitles(folderSettings).Run()) return false;
            //if (!new CleanupRenamingFiles(folderSettings).Run()) return false;
            //if (!new CleanupCopyNfoFiles(folderSettings).Run()) return false;
            return true;
        }
    }
}
