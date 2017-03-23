using System;
using CS.Components;
using CS.Html;

namespace CS.Attribute
{
    /// <summary>
    /// 枚举文本描述
    /// <remarks>
    /// 仅支持int类型
    /// </remarks>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class HtmlExtAttribute : System.Attribute
    {
        public HtmlExtAttribute(string label, HtmlElementType element = HtmlElementType.None)
        {
            Label = label;
            Element = element;
        }

        /// <summary>
        /// 是否忽略当前项
        /// </summary>
        public bool Ignore { get; set; }

        public string Label { get; private set; }

        public string Description { get; set; }
        /// <summary>
        /// 标记为该类型的元素将只能读取
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        public string DefaultValue { get; set; }

        /// <summary>
        /// 当前属性元素类型
        /// </summary>
        public HtmlElementType Element { get; private set; }

    }



}