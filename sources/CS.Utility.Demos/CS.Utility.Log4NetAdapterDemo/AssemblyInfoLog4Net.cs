
//必须要配合Global的代码才可以正常使用 log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(@"config\log4net.config"));

using EE.ServiceNests.Configuration;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = Consts.Log4NetConfigFile, Watch = true)]



