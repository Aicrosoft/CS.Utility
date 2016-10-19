using System;

namespace CS.Logging
{


    public class ColorConsoleLogFactory : ILogFactory
    {
        ILog ILogFactory.GetLogger(string name)
        {
            return new ColorConsoleLog(name, Console.Out);
        }

        ILog ILogFactory.GetLogger(Type type)
        {
            return new ColorConsoleLog(type.FullName, Console.Out);
        }
    }
}