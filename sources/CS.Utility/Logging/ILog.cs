using System;

namespace CS.Logging
{

    /// <summary>
    /// 用于记录消息的日志接口
    /// <remarks>
    /// 
    /// </remarks>
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 消息输出可用 <see cref="LogLevel.Trace" />
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// 消息输出可用 <see cref="LogLevel.Debug" />
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// 消息输出可用 <see cref="LogLevel.Info" />
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// 消息输出可用 <see cref="LogLevel.Warn" />
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// 消息输出可用 <see cref="LogLevel.Error" />
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// 消息输出可用 <see cref="LogLevel.Fatal" />
        /// </summary>
        bool IsFatalEnabled { get; }


        /// <summary>
        /// Trace消息输出
        /// </summary>
        /// <param name="message"></param>
        void Trace(object message);

        /// <summary>
        /// Trace消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Trace(object message, Exception exception);

        /// <summary>
        /// Trace ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void TraceFormat(string format, params object[] args);

        //void Debug(string format, object arg0);

        //void Debug(string format, object arg0, object arg1);

        //void DebugF(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Trace ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void TraceFormat(IFormatProvider provider, string format, params object[] args);


        /// <summary>
        /// Debug消息输出
        /// </summary>
        /// <param name="message"></param>
        void Debug(object message);

        /// <summary>
        /// 记录Debug消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Debug(object message, Exception exception);

        /// <summary>
        /// 记录Debug ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void DebugFormat(string format, params object[] args);

        //void Debug(string format, object arg0);

        //void Debug(string format, object arg0, object arg1);

        //void DebugF(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// 记录Debug ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void DebugFormat(IFormatProvider provider, string format, params object[] args);


        /// <summary>
        /// Info消息输出
        /// </summary>
        /// <param name="message"></param>
        void Info(object message);

        /// <summary>
        /// 记录Info消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Info(object message, Exception exception);

        /// <summary>
        /// 记录Info ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void InfoFormat(string format, params object[] args);

        //void Info(string format, object arg0);

        //void Info(string format, object arg0, object arg1);

        //void Info(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// 记录Info ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void InfoFormat(IFormatProvider provider, string format, params object[] args);


        /// <summary>
        /// Warn消息输出
        /// </summary>
        /// <param name="message"></param>
        void Warn(object message);

        /// <summary>
        /// 记录Warn消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Warn(object message, Exception exception);

        /// <summary>
        /// 记录Warn ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void WarnFormat(string format, params object[] args);

        //void Warn(string format, object arg0);

        //void Warn(string format, object arg0, object arg1);

        //void Warn(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// 记录Warn ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void WarnFormat(IFormatProvider provider, string format, params object[] args);


        /// <summary>
        /// Error消息输出
        /// </summary>
        /// <param name="message"></param>
        void Error(object message);

        /// <summary>
        /// 记录Warn消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Error(object message, Exception exception);

        /// <summary>
        /// 记录Error ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void ErrorFormat(string format, params object[] args);

        //void Warn(string format, object arg0);

        //void Warn(string format, object arg0, object arg1);

        //void Warn(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// 记录Warn ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void ErrorFormat(IFormatProvider provider, string format, params object[] args);


        /// <summary>
        /// Fatal消息输出
        /// </summary>
        /// <param name="message"></param>
        void Fatal(object message);

        /// <summary>
        /// 记录Fatal消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Fatal(object message, Exception exception);

        /// <summary>
        /// 记录Fatal ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void FatalFormat(string format, params object[] args);

        //void Fatal(string format, object arg0);

        //void Fatal(string format, object arg0, object arg1);

        //void Fatal(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// 记录Fatal ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void FatalFormat(IFormatProvider provider, string format, params object[] args);
    }

    /// <summary>
    /// 日志级别，对应不同的方法
    /// </summary>
    [Flags]
    public enum LogLevel
    {
        /// <summary>
        /// 关闭输出
        /// </summary>
        Off = 0,
        /// <summary>
        /// 输出全开
        /// </summary>
        All = Trace | Debug | Info | Warn | Error | Fatal,
        /// <summary>
        /// 跟踪
        /// </summary>
        Trace = 1,
        /// <summary>
        /// 调试 对应条件编译Debug
        /// </summary>
        Debug = 2,
        /// <summary>
        /// 消息
        /// </summary>
        Info = 4,
        /// <summary>
        /// 警告
        /// </summary>
        Warn = 8,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 16,
        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal = 32,
    }
}