using System;

namespace MovieServerCleaner
{
    public static class ConsoleEx
    {
        public static void WriteLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(DateTime.Now.ToLongTimeString() + "=" + message);
            Console.ResetColor();
        }
    }
}