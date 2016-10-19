using System;
using System.Diagnostics;
using CS.Logging;

namespace CS.Utils
{
    /// <summary>
    /// 只有调试状态才会输出信息的控制台输出
    /// </summary>
    public class DebugConsole
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (DebugConsole));
        /// <summary>
        /// 红，背白
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            log.Fatal(msg);
        }

        /// <summary>
        /// 红
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            log.Error(msg);
        }

        /// <summary>
        /// 黄
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            log.Warn(msg);
        }

        /// <summary>
        /// 仅在DEBUG模式输出 灰
        /// </summary>
        /// <param name="msg"></param>
        [Conditional("DEBUG")]
        public static void Debug(string msg)
        {
            log.Debug(msg);
        }

        /// <summary>
        /// 绿
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            log.Info(msg);
        }

        

        
    }
}