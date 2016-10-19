using System;

namespace CS.Attribute
{
    /// <summary>
    /// 文本描述
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class TextAttribute:System.Attribute
    {
        /// <summary>
        /// 本地化显示的名称
        /// </summary>
        /// <param name="name"></param>
        public TextAttribute(string name)
        {
            NativeName = name;
        }

        /// <summary>
        /// 本地化名称或本地化名称的代称（一个键可以通过一定规则译成本地语言）
        /// <remarks>显示给用户查看的枚举值</remarks>
        /// </summary>
        public string NativeName { get; set; }
    }
}