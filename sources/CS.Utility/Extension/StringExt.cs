using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CS.Extension
{
    public static class StringExt
    {

        #region 字符串的基本扩展处理

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 是否为null或空字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        

        /// <summary>
        /// 当值为空时返回为空，否则返回通过template格式化后的值
        /// </summary>
        /// <param name="format"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string FormatWhenNotNull(this string format, params object[] values)
        {
            return values.Length == 0 ? null : string.Format(format, values);
        }

        /// <summary>
        /// 是否为无内容的串
        /// </summary>
        /// <param name="nullString"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string nullString)
        {
            return string.IsNullOrWhiteSpace(nullString);
        }

        /// <summary>
        /// 如果串为空则使用替补串替换
        /// </summary>
        /// <param name="nullString"></param>
        /// <param name="standbyString">替补串</param>
        /// <returns></returns>
        public static string IsEmpty(this string nullString, string standbyString)
        {
            return string.IsNullOrWhiteSpace(nullString) ? standbyString : nullString;
        }

        /// <summary>
        /// 空串时返回第一个参数的值
        /// </summary>
        /// <param name="nullStr">可能为空的串</param>
        /// <param name="trueString">空时返回</param>
        /// <param name="falseString">非空时返回</param>
        /// <returns></returns>
        public static string IsEmpty(this string nullStr, string trueString, string falseString)
        {
            return string.IsNullOrWhiteSpace(nullStr) ? trueString : falseString;
        }

        #endregion


        #region 字符串统计

        /// <summary>
        ///  字符串的字节长度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ByteLength(this string s)
        {
            var l = 0;
            foreach (var c in s)
            {
                if (c > 255)
                    l = l + 2;
                else
                    l++;
            }
            return l;
        }

        #endregion



        #region 字符串剪切

        /// <summary>
        ///     按字节数左剪切
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ByteLeft(this string s, int count)
        {
            int bytecount = 0;
            int charcount = 0;
            foreach (char c in s)
            {
                bytecount += (c > 255 ? 2 : 1);
                if (bytecount >= count)
                    break;
                charcount++;
            }
            return charcount >= s.Length ? s : s.Substring(0, charcount);
        }

        /// <summary>
        ///     按字符数左剪切
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string s, int length)
        {
            return s.Length <= length ? s : s.Substring(0, length);
        }

        /// <summary>
        ///     按字节数右剪切
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ByteRight(this string s, int count)
        {
            int bytecount = 0;
            int charcount = 0;
            foreach (char c in s)
            {
                bytecount += (c > 255 ? 2 : 1);
                if (bytecount >= count)
                    break;
                charcount++;
            }
            return charcount >= s.Length ? s : s.Substring(s.Length - charcount, charcount);
        }

        /// <summary>
        ///     按字符数右剪切
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string s, int length)
        {
            return s.Length <= length ? s : s.Substring(s.Length - length, length);
        }

        #endregion

    }
}