
using System.IO;


namespace CS.Storage
{
    /// <summary>
    ///   
    /// </summary>
    /// 
    /// <description class = "CS.Data.Storage.JsonStorage">
    ///   
    /// </description>
    /// 
    /// <history>
    ///   2010-7-15 15:14:01 , zhouyu ,  创建	     
    ///  </history>
    public class JsonStorage
    {
        ///// <summary>
        ///// 保存至流
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="value"></param>
        ///// <param name="stream"></param>
        ///// <returns></returns>
        // public static Stream SaveToStream<T>(T value, Stream stream = null)
        //{
        //    if (stream == null) stream = new MemoryStream();
        //    (new DataContractJsonSerializer(typeof(T))).WriteObject(stream, value);
        //    return stream;
        //}
        ///// <summary>
        ///// 保存至文件
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static string SaveToText<T>(T value)
        //{
        //    var stream = SaveToStream(value);
        //    stream.Position = 0;

        //    StreamReader sr = new StreamReader(stream);
        //    var result = sr.ReadToEnd();
        //    sr.Close();

        //    return result;
        //}

        ///// <summary>
        ///// 从流中载入
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="stream"></param>
        ///// <returns></returns>
        //public static T LoadFromStream<T>(Stream stream)
        //{
        //    stream.Position = 0;
        //    return (T)(new DataContractJsonSerializer(typeof(T))).ReadObject(stream);
        //}

        ///// <summary>
        ///// 从文本中载入
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static T LoadFromText<T>(string value)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    StreamWriter writer = new StreamWriter(ms);
        //    writer.Write(value);

        //    var result = LoadFromStream<T>(ms);
        //    writer.Close();

        //    return result;
        //}


    }
}

