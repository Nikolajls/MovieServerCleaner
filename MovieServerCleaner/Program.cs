using System;
using MovieServerCleaner.Cleanups;

namespace MovieServerCleaner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Settings.GetInstance().WriteSettings();

            if (!new CleanupCheckForRars().Run()) return;
            if (!new CleanupRenameFolders().Run()) return;
            if (!new CleanupDeleteSubtitles().Run()) return;
            if (!new CleanupDeleteSamples().Run()) return;
            if (!new CleanupMoveSubtitles().Run()) return;
            if (!new CleanupRenamingFiles().Run()) return;
            if (!new CleanupCopyNfoFiles().Run()) return;
            ConsoleEx.WriteLine("Program finished cleanup - idling now...", ConsoleColor.Red);
            Console.ReadLine();
        }
    }
}