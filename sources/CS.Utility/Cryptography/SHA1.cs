using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CS.Cryptography
{
    /// <summary>
    /// SHA1签名
    /// </summary>
    public class Sha1
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Encrypt(string source)
        {
            return Encrypt(source, Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Encrypt(byte[] bytes)
        {
            var provider = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(provider.ComputeHash(bytes)).Replace("-", "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Encrypt(string source, Encoding encoding)
        {
            return Encrypt(encoding.GetBytes(source));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Encrypt(string source, Encoding encoding, Md5FormatType type)
        {
            string str = Encrypt(source, encoding);
            var builder = new StringBuilder(0x20);
            if (type.HasFlag(Md5FormatType.One))
            {
                builder.Append(str.Substring(0, 8));
            }
            if (type.HasFlag(Md5FormatType.Two))
            {
                builder.Append(str.Substring(8, 8));
            }
            if (type.HasFlag(Md5FormatType.Three))
            {
                builder.Append(str.Substring(0x10, 8));
            }
            if (type.HasFlag(Md5FormatType.Four))
            {
                builder.Append(str.Substring(0x18, 8));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 创建随机字符串  
        /// </summary>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var arrStr = new List<string>(16);
            var rad = new Random();
            for (var i = 0; i < length; i++)
            {
                arrStr.Add(chars.Substring(rad.Next(0, chars.Length - 1), 1));
            }
            return string.Concat(arrStr.ToArray());
        }


    }



}