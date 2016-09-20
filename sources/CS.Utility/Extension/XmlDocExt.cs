using CS.Serialization;

namespace CS.Extension
{
    public static class XmlDocExt
    {

        #region ToXml,FromXml

        /// <summary>
        /// 序列化成XML
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToXml<T>(this T o)
        {
            // xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"
            var rst = XmlSerializor.Serialize(o);
            return rst.Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
        }

        /// <summary>
        /// 将XML序列化成目标对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T FromXml<T>(this string xml)
        {
            return XmlSerializor.Deserialize<T>(xml);
        }

        #endregion


    }
}