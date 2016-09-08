#region copyright info

//------------------------------------------------------------------------------
// <copyright company="ChaosStudio">
//     Copyright (c) 2002-2010 巧思工作室.  All rights reserved.
//     Contact:		MSN:zhouyu@cszi.com , QQ:478779122
//		Link:				http://www.69sea.com http://www.cszi.com
// </copyright>
//------------------------------------------------------------------------------

#endregion

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CS.Storage
{
    /// <summary>
    ///   对象二进制持久化
    /// <para>采用序列化方式</para>
    /// </summary>
    /// 
    /// <description class = "CS.Data.Storage.FileStorage">
    ///   
    /// </description>
    /// 
    /// <history>
    ///   2010-7-15 15:07:01 , zhouyu ,  创建	     
    ///  </history>
    public class FileStorage
    {
        /// <summary>
        /// 对象序列化后保存
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">保存路径</param>
        /// <param name="value">对象的引用</param>
        public static void Save<T>(string path, T value)
        {
            var fs = new FileStream(path, FileMode.Create);
            var formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, value);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 将保存后的文件反序列化成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns>对象实例</returns>
        public static T Load<T>(string path)
        {
            var fs = new FileStream(path, FileMode.Open);
            var formatter = new BinaryFormatter();
            try
            {
                return (T) formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

  

    }
}