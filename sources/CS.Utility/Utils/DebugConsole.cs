using System;
using System.Diagnostics;

namespace CS.Utils
{
    /// <summary>
    /// 只有调试状态才会输出信息的控制台输出
    /// </summary>
    public class DebugConsole
    {
        /// <summary>
        /// 红，背白
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            WriteLine(msg,ConsoleColor.Red,ConsoleColor.White);
        }

        /// <summary>
        /// 红
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            WriteLine(msg,ConsoleColor.Red,Console.BackgroundColor);
        }

        /// <summary>
        /// 黄
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            WriteLine(msg, ConsoleColor.Yellow, Console.BackgroundColor);
        }

        /// <summary>
        /// 仅在DEBUG模式输出 灰
        /// </summary>
        /// <param name="msg"></param>
        [Conditional("DEBUG")]
        public static void Debug(string msg)
        {
            WriteLine(msg, ConsoleColor.DarkGray, Console.BackgroundColor);
        }

        /// <summary>
        /// 绿
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            WriteLine(msg, ConsoleColor.Green, Console.BackgroundColor);
        }

        public static void WriteLine(string msg, ConsoleColor foreColor, ConsoleColor backColor)
        {
            var currentForeColor = Console.ForegroundColor;
            var currentBackColor = Console.BackgroundColor;
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Console.WriteLine(msg);
            Console.ForegroundColor = currentForeColor;
            Console.BackgroundColor = currentBackColor;
        }

        
    }
}