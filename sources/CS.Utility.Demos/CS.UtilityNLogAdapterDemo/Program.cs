using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Config;

namespace CS.UtilityNLogAdapterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //LogManager.Configuration = new XmlLoggingConfiguration("config/NLog.config");
            CS.Logging.LogManager.SetLogConfigFile("config/NLog.config");


            var log1 = CS.Logging.LogManager.GetLogger(typeof (Program));
            var log2 = CS.Logging.LogManager.GetLogger();

            log1.Debug("Debug info");
            log1.Info("info info");
            log1.Warn("warn info");
            log1.Error("error info",new IndexOutOfRangeException("测试而已"));
            log1.Fatal("fatal info");
            log1.Trace("trace info");

            log2.Debug("Debug info");
            log2.Info("info info");
            log2.Warn("warn info");
            log2.Error("error info", new IndexOutOfRangeException("测试而已"));
            log2.Fatal("fatal info");
            log2.Trace("trace info");



            //Console.WriteLine(@"press any key to close.");
            Console.ReadKey();
        }
    }
}
