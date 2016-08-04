using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS.Logging;

namespace CS.Utility.Log4NetAdapterDemo
{
    class Program
    {
        static Program() 
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Consts.Log4NetConfigFile));
            //注意，还要有AssemblyInfoLog4Net.cs里的相关代码
        }

        static void Main(string[] args)
        {
            ILog _sysLog = LogManager.GetLogger(typeof(Program));
            ILog _appLog = LogManager.GetLogger();


            _sysLog.Debug($"{DateTime.Now}");

            _appLog.Debug($"App日志输出，{DateTime.Now}");

        }
    }
}
