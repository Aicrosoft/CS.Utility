using System.IO;
using System.Xml.Serialization;
using CS.Serialization;

namespace CS.Storage
{
    /// <summary>
    ///   对象的XML持久化
    /// </summary>
    /// 
    /// <description class = "CS.Storage.XmlStorage">
    ///   
    /// </description>
    /// 
    /// <history>
    ///   2010-7-15 15:15:37 , zhouyu ,  创建	     
    ///  </history>
    public class XmlStorage
    {
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>存在性</returns>
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 对象序列化后以XML格式保存
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">保存路径</param>
        /// <param name="value">对象的引用</param>
        public static void Save<T>(string path, T value)
        {
            using (var writer = new StreamWriter(path))
            {
                (new XmlSerializer(typeof(T))).Serialize(writer, value);
            }
        }

        /// <summary>
        /// 将保存后的XML文件反序列化成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns>对象实例</returns>
        public static T Load<T>(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"{path} not exist.");
            using (var reader = new StreamReader(path))
            {
                return (T)(new XmlSerializer(typeof(T))).Deserialize(reader);
            }
        }

    }
}