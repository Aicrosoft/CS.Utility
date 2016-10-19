namespace CS.Extension
{
    public static class UShortExt
    {

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defaultValue">默认值  0</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static short ToShort(this short p, short min, short max, short defaultValue = 0)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }


        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static ushort ToUShort(this ushort p, ushort min, ushort max, ushort defaultValue = 0)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }




        #region string -> ToUShort() ushort  UInt16 类型处理


        /// <summary>
        /// 将字符串转换为 short
        /// </summary>
        /// <param name="param">需要转换的 文本</param>
        /// <param name="defaultValue">默认值  0</param>
        /// <returns>转换结果</returns>
        public static ushort ToUShort(this string param, ushort defaultValue = 0)
        {
            ushort result;
            if (!ushort.TryParse(param, out result)) result = defaultValue;
            return result;
        }


        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defaultValue">默认值  0</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static ushort ToUShort(this string p, ushort min, ushort max, ushort defaultValue = 0)
        {
            var r = p.ToUShort(defaultValue);
            return r.ToUShort(defaultValue, min, max);
        }



        #endregion



        #region object -> ToUShort() ushort  UInt16 类型处理


        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">需要转换的 object</param>
        /// <param name="defaultValue">默认值 0</param>
        /// <returns>转换结果</returns>
        public static ushort ToUShort(this object param, ushort defaultValue = 0)
        {
            if (param == null) return defaultValue;
            if (param is ushort) return (ushort)param;
            ushort val;
            return ushort.TryParse(param.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static ushort ToUShort(this object obj, ushort min, ushort max, ushort defaultValue = 0)
        {
            var r = obj.ToUShort(defaultValue);
            return r.ToUShort(min, max, defaultValue);
        }

      

        #endregion

    }
}