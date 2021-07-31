using MovieServerCleaner.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace MovieServerCleaner
{
    public class Settings
    {
        public Settings()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "Settings.json");
            Console.WriteLine(file);
            if (File.Exists(file))
            {
                Console.WriteLine("Loading settings from json");
                var json = File.ReadAllText(file);
                LocalSettings = JsonConvert.DeserializeObject<ApplicationSettings>(json);
            }
            else
            {
                LocalSettings = new ApplicationSettings();
            }
        }

        private static Settings LocalInstance { get; set; }
        public static Settings Instance => LocalInstance ??= new Settings();
        private ApplicationSettings LocalSettings { get; }

        public List<string> AllowedSubtitles => LocalSettings.AllowedSubtitles;
        public List<string> SubDirNames => LocalSettings.SubDirNames;
        public List<FolderSettings> Folders => LocalSettings.Folders;
    
        //public string
        public void WriteSettings()
        {
            //ConsoleEx.WriteLine("Dumping settings", ConsoleColor.DarkYellow);
            //ConsoleEx.WriteLine($"{string.Join(";", AllowedSubtitles)}", ConsoleColor.DarkYellow);
            //ConsoleEx.WriteLine($"{string.Join(";", SubDirNames)}", ConsoleColor.DarkYellow);
        }
    }
}