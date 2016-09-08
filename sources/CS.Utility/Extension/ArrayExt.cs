using System;
using System.Collections.Generic;
using System.Linq;

namespace CS.Extension
{
    public static class ArrayExt
    {

        /// <summary>
        /// 将某一数尽量平均分配到一个数组中去
        /// <remarks>
        /// 
        /// </remarks>
        /// </summary>
        /// <param name="totalAmount"></param>
        /// <param name="arrLength">注意，数组太长会抛异常的</param>
        /// <returns></returns>
        public static int[] AverageToArray(this int totalAmount, int arrLength)
        {
            try
            {
                var couVal = totalAmount;
                var arr = new int[arrLength];
                var val = totalAmount / arrLength;
                //Console.WriteLine($"val:{val}");
                if (val != 0)
                {
                    for (int i = 0; i < arrLength; i++)
                    {
                        arr[i] = val;
                        couVal -= val;
                    }
                }

                var yu = totalAmount % arrLength;
                //Console.WriteLine($"yu:{yu}");
                var addVal = yu > 0 ? 1 : -1;
                if (yu != 0)
                {
                    for (int i = 0; i < arrLength; i++)
                    {
                        if (couVal == 0) break;
                        arr[i] = arr[i] + addVal;
                        couVal -= addVal;
                    }
                }
                return arr;
            }
            catch (Exception)
            {
                throw new ArgumentException($"{arrLength} is too long.");
            }

        }


        /// <summary>
        /// 将id数据用 "," 连接成一定长度的新的分组后的数组，每组长度不超过设定的值。
        /// </summary>
        /// <param name="h">被扩展的对象(待处理的字符串数组)</param>
        /// <param name="lengthLimit">每一组连接后的最大长度</param>
        /// <param name="joinChar">连接串字符</param>
        /// <returns></returns>
        public static string[] GroupJoin(this string[] h, /*string[] id,*/ int lengthLimit, string joinChar)
        {
            var maxLength = h.Max(x => x.Length);
            if (maxLength >= lengthLimit) throw new ArgumentOutOfRangeException(nameof(lengthLimit), @"值不能小于数组中的最大长度的元素的长度数值。");

            var joinCharLength = joinChar.Length;
            var allLength = h.Sum(s => s.Length);

            if (allLength + (h.Length - 1) * joinCharLength <= lengthLimit)
                return new[] { string.Join(joinChar, h) };

            var arr = new List<string>();
            var arrT = new List<string>();
            var length = 0;
            foreach (var s in h)
            {
                length += joinCharLength + s.Length;
                if (length >= lengthLimit)
                {
                    arr.Add(string.Join(",", arrT.ToArray()));
                    arrT.Clear();

                    //当前的内容加到临时集合中并初始化长度
                    arrT.Add(s);
                    length = s.Length;
                }
                else
                {
                    arrT.Add(s);
                }
            }
            if (arrT.Count > 0)
            {
                arr.Add(string.Join(",", arrT.ToArray()));
            }
            return arr.ToArray();
        }
    }
}