using System;
using System.IO;
using System.Threading;

namespace CS.Logging
{

    /// <summary>
    ///     彩色控制台输出日志
    /// </summary>
    internal class ColorConsoleLog : ILog
    {
        private readonly string _name;

        private readonly TextWriter _writer;

        public ColorConsoleLog(string name, TextWriter writer)
        {
            _name = name;
            _writer = writer;
        }

        bool ILog.IsTraceEnabled => true;

        bool ILog.IsDebugEnabled => true;

        bool ILog.IsInfoEnabled => true;

        bool ILog.IsWarnEnabled => true;

        bool ILog.IsErrorEnabled => true;

        bool ILog.IsFatalEnabled => true;


        void ILog.Trace(object message)
        {
            Dump(LogLevel.Trace, message);
        }

        void ILog.Trace(object message, Exception exception)
        {
            Dump(LogLevel.Trace, message + " - " + exception);
        }


        void ILog.TraceFormat(string format, params object[] args)
        {
            Dump(LogLevel.Trace, format, args);
        }

        void ILog.TraceFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(LogLevel.Trace, string.Format(provider, format, args));
        }

        void ILog.Debug(object message)
        {
            Dump(LogLevel.Debug, message);
        }

        void ILog.Debug(object message, Exception exception)
        {
            Dump(LogLevel.Debug, message + " - " + exception);
        }


        void ILog.DebugFormat(string format, params object[] args)
        {
            Dump(LogLevel.Debug, format, args);
        }

        void ILog.DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(LogLevel.Debug, string.Format(provider, format, args));
        }

        void ILog.Info(object message)
        {
            Dump(LogLevel.Info, message);
        }

        void ILog.Info(object message, Exception exception)
        {
            Dump(LogLevel.Info, message + " - " + exception);
        }


        void ILog.InfoFormat(string format, params object[] args)
        {
            Dump(LogLevel.Info, format, args);
        }

        void ILog.InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(LogLevel.Info, string.Format(provider, format, args));
        }

        void ILog.Warn(object message)
        {
            Dump(LogLevel.Warn, message);
        }

        void ILog.Warn(object message, Exception exception)
        {
            Dump(LogLevel.Warn, message + " - " + exception);
        }


        void ILog.WarnFormat(string format, params object[] args)
        {
            Dump(LogLevel.Warn, format, args);
        }

        void ILog.WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(LogLevel.Warn, string.Format(provider, format, args));
        }

        void ILog.Error(object message)
        {
            Dump(LogLevel.Error, message);
        }

        void ILog.Error(object message, Exception exception)
        {
            Dump(LogLevel.Error, message + " - " + exception);
        }


        void ILog.ErrorFormat(string format, params object[] args)
        {
            Dump(LogLevel.Error, format, args);
        }

        void ILog.ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(LogLevel.Error, string.Format(provider, format, args));
        }

        void ILog.Fatal(object message)
        {
            Dump(LogLevel.Fatal, message);
        }

        void ILog.Fatal(object message, Exception exception)
        {
            Dump(LogLevel.Fatal, message + " - " + exception);
        }


        void ILog.FatalFormat(string format, params object[] args)
        {
            Dump(LogLevel.Fatal, format, args);
        }

        void ILog.FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(LogLevel.Fatal, string.Format(provider, format, args));
        }

        private void Dump(LogLevel prefix, string message, params object[] args)
        {
            var line =
                $"{DateTime.Now:HH:mm:ss:ffffff} [{prefix}] {Thread.CurrentThread.ManagedThreadId} {_name} : " +
                string.Format(message, args);

            WriteLine(prefix, line);
        }

        private void Dump(LogLevel prefix, object message)
        {
            var line =
                $"{DateTime.Now:HH:mm:ss:ffffff} [{prefix}] {Thread.CurrentThread.ManagedThreadId} {_name} : {message}";

            WriteLine(prefix, line);
        }

        private void WriteLine(LogLevel level, string msg)
        {
            lock (_writer)
            {
                if (!Tracer.IsLog(level)) return;
                var forColor = Console.ForegroundColor;
                var bakColor = Console.BackgroundColor;
                switch (level)
                {
                    case LogLevel.Debug:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case LogLevel.Info:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case LogLevel.Warn:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case LogLevel.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case LogLevel.Fatal:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.White;
                        break;
                }
                _writer.WriteLine(msg);
                _writer.Flush();
                Console.ForegroundColor = forColor;
                Console.BackgroundColor = bakColor;
            }
        }


        //[AttributeUsage(AttributeTargets.Field, Inherited = false)]
        //public class ColorConsoleAttribute : System.Attribute
        //{

        //    public ColorConsoleAttribute(ConsoleColor foregroundColor)
        //    {
        //        ForegroundColor = foregroundColor;
        //    }

        //    /// <summary>
        //    /// 前景色
        //    /// </summary>
        //    public ConsoleColor ForegroundColor { get; set; }

        //    /// <summary>
        //    /// 背景色
        //    /// </summary>
        //    public ConsoleColor BackgroundColor { get; set; }
        //}


    }
}