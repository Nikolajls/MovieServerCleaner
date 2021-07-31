using System;
using MovieServerCleaner.Cleanups;

namespace MovieServerCleaner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Settings.Instance.WriteSettings();
           
            var running = true;
            while (running)
            {
                Console.Clear();
                for (var i = 0; i < Settings.Instance.Folders.Count; i++)
                {
                    var folder = Settings.Instance.Folders[i];
                    Console.WriteLine($"{i + 1}\t - {folder.Title}");
                }

                Console.Write("Input folder index or type exit to stop\n>");
                var input = Console.ReadLine();
                input = string.IsNullOrEmpty(input) ? string.Empty : input;
                input = input.ToLower();
                if (input == "exit")
                {
                    Console.WriteLine("Exiting");
                    running = false;
                }
                else if (int.TryParse(input, out var selectedIndex) && selectedIndex <= Settings.Instance.Folders.Count)
                {
                    Console.Clear();
                    var folder = Settings.Instance.Folders[selectedIndex - 1];
                    Console.WriteLine($"You chosed {selectedIndex} with title:{folder.Title}");
                    new CleanupStart().Cleanup(folder);
                    Console.WriteLine("Ran cleanup, press enter to return to menu");
                    Console.ReadLine();
                }


            }
        }
    }
}