using System.Collections.Specialized;
using System.Configuration;

namespace CS.Configuration
{
    /// <summary>
    /// 应用基础设定
    ///<code>
    /// <!-- 在app.config文件中配置如下 -->
    /// <configuration>
    /// <configSections>
    ///     <!-- 平台相关配置 -->
    ///     <section name="Platform" type="System.Configuration.NameValueSectionHandler" />
    /// </configSections>
    /// <!-- 平台相关配置 -->
    /// <Platform>
    ///  <add key="PlatformId" value="1" />
    ///  <add key="PlatformKey" value="IOS" />
    /// </Platform>
    /// 
    /// </configuration>
    /// </code>
    /// </summary>
    public abstract class SectionBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        protected SectionBase(string sectionName)
        {
            KeyValues = ConfigurationManager.GetSection(sectionName) as NameValueCollection;
            if (KeyValues == null)
                throw new ConfigurationErrorsException($"read setction={sectionName} from config is exception.");

            //interpolation:参数模板？
            //throw new ConfigurationErrorsException(string.Format("read setction={0} from config is exception.", sectionName));
        }

        /// <summary>
        /// 配置的Key，Value键值集合，通过Name索引访问
        /// </summary>
        public NameValueCollection KeyValues { get; private set; }
        

    }


}