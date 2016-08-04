using System;
using CS.Logging;

namespace CS.NLogAdapter
{
    public class NLogFactory : CS.Logging.ILogFactory
    {
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