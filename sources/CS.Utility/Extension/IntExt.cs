using System;
using System.Collections.Generic;
using System.Linq;

namespace CS.Extension
{
    public static class IntExt
    {
        /// <summary>
        /// 当两个给出的int值相同时返回trueStr，否则返回null
        /// </summary>
        /// <param name="int1"></param>
        /// <param name="int2"></param>
        /// <param name="equalStr"></param>
        /// <param name="unequalStr"></param>
        /// <returns></returns>
        public static string IsEqual(this int int1, int int2, string equalStr,string unequalStr = null)
        {
            return int1 == int2 ? equalStr : unequalStr;
        }


        #region ToInt() Int32 类型处理

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="bs"></param>
        ///// <returns></returns>
        //public static int ToInt(this byte[] bs)
        //{
        //    return BitConverter.ToInt32(bs, 0);
        //}


        #endregion


        #region string-> ToInt() Int32 类型处理

        /// <summary>
        /// 将字符串转换为 int ， 转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <returns>转换结果</returns>
        public static int ToInt(this string p)
        {
            return p.ToInt(0);
        }

        /// <summary>
        /// 将字符串转换为 int ， 转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值 0</param>
        /// <returns>转换结果</returns>
        public static int ToInt(this string p, int defaultValue)
        {
            int result;
            if (!int.TryParse(p, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defalutValue">默认值 0</param>
        /// <returns>返回值</returns>
        public static int ToInt(this string p, int min, int max, int defalutValue = 0)
        {
            var r = p.ToInt(defalutValue);
            return r.ToInt(defalutValue, min, max);
        }
       
        #endregion



        #region byte ToInt

        /// <summary>
        /// 返回指定数组的头4字节对应的Int值
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static int ToInt(this byte[] bs)
        {
            return BitConverter.ToInt32(bs, 0);
        }

        #endregion


        #region decimal 至 int的转换

        /// <summary>
        /// 四舍五入转为int
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToInt(this decimal val)
        {
            return (int)Math.Round(val);
        }


        #endregion


        #region ToInt() Range


        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="param">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static int ToInt(this int param, int min, int max, int defalutValue)
        {
            if (min <= param && param <= max) return param;
            return defalutValue;
        }


        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="param">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static int ToInt(this int param, int min, int max)
        {
            return param.ToInt(min, max, 0);
        }

        #endregion


        #region object  ToInt() 

        /// <summary>
        /// 是否为Int32
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsInt32(this object o)
        {
            if (o == null) return false;
            if (o is int) return true;
            int val;
            return Int32.TryParse(o.ToString(), out val);
        }


        /// <summary>
        /// 空对象或转换失败时为默认值
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static int ToInt(this object obj, int defaultValue = 0)
        {
            if (obj == null) return defaultValue;
            if (obj is int) return (int)obj;
            int val;
            return int.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static int ToInt(this object obj, int min, int max, int defalutValue = 0)
        {
            var r = obj.ToInt(defalutValue);
            return r.ToInt(min, max, defalutValue);
        }


        #endregion


        #region string-> int[] ToIntArray(string)  转为数组

        /// <summary>
        /// 可使用条件来返回结果
        /// </summary>
        /// <param name="s"></param>
        /// <param name="predicate"></param>
        /// <param name="separator">分隔符,exp: ,|W+-。即每个Char都将做为分隔符使用</param>
        /// <returns></returns>
        public static IEnumerable<int> ToIntArray(this string s, Predicate<int> predicate, string separator = ",")
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(s))
                return result;
            string[] intsrearray = s.Split(separator.ToCharArray());
            if (intsrearray.Length > 0)
            {
                for (int i = 0; i < intsrearray.Length; i++)
                {
                    int t;
                    if (int.TryParse(intsrearray[i], out t) && predicate(t))
                    {
                        result.Add(t);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 将默认的参数转为数组集合后再或运算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ToOrResult(this string param)
        {
            var arr = param.ToIntArray();
            return arr.Aggregate(0, (current, i) => current | i);
        }


        ///<summary>
        /// 将字符串转为可枚举的数组
        ///</summary>
        ///<param name="str">参数</param>
        ///<param name="spliter">分隔符,exp: ,|W+-</param>
        ///<returns></returns>
        public static IEnumerable<int> ToIntArray(this string str, string spliter = ",")
        {
            return String.IsNullOrWhiteSpace(str) ? new int[0] : str.Split(spliter.ToCharArray()).ToIntArray();
        }

        /// <summary>
        /// 将字符串转为可枚举的数组
        /// </summary>
        /// <param name="param">需要转换的 string[]</param>
        /// <returns>转换结果</returns>
        public static IEnumerable<int> ToIntArray(this string[] param)
        {
            return System.Array.ConvertAll(param, ToInt);
        }


        #endregion




    }
}