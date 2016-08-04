using System;
using CS.Logging;

namespace CS.NLogAdapter
{
    internal class NLogWrapper : CS.Logging.ILog
    {
        private readonly NLog.Logger _log;

        public NLogWrapper(NLog.Logger log)
        {
            _log = log;
        }

        #region [ ILog                         ]


        bool ILog.IsDebugEnabled => _log.IsDebugEnabled;

        bool ILog.IsInfoEnabled => _log.IsDebugEnabled;

        bool ILog.IsWarnEnabled => _log.IsWarnEnabled;

        bool ILog.IsErrorEnabled => _log.IsErrorEnabled;

        bool ILog.IsFatalEnabled => _log.IsFatalEnabled;

        void ILog.Debug(object message)
        {
            _log.Debug(message);
        }

        void ILog.Debug(object message, Exception exception)
        {
            //_log.DebugException((message ?? string.Empty).ToString(), exception);
            _log.Debug(exception, message?.ToString());
        }


        void ILog.DebugFormat(string format, params object[] args)
        {
            _log.Debug(format, args);
        }

        void ILog.DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.Debug(provider, format, args);
        }

        void ILog.Info(object message)
        {
            _log.Info(message);
        }

        void ILog.Info(object message, Exception exception)
        {
            //_log.InfoException((message ?? String.Empty).ToString(), exception);
            _log.Info(exception, message?.ToString());
        }


        void ILog.InfoFormat(string format, params object[] args)
        {
            _log.Info(format, args);
        }

        void ILog.InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.Info(provider, format, args);
        }

        void ILog.Warn(object message)
        {
            _log.Warn(message);
        }

        void ILog.Warn(object message, Exception exception)
        {
            //_log.WarnException((message ?? String.Empty).ToString(), exception);
            _log.Warn(exception, message?.ToString());
        }



        void ILog.WarnFormat(string format, params object[] args)
        {
            _log.Warn(format, args);
        }

        void ILog.WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.Warn(provider, format, args);
        }

        void ILog.Error(object message)
        {
            _log.Error(message);
        }

        void ILog.Error(object message, Exception exception)
        {
            //_log.ErrorException((message ?? String.Empty).ToString(), exception);
            _log.Error(exception, message?.ToString());
        }


        void ILog.ErrorFormat(string format, params object[] args)
        {
            _log.Error(format, args);
        }

        void ILog.ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.Error(provider, format, args);
        }

        void ILog.Fatal(object message)
        {
            _log.Fatal(message);
        }

        void ILog.Fatal(object message, Exception exception)
        {
            //_log.FatalException((message ?? String.Empty).ToString(), exception);
            _log.Fatal(exception, message?.ToString());
        }

        void ILog.FatalFormat(string format, params object[] args)
        {
            _log.Fatal(format, args);
        }

        void ILog.FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.Fatal(provider, format, args);
        }

        #endregion
    }
}