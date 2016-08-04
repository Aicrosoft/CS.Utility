using System.Configuration;

namespace CS.Configuration
{
    public class ConfigHelper
    {
        /// <summary>
        /// SectionGroup节点名称
        /// </summary>
        public static string SectionGroupName => ConfigurationManager.AppSettings["CS.Utility.SectionGroupName"] ?? "cszi.com";
    }
}