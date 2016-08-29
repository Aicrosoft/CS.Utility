using CS.Serialization;

namespace CS.Extension
{
    public static class XmlSerializeExt
    {
        public static string ToXml(this object o)
        {
            return XmlSerializor.Serialize(o);
        }

        public static T FromXml<T>(this string xml)
        {
            return XmlSerializor.Deserialize<T>(xml);
        }
    }
}