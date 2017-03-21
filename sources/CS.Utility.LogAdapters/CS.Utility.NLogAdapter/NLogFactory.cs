using System;
using System.IO;
using System.Linq;
using System.Reflection;
using CS.Logging;
using CS.Utils;
using NLog.Config;
using LogManager = NLog.LogManager;

namespace CS.NLogAdapter
{
    public class NLogFactory : CS.Logging.ILogFactory
    {
        public void SetLogConfigFile(string path)
        {
            //LogManager.Configuration = new XmlLoggingConfiguration(path); //该方法不支持Web方式
            LogManager.Configuration = new XmlLoggingConfiguration(AppDomain.CurrentDomain.BaseDirectory + path, true);
        }

        ILog ILogFactory.GetLogger(string name)
        {
            var log = NLog.LogManager.GetLogger(name);

            return new NLogWrapper(log);
        }

        ILog ILogFactory.GetLogger(Type type)
        {
            var log = NLog.LogManager.GetLogger(type.FullName);

            return new NLogWrapper(log);
        }
    }
}