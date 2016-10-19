using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS.Extension
{
    /// <summary>
    /// Byte的相关的基本类型的互相转换
    /// </summary>
    public static class ByteExtension
    {

        #region stirng -> ToByte() ToPackageByte() btye[] 字符串转为byte[]

        /// <summary>
        /// 将字符串按UTF8格式转为byte[]
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string param)
        {
            return Encoding.UTF8.GetBytes(param);
        }

        /// <summary>
        /// 将字符串按某种编码格式转换为字节流
        /// </summary>
        /// <param name="param"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string param, Encoding encoding)
        {
            return encoding.GetBytes(param);
        }

        /// <summary>
        /// 将字符串转为UTF8的字节数组并在头上加上长度
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static byte[] ToBytesPackage(this string message)
        {
            return message.ToBytes().ToPackage();
        }

        /// <summary>
        /// 将字符串转为某种编码的字节数组并在头上加上长度
        /// </summary>
        /// <param name="message"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytesPackage(this string message, Encoding encoding)
        {
            return message.ToBytes(encoding).ToPackage();
        }


        #endregion


        #region HexToByte 16制字符串转为Byte数组

        /// <summary>
        /// 16制字符串转为Byte数组
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexToByte(this string hex)
        {
            var fixedHex = hex.Replace("-", string.Empty);
            // array to put the result in
            var bytes = new byte[fixedHex.Length / 2];
            // variable to determine shift of high/low nibble
            var shift = 4;
            // offset of the current byte in the array
            var offset = 0;
            // loop the characters in the string
            foreach (char c in fixedHex)
            {
                // get character code in range 0-9, 17-22
                // the % 32 handles lower case characters
                var b = (c - '0') % 32;
                // correction for a-f
                if (b > 9) b -= 7;
                // store nibble (4 bits) in byte array
                bytes[offset] |= (byte)(b << shift);
                // toggle the shift variable between 0 and 4
                shift ^= 4;
                // move to next byte
                if (shift != 0) offset++;
            }
            return bytes;
        }

        #endregion


        //#region 常用类型转为Byte

        ///// <summary>
        ///// 将int 转为byte[]
        ///// </summary>
        ///// <param name="val"></param>
        ///// <returns></returns>
        //public static byte[] ToBytes(this int val)
        //{
        //    return  BitConverter.GetBytes(val);
        //}

        //#endregion



        ///// <summary>
        ///// 将C#中的Byte转为Java中的Byte
        ///// <remarks>
        ///// 原理很简单，就是当 byte 小于 128 时其值保持不变，大于等于 128 时就将其减去 256。BUG：还有字节序问题
        ///// </remarks>
        ///// </summary>
        ///// <param name="bytes"></param>
        ///// <returns></returns>
        //public static sbyte[] ToJavaBytes(this byte[] bytes)
        //{
        //    var arrResult = new sbyte[bytes.Length];
        //    for (var i = 0; i < bytes.Length; i++)
        //    {
        //        if (bytes[i] > 127)
        //            arrResult[i] = (sbyte)(bytes[i] - 256);
        //        else
        //            arrResult[i] = (sbyte)bytes[i];
        //    }
        //    return arrResult;
        //}

        /// <summary>
        /// 返回Int对应的字节数组
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this int param)
        {
            return BitConverter.GetBytes(param);
        }

        /// <summary>
        /// 返回Uint对应的字节数组
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this uint pm)
        {
            return BitConverter.GetBytes(pm);
        }



        /// <summary>
        /// 从原字节数组中返回新的数组
        /// </summary>
        /// <param name="bs">源数组</param>
        /// <param name="length">返回的长度</param>
        /// <returns></returns>
        public static byte[] GetBytes(this byte[] bs, int length)
        {
            return bs.GetBytes(0, length);
        }

        /// <summary>
        /// 从原字节数组中返回新的数组
        /// </summary>
        /// <param name="bs">源数组</param>
        /// <param name="index">源数组起始索引</param>
        /// <param name="length">返回的长度</param>
        /// <returns></returns>
        public static byte[] GetBytes(this byte[] bs, int index, int length)
        {
            var newBs = new byte[length];
            Buffer.BlockCopy(bs, 0, newBs, index, length);
            return newBs;
        }

        /// <summary>
        /// 将字节流按UTF8编码返回结果字符串
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static string ToUtf8(this byte[] bs)
        {
            return Encoding.UTF8.GetString(bs);
        }

        /// <summary>
        /// 将字节流按UTF8编码返回结果字符串
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="index">字节流中起始索引</param>
        /// <param name="length">有效长度</param>
        /// <returns></returns>
        public static string ToUtf8(this byte[] bs, int index, int length)
        {
            return Encoding.UTF8.GetString(bs, index, length);
        }

        /// <summary>
        /// 将字节流按编码返回结果字符串
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToString(this byte[] bs, Encoding encoding)
        {
            return encoding.GetString(bs);
        }

        /// <summary>
        /// 返回16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes)
        {
            return ToHexString(bytes, false);
        }
        /// <summary>
        /// 返回16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="length">转换长度</param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes, int startIndex, int length)
        {
            return bytes.ToHexString(startIndex, length, false);
        }

        /// <summary>
        /// 返回16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="removeDashes">是否移除连字符</param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes, bool removeDashes)
        {
            string hex = BitConverter.ToString(bytes);
            if (removeDashes)
                hex = hex.Replace("-", "");

            return hex;
        }

        /// <summary>
        /// 返回16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="length">转换长度</param>
        /// <param name="removeDashes">是否移除连字符</param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes, int startIndex, int length, bool removeDashes)
        {
            string hex = BitConverter.ToString(bytes, startIndex, length);
            if (removeDashes)
                hex = hex.Replace("-", "");

            return hex;
        }

        /// <summary>
        /// 返回2进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBinString(this byte[] bytes)
        {
            return bytes.ToBinString("-");
        }

        /// <summary>
        /// 返回2进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="dashes">连字符</param>
        /// <returns></returns>
        public static string ToBinString(this byte[] bytes, string dashes)
        {
            var list = new List<string>(bytes.Length);
            list.AddRange(bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
            return string.Join(dashes, list.ToArray());
        }

        /// <summary>
        /// 返回2进制字符串
        /// </summary>
        /// <param name="sbs"></param>
        /// <returns></returns>
        public static string ToBinString(this sbyte[] sbs)
        {
            return sbs.ToBinString("-");
        }

        /// <summary>
        /// 返回2进制字符串
        /// </summary>
        /// <param name="sbs"></param>
        /// <param name="dashes"></param>
        /// <returns></returns>
        public static string ToBinString(this sbyte[] sbs, string dashes)
        {
            var list = new List<string>(sbs.Length);
            list.AddRange(sbs.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
            return string.Join(dashes, list.ToArray());
        }

        /// <summary>
        /// 将小于2G的byte[]数据前加上长度标识
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] ToPackage(this byte[] bytes)
        {
            var length = bytes.Length;
            var packageLength = length + 4;
            var bs = new byte[packageLength];
            Buffer.BlockCopy(BitConverter.GetBytes(packageLength), 0, bs, 0, 4);
            Buffer.BlockCopy(bytes, 0, bs, 4, length);
            //Array.Copy(BitConverter.GetBytes(length), 0, bs, 0, 4);
            //Array.Copy(bytes, 0, bs, 4, length);
            return bs;
        }

        /// <summary>
        /// 字节流转为base64字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

    }
}