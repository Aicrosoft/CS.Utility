using System;

namespace CS.Extension
{
    public static class ShortExt
    {

        //public static T ToValue<T>(this T p, T min, T max, T defaultValue = default(T))
        //{
        //    if (min <= p && p <= max) return p;
        //    return defaultValue;
        //}


        #region string -> ToShort() short  Int16 类型处理


        /// <summary>
        /// 将字符串转换为 short
        /// </summary>
        /// <param name="param">需要转换的 文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static short ToShort(this string param, short defaultValue = 0)
        {
            short result;
            if (!short.TryParse(param, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defaultValue">默认值 0</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static short ToShort(this string p, short min, short max, short defaultValue = 0)
        {
            var r = p.ToShort(defaultValue);
            return r.ToShort(defaultValue, min, max);
        }


        #endregion



        #region object -> ToShort() short  int16 类型处理


        /// <summary>
        /// 将对象转为int16类型值
        /// </summary>
        /// <param name="param">需要转换的 object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static short ToShort(this object param, short defaultValue = 0)
        {
            if (param == null) return defaultValue;
            if (param is short) return (short)param;
            short val;
            return short.TryParse(param.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static short ToShort(this object obj, short min, short max, short defaultValue = 0)
        {
            var r = obj.ToShort(defaultValue);
            return r.ToShort(max, defaultValue, min);
        }


        #endregion

    }
}