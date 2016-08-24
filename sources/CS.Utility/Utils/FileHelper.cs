using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.String;

namespace CS.Utils
{
    /// <summary>
    /// 文件辅助类
    /// </summary>
    public class FileHelper
    {



        /// <summary>
        /// 将多个文件拷贝至目标目录
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <param name="destDirectory"></param>
        /// <param name="overwrite"></param>
        public static void Copy(string[] sourceFiles, string destDirectory, bool overwrite = false)
        {
            foreach (var file in sourceFiles)
            {
                var destFile = $"{destDirectory}{Path.GetFileName(file)}";
                //Console.WriteLine(destFile);
                if (!overwrite && File.Exists(destFile))
                {
                    Console.WriteLine($"{destFile}已经存在，跳过。");
                    return;
                }
                GetFullPathCreateIfNeed(destFile);
                File.Copy(file,destFile,overwrite);
            }
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destFile"></param>
        /// <param name="overwrite"></param>
        public static void Copy(string sourceFile, string destFile, bool overwrite = false)
        {
            if (!overwrite && File.Exists(destFile))
            {
                Console.WriteLine($"{destFile}已经存在，跳过。");
                return;
            }
            GetFullPathCreateIfNeed(destFile);
            File.Copy(sourceFile, destFile,overwrite);
        }

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetExtension(string uri) => Path.GetExtension(uri);

        /// <summary>
        /// 返回文件或目录的全路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFullPath(string fileName)
        {
            return CombinePath(fileName);
        }

        /// <summary>
        /// 保存文件，不存在路径自动创建，默认UTF8编码
        /// </summary>
        /// <param name="file"></param>
        /// <param name="content"></param>
        /// <param name="overwrite">是否覆盖原文件，默认不覆写</param>
        /// <param name="encoding"></param>
        public static void Save(string file, string content,bool overwrite = false, Encoding encoding = null)
        {
            var filePath = GetFullPathCreateIfNeed(file);
            if (File.Exists(filePath) && !overwrite)
            {
                Console.WriteLine($"{filePath}已经存在，跳过。");
                return;
            }
            File.WriteAllText(filePath, content, encoding ?? Encoding.UTF8);
        }

        /// <summary>
        /// 返回全路径，如果不存在目录则创建
        /// </summary>
        /// <param name="relativeFileName">相对路径的文件名：~/xx/xx/xx.png</param>
        /// <returns></returns>
        static string GetFullPathCreateIfNeed(string relativeFileName)
        {
            var fullFileName = GetFullPath(relativeFileName);
            var dicName = Path.GetDirectoryName(fullFileName);
            if (dicName != null && !Directory.Exists(dicName))
                Directory.CreateDirectory(dicName);
            return fullFileName;
        }

        /// <summary>
        /// 返回相对路径
        /// </summary>
        /// <param name="fileName">非全路径的文件名，如：xxx\xxx\001.gif</param>
        /// <returns></returns>
        public static string GetRelativePath(string fileName)
        {
            var fullFile = GetFullPath(fileName);
            var basePath = AppHelper.BaseDirectory;
            return fullFile.Replace(basePath, "");
        }

        /// <summary>
        /// 返回Web中Image使用的相对路径，结果如: /attach/demo/1.png
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetWebRelativePath(string fileName)
        {
            var realName = GetRelativePath(fileName);
            return $"\\{realName.Replace("/", "\\")}";
        }

        /// <summary>
        /// 保存文件，不存在路径时自动创建
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public static void Save(string fileName, byte[] data)
        {
            var fileInfo = new FileInfo(fileName);
            var directory = fileInfo.Directory;
            if (directory == null) return;
            if (!directory.Exists)
            {
                directory.Create();
            }
            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
        }

        /// <summary>
        /// 删除所有文件
        /// </summary>
        /// <param name="fileNames"></param>
        public static void Delete(string[] fileNames)
        {
            foreach (var name in fileNames)
            {
                File.Delete(name);
            }
        }

        /// <summary>
        /// Path 拼接
        /// </summary>
        /// <param name="paths">需要拼接的 Path 参数</param>
        /// <returns>拼接后的结果</returns>
        public static string CombinePath(params string[] paths)
        {
            var path = Join(Path.DirectorySeparatorChar + "", paths);
            path = path.Replace('/', Path.DirectorySeparatorChar);
            if (path.StartsWith("~" + Path.DirectorySeparatorChar) || path.StartsWith(Path.DirectorySeparatorChar + ""))
                path = AppHelper.BaseDirectory + path.TrimStart('~', Path.DirectorySeparatorChar);
            return path;
        }
        ///// <summary>
        ///// 返回扩展名
        ///// </summary>
        ///// <param name="resourceName"></param>
        ///// <returns></returns>
        //public static string GetExtName(string resourceName)
        //{
        //    //var index = resourceName.LastIndexOf(".", StringComparison.Ordinal);
        //    //var ext = resourceName.Substring(index + 1, resourceName.Length - index - 1);
        //    //return ext;

        //    //string filePath = @"E:\Randy0528\中文目录\JustTest.rar";
        //    //Response.Write("文件路径：" + filePath);
        //    //Response.Write("更改路径字符串的扩展名。");
        //    //Response.Write(System.IO.Path.ChangeExtension(filePath, "txt"));
        //    //Response.Write("返回指定路径字符串的目录信息。。");
        //    //Response.Write(System.IO.Path.GetDirectoryName(filePath));
        //    //Response.Write("返回指定的路径字符串的扩展名。");
        //    //Response.Write(System.IO.Path.GetExtension(filePath));
        //    //Response.Write("返回指定路径字符串的文件名和扩展名。");
        //    //Response.Write(System.IO.Path.GetFileName(filePath));
        //    //Response.Write("返回不具有扩展名的指定路径字符串的文件名。");
        //    //Response.Write(System.IO.Path.GetFileNameWithoutExtension(filePath));
        //    //Response.Write("获取指定路径的根目录信息。");
        //    //Response.Write(System.IO.Path.GetPathRoot(filePath));
        //    //Response.Write("返回随机文件夹名或文件名。");
        //    //Response.Write(System.IO.Path.GetRandomFileName());
        //    //Response.Write("创建磁盘上唯一命名的零字节的临时文件并返回该文件的完整路径。");
        //    //Response.Write(System.IO.Path.GetTempFileName());
        //    //Response.Write("返回当前系统的临时文件夹的路径。");
        //    //Response.Write(System.IO.Path.GetTempPath());
        //    //Response.Write("确定路径是否包括文件扩展名。");
        //    //Response.Write(System.IO.Path.HasExtension(filePath));
        //    //Response.Write("获取一个值，该值指示指定的路径字符串是包含绝对路径信息还是包含相对路径信息。");
        //    //Response.Write(System.IO.Path.IsPathRooted(filePath));

        //    return Path.GetExtension(resourceName);
        //}
    }
}