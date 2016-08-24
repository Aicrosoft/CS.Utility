using System.IO;
using System.Text;

namespace CS.Storage
{
    /// <summary>
    ///   字符串保存为文本内容的文件
    /// </summary>
    /// 
    /// <description class = "CS.Storage.JsonStorage">
    ///   
    /// </description>
    /// 
    /// <history>
    ///   2010-7-15 15:14:01 , zhouyu ,  创建	     
    ///  </history>
    public class StringStorage
    {
        /// <summary>
        /// 保存字符串
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="value">字符串</param>
        /// <param name="encoding"></param>
        public static void Save(string path, string value,Encoding encoding)
        {
            File.WriteAllText(path, value,encoding);
        }

        /// <summary>
        /// 保存字符串
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="value">字符串</param>
        public static void Save(string path, string value)
        {
            File.WriteAllText(path, value);
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="encoding"></param>
        /// <returns>对象实例</returns>
        public static string Load(string path,Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }

    }
}