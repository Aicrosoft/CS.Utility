using System.Configuration;
using System.Data.Common;

namespace CS.Configuration
{
    public class ConfigHelper
    {
        /// <summary>
        /// SectionGroup节点名称
        /// </summary>
        public static string SectionGroupName => ConfigurationManager.AppSettings["CS.Utility.SectionGroupName"] ?? "cszi.com";

        /// <summary>
        /// 获取Db配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ConnectionStringSettings GetDbSetting(string key)
        {
            return ConfigurationManager.ConnectionStrings[key];
        }

       
    }
}