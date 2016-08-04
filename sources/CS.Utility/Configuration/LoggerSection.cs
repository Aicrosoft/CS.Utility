using System;
using System.ComponentModel;
using System.Configuration;
using CS.Logging;

namespace CS.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggerSection : ConfigurationSection
    {
        /// <summary>
        /// sectionGroup中对应的name = log 的处理器
        /// <remarks>
        /// 通过factory的值找到具体的日志工厂
        /// </remarks>
        /// </summary>
        [ConfigurationProperty("factory", IsRequired = true)]
        [InterfaceValidator(typeof(ILogFactory)), TypeConverter(typeof(TypeNameConverter))]
        public Type LogFactory
        {
            get { return (Type)base["factory"]; }
            set { base["factory"] = value; }
        }
    }
}