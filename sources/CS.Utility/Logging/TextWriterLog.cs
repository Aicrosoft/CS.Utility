using System;
using System.IO;
using System.Threading;

namespace CS.Logging
{

    /// <summary>
    ///     文本输出日志
    /// </summary>
    internal class TextWriterLog : ILog
    {
        private const string PrefixDebug = "DEBUG";
        private const string PrefixInfo = "INFO";
        private const string PrefixWarn = "WARN";
        private const string PrefixError = "ERROR";
        private const string PrefixFatal = "FATAL";
        private readonly string _name;

        private readonly TextWriter _writer;

        public TextWriterLog(string name, TextWriter writer)
        {
            _name = name;
            _writer = writer;
        }

        bool ILog.IsDebugEnabled => true;

        bool ILog.IsInfoEnabled => true;

        bool ILog.IsWarnEnabled => true;

        bool ILog.IsErrorEnabled => true;

        bool ILog.IsFatalEnabled => true;

        void ILog.Debug(object message)
        {
            Dump(PrefixDebug, message);
        }

        void ILog.Debug(object message, Exception exception)
        {
            Dump(PrefixDebug, message + " - " + exception);
        }


        void ILog.DebugFormat(string format, params object[] args)
        {
            Dump(PrefixDebug, format, args);
        }

        void ILog.DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(PrefixDebug, string.Format(provider, format, args));
        }

        void ILog.Info(object message)
        {
            Dump(PrefixInfo, message);
        }

        void ILog.Info(object message, Exception exception)
        {
            Dump(PrefixInfo, message + " - " + exception);
        }


        void ILog.InfoFormat(string format, params object[] args)
        {
            Dump(PrefixInfo, format, args);
        }

        void ILog.InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(PrefixInfo, string.Format(provider, format, args));
        }

        void ILog.Warn(object message)
        {
            Dump(PrefixWarn, message);
        }

        void ILog.Warn(object message, Exception exception)
        {
            Dump(PrefixWarn, message + " - " + exception);
        }


        void ILog.WarnFormat(string format, params object[] args)
        {
            Dump(PrefixWarn, format, args);
        }

        void ILog.WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(PrefixWarn, string.Format(provider, format, args));
        }

        void ILog.Error(object message)
        {
            Dump(PrefixError, message);
        }

        void ILog.Error(object message, Exception exception)
        {
            Dump(PrefixError, message + " - " + exception);
        }


        void ILog.ErrorFormat(string format, params object[] args)
        {
            Dump(PrefixError, format, args);
        }

        void ILog.ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(PrefixError, string.Format(provider, format, args));
        }

        void ILog.Fatal(object message)
        {
            Dump(PrefixFatal, message);
        }

        void ILog.Fatal(object message, Exception exception)
        {
            Dump(PrefixFatal, message + " - " + exception);
        }


        void ILog.FatalFormat(string format, params object[] args)
        {
            Dump(PrefixFatal, format, args);
        }

        void ILog.FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            Dump(PrefixFatal, string.Format(provider, format, args));
        }

        private void Dump(string prefix, string message, params object[] args)
        {
            var line =
                $"{DateTime.Now:yyyy-MM-dd' 'HH:mm:ss:ff} [{prefix}] {Thread.CurrentThread.ManagedThreadId} {_name} - " +
                string.Format(message, args);

            DumpWrite(line);
        }

        private void Dump(string prefix, object message)
        {
            var line =
                $"{DateTime.Now:yyyy-MM-dd' 'HH:mm:ss:ff} [{prefix}] {Thread.CurrentThread.ManagedThreadId} {_name} - {message}";

            DumpWrite(line);
        }

        private void DumpWrite(string msg)
        {
            lock (_writer)
            {
                _writer.WriteLine(msg);
                _writer.Flush();
            }
        }
    }
}