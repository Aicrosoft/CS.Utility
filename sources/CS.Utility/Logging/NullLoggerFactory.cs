using System;

namespace CS.Logging
{
    /// <summary>
    ///     当无日志输出时的替代默认空日志
    /// </summary>
    public class NullLoggerFactory : ILogFactory
    {
        ILog ILogFactory.GetLogger(string name)
        {
            return NullLogger.Instance;
        }

        ILog ILogFactory.GetLogger(Type type)
        {
            return NullLogger.Instance;
        }

        #region [ NullLogger                   ]

        private class NullLogger : ILog
        {
            internal static readonly ILog Instance = new NullLogger();

            private NullLogger()
            {
            }

            #region [ ILog                         ]

            bool ILog.IsDebugEnabled => false;

            bool ILog.IsInfoEnabled => false;

            bool ILog.IsWarnEnabled => false;

            bool ILog.IsErrorEnabled => false;

            bool ILog.IsFatalEnabled => false;

            void ILog.Debug(object message)
            {
            }

            void ILog.Debug(object message, Exception exception)
            {
            }

            void ILog.DebugFormat(string format, params object[] args)
            {
            }

            void ILog.DebugFormat(IFormatProvider provider, string format, params object[] args)
            {
            }

            void ILog.Info(object message)
            {
            }

            void ILog.Info(object message, Exception exception)
            {
            }

            void ILog.InfoFormat(string format, params object[] args)
            {
            }

            void ILog.InfoFormat(IFormatProvider provider, string format, params object[] args)
            {
            }

            void ILog.Warn(object message)
            {
            }

            void ILog.Warn(object message, Exception exception)
            {
            }

            void ILog.WarnFormat(string format, params object[] args)
            {
            }

            void ILog.WarnFormat(IFormatProvider provider, string format, params object[] args)
            {
            }

            void ILog.Error(object message)
            {
            }

            void ILog.Error(object message, Exception exception)
            {
            }

            void ILog.ErrorFormat(string format, params object[] args)
            {
            }

            void ILog.ErrorFormat(IFormatProvider provider, string format, params object[] args)
            {
            }

            void ILog.Fatal(object message)
            {
            }

            void ILog.Fatal(object message, Exception exception)
            {
            }

            void ILog.FatalFormat(string format, params object[] args)
            {
            }

            void ILog.FatalFormat(IFormatProvider provider, string format, params object[] args)
            {
            }

            #endregion

        }

        #endregion
    }
}