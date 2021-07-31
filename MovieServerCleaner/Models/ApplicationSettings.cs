using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieServerCleaner.Models
{
    public class ApplicationSettings
    {
        public List<string> AllowedSubtitles { get; set; } 
        public List<string> SubDirNames { get; set; }
        public List<FolderSettings> Folders { get; set; }
    }
}
