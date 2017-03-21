using System;
using System.Configuration;
using System.IO;

namespace CS.Logging
{
    public class DiagnosticsLogFactory : ILogFactory
    {
        private readonly TextWriter _writer;

        public DiagnosticsLogFactory() : this(ConfigurationManager.AppSettings["CS.Diangnostics.LogPath"])
        {
        }

        public DiagnosticsLogFactory(string logPath)
        {
            if (string.IsNullOrWhiteSpace(logPath))
                throw new ArgumentNullException(
                    "请在配置文件中的configuration/appSettings:<add key=\"CS.Diangnostics.LogPath\" value=\"记录日志的文件路径\" /> 或手动构造一个有效路径。");

            _writer = new StreamWriter(logPath, true);
        }


        public void SetLogConfigFile(string path)
        {
            //throw new NotImplementedException();
        }

        public ILog GetLogger(string name)
        {
            return new TextWriterLog(name, _writer);
        }

        public ILog GetLogger(Type type)
        {
            return new TextWriterLog(type.FullName, _writer);
        }
    }

}