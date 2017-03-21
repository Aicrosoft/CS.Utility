using System;
using CS.Logging;

namespace CS.Log4NetAdapter
{
    /// <summary>
	/// log4net 的日志工厂
	/// </summary>
	public class Log4NetFactory : ILogFactory
    {
        public void SetLogConfigFile(string path)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
        }

        ILog ILogFactory.GetLogger(string name)
        {
            return new Log4NetWrapper(log4net.LogManager.GetLogger(name));
        }

        ILog ILogFactory.GetLogger(Type type)
        {
            return new Log4NetWrapper(log4net.LogManager.GetLogger(type));
        }
    }
}