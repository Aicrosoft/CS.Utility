using System;
using System.Configuration;
using CS.Configuration;
using CS.Reflection;

namespace CS.Logging
{

    /// <summary>
	/// 创建一个基于配置的日志记录器
	/// </summary>
	/// <example>
	/// 
	/// 配置实例:
	/// 
	/// <configuration>
	///		<configSections>
	///			<sectionGroup name="sectionGroupName">
	///				<section name="log" type="CS.Configuration.LoggerSection, CS.Utility" />
	///			</sectionGroup>
	///		</configSections>
	///		<xx3700.com>
	///			<log factory="CS.Logger.Log4NetAdapter.Log4NetLoggerFactory, CS.Logger.Log4NetAdapter" />
	///		</xx3700.com>
	/// </configuration>
	/// 
	/// 代码实现:
	/// 
	///		LogManager.AssignFactory(new Log4NetLoggerFactory());
	/// 
	/// </example>
    public static class LogManager
    {
        private static ILogFactory _factory;

        static LogManager()
        {
            var section = ConfigurationManager.GetSection($"{ConfigHelper.SectionGroupName}/log") as LoggerSection;
            ILogFactory f = null;

            if (section?.LogFactory != null)
            {
                f = FastActivator.Create(section.LogFactory) as ILogFactory;
            }
#if DEBUG
            // use an empty logger if nothing is specified in the app.config
            //LogManager._factory = f ?? (ILogFactory)new NullLoggerFactory();
            LogManager._factory = f ?? (ILogFactory)new ColorConsoleLogFactory();
#else
			// use the log4net logger logger if nothing is specified in the app.config
			//LogManager._factory = f ?? (ILogFactory)new Log4NetLogFactory();
            LogManager._factory = f ?? (ILogFactory)new NullLoggerFactory();
#endif
        }

        public static void SetLogConfigFile(string path)
        {
            if (_factory == null) throw new InvalidOperationException("LogFactory is not a instance.");
            _factory.SetLogConfigFile(path);
        }

        /// <summary>
        /// 手动赋值日志工厂
        /// </summary>
        /// <param name="factory"></param>
        public static void AssignFactory(ILogFactory factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            LogManager._factory = factory;
        }

        /// <summary>
        /// 返回基于某个类型的日志
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILog GetLogger(Type type)
        {
            return _factory.GetLogger(type);
        }

        /// <summary>
        /// 返回基于名称的日志，默认名称App
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILog GetLogger(string name = "App")
        {
            return _factory.GetLogger(name);
        }
    }
}