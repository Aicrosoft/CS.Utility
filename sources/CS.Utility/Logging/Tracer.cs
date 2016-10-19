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
        }

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