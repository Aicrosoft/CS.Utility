using System;
using System.Xml.Serialization;

namespace CS.Serialization
{
    /// <summary>
    /// CDATA XML数据
    /// </summary>
    [Serializable]
    public class XmlCdataText
    {
        /// <summary>
        /// 带CDATA的文本
        /// </summary>
        [XmlText]
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}