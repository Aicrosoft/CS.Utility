using System;
using System.Collections.Generic;

namespace CS.Extension
{
    public static class LongExt
    {

        #region 区间取值 

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static long ToLong(this long p, long min, long max, long defaultValue)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static long ToLong(this long p, long min, long max)
        {
            return p.ToLong(0, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static ulong ToLong(this ulong p, ulong defaultValue, ulong min, ulong max)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static ulong ToLong(this ulong p, ulong min, ulong max)
        {
            return p.ToLong(0, min, max);
        }


        #endregion


        #region string  ToLong() long Int64 类型处理


        /// <summary>
        /// 转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值  0</param>
        /// <returns>转换结果</returns>
        public static long ToLong(this string p, long defaultValue = 0)
        {
            long result;
            if (!long.TryParse(p, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defalutValue">默认值  为0</param>
        /// <returns>返回值</returns>
        public static long ToLong(this string p, long min, long max, long defalutValue = 0)
        {
            var r = p.ToLong(defalutValue);
            return r.ToLong(min, max, defalutValue);
        }


        #endregion


        #region object -> ToLong() Int64 类型处理


        /// <summary>
        /// 空对象或转换失败时为默认值
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值 0</param>
        /// <returns>转换结果</returns>
        public static long ToLong(this object obj, long defaultValue = 0)
        {
            if (obj == null) return defaultValue;
            if (obj is long) return (long)obj;
            long val;
            return long.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defalutValue">默认值 0</param>
        /// <returns>返回值</returns>
        public static long ToLong(this object obj, long min, long max, long defalutValue = 0)
        {
            var r = obj.ToLong(defalutValue);
            return r.ToLong(defalutValue, min, max);
        }


        #endregion


        #region string -> long[] ToLongArray()  转为数组

        /// <summary>
        /// 可使用条件来返回结果
        /// </summary>
        /// <param name="s"></param>
        /// <param name="predicate"></param>
        /// <param name="separator">分隔符,exp: ,|W+-。即每个Char都将做为分隔符使用</param>
        /// <returns></returns>
        public static IEnumerable<long> ToLongArray(this string s, Predicate<long> predicate, string separator = ",")
        {
            var result = new List<long>();
            if (string.IsNullOrEmpty(s))
                return result;
            string[] intsrearray = s.Split(separator.ToCharArray());
            if (intsrearray.Length > 0)
            {
                for (long i = 0; i < intsrearray.Length; i++)
                {
                    long t;
                    if (long.TryParse(intsrearray[i], out t) && predicate(t))
                    {
                        result.Add(t);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 将字符串转为可枚举的数组
        /// </summary>
        /// <param name="param">需要转换的文本</param>
        /// <returns>转换结果</returns>
        public static IEnumerable<long> ToLongArray(this string param)
        {
            return param.ToLongArray(",");
        }

        ///<summary>
        /// 将字符串转为可枚举的数组
        ///</summary>
        ///<param name="str">参数</param>
        ///<param name="spliter">分隔符,exp: ,|W+-</param>
        ///<returns></returns>
        public static IEnumerable<long> ToLongArray(this string str, string spliter)
        {
            return String.IsNullOrWhiteSpace(str) ? new long[0] : str.Split(spliter.ToCharArray()).ToLongArray();
        }

        /// <summary>
        /// 将字符串转为可枚举的数组
        /// </summary>
        /// <param name="param">需要转换的 string[]</param>
        /// <returns>转换结果</returns>
        public static IEnumerable<long> ToLongArray(this string[] param)
        {
            return Array.ConvertAll(param, ToLong);
        }




        #endregion


    }
}