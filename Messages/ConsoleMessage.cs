using System;

namespace Messages
{
    public static class ConsoleMessage
    {
        private static void Show(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void ShowLog(string msg)
        {
            Show(msg, ConsoleColor.DarkBlue);
        }

        public static void ShowInfo(string msg)
        {
            Show(msg, ConsoleColor.DarkGreen);
        }

        public static void ShowError(string msg)
        {
            Show(msg, ConsoleColor.DarkRed);
        }
    }
}