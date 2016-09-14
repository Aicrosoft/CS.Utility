namespace CS.Extension
{
    public static class FloatExt
    {

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static float ToFloat(this float p, float min, float max, float defaultValue = 0)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }



        #region string -> ToFloat() Float float 类型处理
       

        /// <summary>
        /// 转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值 0</param>
        /// <returns>转换结果</returns>
        public static float ToFloat(this string p, float defaultValue = 0)
        {
            float result;
            if (!float.TryParse(p, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// /// <param name="defalutValue">默认值 0</param>
        /// <returns>返回值</returns>
        public static float ToFloat(this string p, float min, float max, float defalutValue = 0)
        {
            var r = p.ToFloat(defalutValue);
            return r.ToFloat(defalutValue, min, max);
        }



        #endregion


    }
}