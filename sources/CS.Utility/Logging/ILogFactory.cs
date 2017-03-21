using System;

namespace CS.Logging
{
    /// <summary>
    /// 实现了<see cref="ILog"/>接口的日志工厂接口
    /// </summary>
    public interface ILogFactory
    {

        /// <summary>
        /// 设定配置文件路径
        /// </summary>
        /// <param name="path"></param>
        void SetLogConfigFile(string path);

        /// <summary>
        /// 获取直接给出名称的日志
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ILog GetLogger(string name);


        /// <summary>
        /// 获取按类型的日志
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ILog GetLogger(Type type);
    }
}