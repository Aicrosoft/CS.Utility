using System;
using System.Collections.Generic;

namespace CS.Extension
{
    /// <summary>
    /// 16进制字符串处理
    /// </summary>
    public static class HexStringExt
    {
        /// <summary>
        /// 将Hex字符串转为字节流，16进制相连，没有连接符
        /// </summary>
        /// <param name="hexStr"></param>
        /// <param name="dashStr">连字符</param>
        /// <returns></returns>
        public static byte[] FromHex(this string hexStr, string dashStr = "-")
        {
            hexStr = hexStr.Replace("-", ""); //默认除去-
            var list = new List<byte>();
            var max = hexStr.Length;
            for (int i = 0; i < max; i++)
            {
                var str = hexStr.Substring(i, 2);
                i++;
                list.Add(Convert.ToByte(str, 16));
            }
            return list.ToArray();
        }
       

    }
}