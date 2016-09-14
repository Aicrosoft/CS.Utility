namespace CS.Extension
{
    public static class DecimalExt
    {


        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defaultValue">默认值 0</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static decimal ToDecimal(this decimal p, decimal min, decimal max, decimal defaultValue = 0)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }



        #region string -> ToDecimal() decimal Decimal 类型处理


        /// <summary>
        /// 转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值   0</param>
        /// <returns>转换结果</returns>
        public static decimal ToDecimal(this string p, decimal defaultValue = 0)
        {
            decimal result;
            if (!decimal.TryParse(p, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static decimal ToDecimal(this string p, decimal min, decimal max, decimal defalutValue = 0)
        {
            var r = p.ToDecimal(defalutValue);
            return r.ToDecimal(min, max, defalutValue);
        }

        #endregion


        #region object -> ToDouble() double Double 类型处理

        /// <summary>
        /// 空对象或转换失败时为默认值（0）
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值 0</param>
        /// <returns>转换结果</returns>
        public static double ToDouble(this object obj, double defaultValue = 0)
        {
            if (obj == null) return defaultValue;
            if (obj is double) return (double)obj;
            double val;
            return double.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defalutValue">默认值 0</param>
        /// <returns>返回值</returns>
        public static double ToDouble(this object obj, double min, double max, double defalutValue = 0)
        {
            var r = obj.ToDouble(defalutValue);
            return r.ToDouble(defalutValue, min, max);
        }


        #endregion


    }
}