using System;
using System.Linq;

namespace CS.Extension
{
    /// <summary>
    /// bool结构扩展
    /// </summary>
    public static class BooleanExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="express"></param>
        /// <param name="trueStr"></param>
        /// <param name="falseStr"></param>
        /// <returns></returns>
        public static string If(this bool express, string trueStr, string falseStr = null)
        {
            return express ? trueStr : falseStr;
        }


        #region object -> ToBool 字符串转为Bool


        /// <summary>
        /// null或转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static bool ToBool(this object p, bool defaultValue = false)
        {
            if (p == null) return defaultValue;
            if (p is bool) return (bool)p;
            bool val;
            return bool.TryParse(p.ToString(), out val) ? val : defaultValue;
        }

        #endregion

        #region string -> ToBool 字符串转为Bool

        /// <summary>
        /// 参数P是否被成功转换,类似于TryParse
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值，转换失败时输出的值</param>
        /// <param name="result">输出转换的结果</param>
        /// <param name="trueChars">判定为真的字符串的设定字符集</param>
        /// <returns>是否转换成功</returns>
        public static bool TryBool(this string p, bool defaultValue, out bool result, params string[] trueChars)
        {
            var rst = false; //默认转换失败
            result = defaultValue;  //默认结果为默认值
            if (p == null) return defaultValue;
            if (trueChars.Any(s => p.Equals(s, StringComparison.CurrentCultureIgnoreCase)))
            {
                rst = true;
                result = true;
            }
            if (!rst)
                rst = bool.TryParse(p, out result);
            return rst;
        }

        /// <summary>
        /// 将参数p转换为bool
        /// <remarks>"1"默认为真</remarks>
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">转换失败时的值</param>
        /// <param name="trueChars">判定为真时的字符串变参，默认为真的是:"1","true"</param>
        /// <returns>转换结果</returns>
        public static bool ToBoolWithTrueChars(this string p, bool defaultValue, params string[] trueChars)
        {
            bool result;
            var chars = trueChars.Length == 0 ? new[] { "1", "true" } : trueChars;
            TryBool(p, defaultValue, out result, chars);
            return result;
        }

        /// <summary>
        /// 将参数p转换为bool
        /// </summary>
        /// <param name="p"></param>
        /// <param name="trueChars">判定为真时的字符串变参，默认为真的是:"1","true"</param>
        /// <returns>转换时为false</returns>
        public static bool ToBoolWithTrueChars(this string p, params string[] trueChars)
        {
            return ToBoolWithTrueChars(p, false, trueChars);
        }


        /// <summary>
        /// 将参数p转换为bool
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认结果，转换失败时的值</param>
        /// <returns>转换结果</returns>
        public static bool ToBool(this string p, bool defaultValue = false)
        {
            bool val;
            var rst = bool.TryParse(p, out val);
            return rst ? val : defaultValue;
        }


        #endregion

    }
}