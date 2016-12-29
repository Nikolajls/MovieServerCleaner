using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MovieServerCleaner
{
    public class Settings
    {
        public Settings()
        {
            var temporayConfigRead = ConfigurationManager.AppSettings["AllowedSubtitles"];
            if (temporayConfigRead != null) AllowedSubtitles = temporayConfigRead.Split('|').ToList();

            temporayConfigRead = ConfigurationManager.AppSettings["SubDirNames"];
            if (temporayConfigRead != null) SubDirNames = temporayConfigRead.Split('|').ToList();

            temporayConfigRead = ConfigurationManager.AppSettings["PathWorkingFolderMovie"];
            if (temporayConfigRead != null) PathMovieFlyttes = temporayConfigRead;

            temporayConfigRead = ConfigurationManager.AppSettings["PathNfo"];
            if (temporayConfigRead != null) PathNfoFolder = temporayConfigRead;

            temporayConfigRead = ConfigurationManager.AppSettings["PathFlyttes"];
            if (temporayConfigRead != null) PathWorkingFolder = temporayConfigRead;
        }

        private static Settings LocalInstance { get; set; }

        public List<string> AllowedSubtitles { get; } = new List<string> {"DA.SRT", "DK.SRT"};
        public List<string> SubDirNames { get; } = new List<string> {"sub", "subs", "subtitles", "undertekster"};
        public string PathMovieFlyttes { get; } = @"\\server\download\Flyttes\Film\";
        public string PathNfoFolder { get; } = @"\\server\download\Flyttes\Extra\NFOs\";
        public string PathWorkingFolder { get; } = @"\\server\download\Flyttes\";

        public static Settings GetInstance()
        {
            LocalInstance = LocalInstance ?? new Settings();
            return LocalInstance;
        }

        public void WriteSettings()
        {
            ConsoleEx.WriteLine("Dumping settings", ConsoleColor.DarkYellow);
            ConsoleEx.WriteLine($"{string.Join(";", AllowedSubtitles)}", ConsoleColor.DarkYellow);
            ConsoleEx.WriteLine($"{string.Join(";", SubDirNames)}", ConsoleColor.DarkYellow);
            ConsoleEx.WriteLine($"{PathMovieFlyttes}", ConsoleColor.DarkYellow);
            ConsoleEx.WriteLine($"{PathNfoFolder}", ConsoleColor.DarkYellow);
            ConsoleEx.WriteLine($"{PathWorkingFolder}", ConsoleColor.DarkYellow);
        }
    }
}