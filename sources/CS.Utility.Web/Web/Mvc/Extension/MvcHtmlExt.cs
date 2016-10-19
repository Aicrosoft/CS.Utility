using System;

namespace CS.Web.Mvc.Extension
{
    public static class MvcHtmlExt
    {
        ///// <summary>
        ///// 将Html代码输出，如果表达式为真时
        ///// </summary>
        ///// <param name="htmlStr"></param>
        ///// <param name="express"></param>
        ///// <returns></returns>
        //public static MvcHtmlString AppendIfEqual(this string htmlStr, bool express )
        //{
        //    return express ? new MvcHtmlString(htmlStr) : null;
        //}



        /// <summary>
        /// 真时返回trueStr,或者是falseStr
        /// </summary>
        /// <param name="express"></param>
        /// <param name="trueStr"></param>
        /// <param name="falseStr"></param>
        /// <returns></returns>
        public static string AppendIf(this bool express, string trueStr,
            string falseStr = null)
        {
            return express ? trueStr : falseStr;
        }

        /// <summary>
        /// 如果两name相等时（不区分大小写）返回trueStr,或者是falseStr
        /// </summary>
        /// <param name="name"></param>
        /// <param name="comparedName"></param>
        /// <param name="trueStr"></param>
        /// <param name="falseStr"></param>
        /// <returns></returns>
        public static string AppendIfEquals(this string name, string comparedName, string trueStr,
            string falseStr = null)
        {
            return name.IsEquals(comparedName) ? trueStr : falseStr;
        }

        /// <summary>
        /// 忽略大小写的比较
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <returns></returns>
        public static bool IsEquals(this string name1, string name2)
        {
            return name1.Equals(name2, StringComparison.OrdinalIgnoreCase);
        }
    }
}