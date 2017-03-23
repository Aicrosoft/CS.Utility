using System;
using System.Diagnostics;

namespace CS.Logging
{
    public class Tracer
    {
        static Tracer()
        {
            IsOpen = IsDebug();
            if (IsOpen)
                Level = LogLevel.All;

            Log = LogManager.GetLogger();
        }

        private static readonly ILog Log;

        /// <summary>
        /// 是否启用跟踪
        /// </summary>
        public static bool IsOpen { get; set; }

        /// <summary>
        /// 启用的跟踪级别
        /// </summary>
        public static LogLevel Level { get; set; }

        /// <summary>
        /// 是否记录某一级别的日志
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static bool IsLog(LogLevel level)
        {
            return IsOpen && Level.HasFlag(level);
        }
        [Conditional("DEBUG")]
        public static void Trace(string message)
        {
            Log.Trace(message);
        }
        [Conditional("DEBUG")]
        public static void Trace(string message, Exception ex)
        {
            Log.Trace(message,ex);
        }
        [Conditional("DEBUG")]
        public static void Debug(string message)
        {
            Log.Debug(message);
        }
        [Conditional("DEBUG")]
        public static void Debug(string message, Exception ex)
        {
            Log.Debug(message, ex);
        }

        static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}