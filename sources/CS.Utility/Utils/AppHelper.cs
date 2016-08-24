using System;
using System.IO;
using static System.String;

namespace CS.Utils
{
    /// <summary>
    /// 系统相关变量与信息
    /// </summary>
    public class AppHelper
    {
        /// <summary>
        /// 当前应用程序的基础路径
        /// </summary>
        public static string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 返回可用的路径信息
        /// </summary>
        /// <param name="relativePath">相对于运行目录的相对路径</param>
        /// <returns>指定路径不存在时返回null</returns>
        public static DirectoryInfo GetDirectory(string relativePath)
        {
            var path = FileHelper.CombinePath(relativePath);
            var di = new DirectoryInfo(path);
            return di.Exists ? di : null;
        }

        /// <summary>
        /// 获取文件的全路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileFullName(string fileName)
        {
           return Path.Combine(BaseDirectory, fileName);
        }

        ///// <summary>
        ///// 返回本地格式的 文件的绝对路径
        ///// </summary>
        ///// <param name="paths"></param>
        ///// <returns></returns>
        //public static string GetFilePath(params string[] paths)
        //{
        //    return $"{CombinePath(paths)}";
        //}
    }
}