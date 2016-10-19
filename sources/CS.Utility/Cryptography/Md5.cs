using System;
using System.Security.Cryptography;
using System.Text;

namespace CS.Cryptography
{
    /// <summary>
    ///   Md5相关处理
    /// </summary>
    public class Md5
    {
        /// <summary>
        /// 获取Md5信息摘要(采用UTF8编码方式)
        /// </summary>
        /// <param name="source">原文</param>
        /// <returns>加密后字符串</returns>
        public static string Encrypt(string source)
        {
            return Encrypt(source, Encoding.UTF8);
        }

        /// <summary>
        /// 按编码返回MD5
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Encrypt(string source, Encoding encoding)
        {
            return Encrypt(encoding.GetBytes(source));
        }

        /// <summary>
        /// 直接对字节流进行摘要
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Encrypt(byte[] bytes)
        {
            var md5 = new MD5CryptoServiceProvider();
            var strRet = BitConverter.ToString(md5.ComputeHash(bytes));
            return strRet.Replace("-", "").ToLower();
        }

        /// <summary>
        ///  获取Md5信息摘要[按8位一组获取]
        /// </summary>
        /// <param name="source">原文</param>
        /// <param name="encoding"></param>
        /// <param name="type">获取摘要后的位雄段</param>
        /// <returns>不管type如何组合，低位在前</returns>
        public static string Encrypt(string source,Encoding encoding, Md5FormatType type)
        {
            var md5 = Encrypt(source,encoding);
            var sb = new StringBuilder(32);   //最多32位
            if (type.HasFlag(Md5FormatType.One))
            {
                sb.Append(md5.Substring(0, 8));
            }
            if (type.HasFlag(Md5FormatType.Two))
            {
                sb.Append(md5.Substring(8, 8));
            }
            if (type.HasFlag(Md5FormatType.Three))
            {
                sb.Append(md5.Substring(16, 8));
            }
            if (type.HasFlag(Md5FormatType.Four))
            {
                sb.Append(md5.Substring(24, 8));
            }
            return sb.ToString();
        }


    }

    /// <summary>
    /// 按8位一组，组合方式
    /// </summary>
    [Flags]
    public enum Md5FormatType
    {
        /// <summary>
        /// 第一组
        /// </summary>
        One = 1,
        /// <summary>
        /// 第二组
        /// </summary>
        Two = 2,
        /// <summary>
        /// 第三组
        /// </summary>
        Three = 4,
        /// <summary>
        /// 第四组
        /// </summary>
        Four = 8,

    }
}