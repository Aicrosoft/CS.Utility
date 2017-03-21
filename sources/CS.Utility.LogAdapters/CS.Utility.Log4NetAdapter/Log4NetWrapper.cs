using System;
using CS.Logging;

namespace CS.Log4NetAdapter
{
    public class Log4NetWrapper : CS.Logging.ILog
    {
        private readonly log4net.ILog _log;

        public Log4NetWrapper(log4net.ILog log)
        {
            _log = log;
        }


        #region [ ILog                         ]

        bool ILog.IsTraceEnabled => false; //Log4Net没有Trace级别

        bool ILog.IsDebugEnabled => _log.IsDebugEnabled;

        bool ILog.IsInfoEnabled => _log.IsInfoEnabled;

        bool ILog.IsWarnEnabled => _log.IsWarnEnabled;

        bool ILog.IsErrorEnabled => _log.IsErrorEnabled;

        bool ILog.IsFatalEnabled => _log.IsFatalEnabled;


        void ILog.Trace(object message)
        {
           
        }

        void ILog.Trace(object message, Exception exception)
        {
           
        }

        void ILog.TraceFormat(string format, params object[] args)
        {
           
        }

        void ILog.TraceFormat(IFormatProvider provider, string format, params object[] args)
        {
            
        }



        void ILog.Debug(object message)
        {
            _log.Debug(message);
        }

        void ILog.Debug(object message, Exception exception)
        {
            _log.Debug(message, exception);
        }

        void ILog.DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        void ILog.DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.DebugFormat(provider, format, args);
        }

        void ILog.Info(object message)
        {
            _log.Info(message);
        }

        void ILog.Info(object message, Exception exception)
        {
            _log.Info(message, exception);
        }

       

        void ILog.InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        void ILog.InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.InfoFormat(provider, format, args);
        }

        void ILog.Warn(object message)
        {
            _log.Warn(message);
        }

        void ILog.Warn(object message, Exception exception)
        {
            _log.Warn(message, exception);
        }

       

        void ILog.WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        void ILog.WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.WarnFormat(provider, format, args);
        }

        void ILog.Error(object message)
        {
            _log.Error(message);
        }

        void ILog.Error(object message, Exception exception)
        {
            _log.Error(message, exception);
        }

      

        void ILog.ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        void ILog.ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.ErrorFormat(provider, format, args);
        }

        void ILog.Fatal(object message)
        {
            _log.Fatal(message);
        }

        void ILog.Fatal(object message, Exception exception)
        {
            _log.Fatal(message, exception);
        }
       

        void ILog.FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }

        void ILog.FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.FatalFormat(provider, format, args);
        }

        #endregion


    }

}