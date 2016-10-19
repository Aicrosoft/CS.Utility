using System.Text.RegularExpressions;

namespace CS.Extension
{

    /// <summary>
    /// 多用于HTML页面上的相关扩展
    /// </summary>
    public static class HtmlExt
    {

        #region Html 安全字符串处理

        /// <summary>
        /// 转换为安全的HTML字符串
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ToSafeHtml(this string html)
        {
            var sf = html.Replace("<", "&#60");
            sf = sf.Replace(">", "&#62");
            return sf;
        } 

        /// <summary>
        /// 先截取后转为安全HTML字符串
        /// </summary>
        /// <param name="html"></param>
        /// <param name="maxLength"></param>
        /// <param name="omitStr">默认省略字符串为空串</param>
        /// <returns></returns>
        public static string CutToSafeHtml(this string html, int maxLength, string omitStr = "")
        {
            var val = $"{html.ByteLeft(maxLength)}{omitStr}";
            return val.ToSafeHtml();
        }

        #endregion


        /// <summary>
        /// 将Html代清除后截取一定长度做为摘要信息
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length"></param>
        /// <param name="omitStr"></param>
        /// <returns></returns>
        public static string ToSummary(this string html, int length,string omitStr = "...")
        {
            var txt = html.RemoveHtml();
            return txt.CutToSafeHtml(length, omitStr);
        }

        /// <summary>
        /// 移除Html的所有标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveHtml(this string html)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.Compiled);
            return reg.Replace(html, "");
        }

        /// <summary>
        /// 生成键与值相同的属性-值 key="value" 字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToProperty(this string value)
        {
            return value.ToProperty(value);
        }

        /// <summary>
        /// 如果值不为空时生成 key="value" 字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToProperty(this string value, string key)
        {
            return string.IsNullOrWhiteSpace(value) ? null : $"{key}=\"{value}\"";
        }

        /// <summary>
        /// 如果值不为空时生成 key="value" 字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SetProperty(this string key, string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : $"{key}=\"{value}\"";
        }

        /// <summary>
        /// 根据表达式结果设定属性值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="express"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SetProperty(this  bool express, string key, string value)
        {
            return express ? key.SetProperty(value) : null;
        }


        ///// <summary>
        ///// 生成Json格式的字符串属性
        ///// </summary>
        ///// <param name="express"></param>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static string SetJsonProperty(this bool express, string key, string value)
        //{
        //    return express ? key.SetJsonProperty(value) : null;
        //}
        ///// <summary>
        ///// 生成Json格式的字符串属性
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static string SetJsonProperty(this string key, string value)
        //{
        //    return string.IsNullOrWhiteSpace(value) ? null : $"\"{key}\"={value}";
        //}
    }
}