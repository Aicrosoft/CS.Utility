using System;

namespace CS.Extension
{
    public static class DateTimeExt
    {

        #region 基本扩展

        /// <summary>
        /// 获取本月的最后一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToMonthEndDate(this DateTime dt)
        {
            return dt.ToMonthBeginDate().AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 获取本日期的第一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToMonthBeginDate(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static DateTime ToDateTime(this DateTime p, DateTime min, DateTime max, DateTime defaultValue)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为 0001/1/1 0:00:00
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static DateTime ToDateTime(this DateTime p, DateTime min, DateTime max)
        {
            return p.ToDateTime(min, max, DateTime.MinValue);
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static DateTime? ToNullDateTime(this DateTime? p, DateTime min, DateTime max, DateTime? defaultValue = null)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }


        static readonly DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));

        /// <summary>
        ///  换为Unix时间戳格式(毫秒)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTime(this DateTime dateTime)
        {
            return (long)Math.Round((dateTime - StartTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        ///    换为Unix时间戳格式(秒)
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>double</returns>
        public static int ToSecondTime(this DateTime dateTime)
        {
            return (int)Math.Round((dateTime - StartTime).TotalSeconds, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 将毫秒时间转为当前时间
        /// </summary>
        /// <param name="millionseconds"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(long millionseconds)
        {
            return StartTime.AddMilliseconds(millionseconds);
        }
        /// <summary>
        /// 将秒时间转为当前时间
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(int seconds)
        {
            return StartTime.AddSeconds(seconds);
        }




        #endregion


        #region string -> ToDateTime() DateTime 类型处理

        /// <summary>
        /// 默认值为 默认值为 0001/1/1 0:00:00
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static DateTime ToDateTime(this string str)
        {
            return str.ToDateTime(DateTime.MinValue);
        }

        /// <summary>
        /// 默认值为 0001/1/1 0:00:00
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static DateTime ToDateTime(this string p, DateTime defaultValue)
        {
            DateTime result;
            if (!DateTime.TryParse(p, out result)) result = defaultValue;
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
        public static DateTime ToDateTime(this string p, DateTime min, DateTime max, DateTime defalutValue)
        {
            var r = p.ToDateTime(defalutValue);
            return r.ToDateTime(min, max, defalutValue);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为 0001/1/1 0:00:00
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static DateTime ToDateTime(this string p, DateTime min, DateTime max)
        {
            return p.ToDateTime(min, max, DateTime.MinValue);
        }


        #endregion

        #region object ->  ToDateTime() DateTime 类型处理

        /// <summary>
        /// 默认值 0001/1/1 0:00:00
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj)
        {
            return obj.ToDateTime(DateTime.MinValue);
        }

        /// <summary>
        /// 空对象或转换失败时为默认值
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static DateTime ToDateTime(this object obj, DateTime defaultValue)
        {
            if (obj == null) return defaultValue;
            if (obj is DateTime) return (DateTime)obj;
            DateTime val;
            return DateTime.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static DateTime ToDateTime(this object obj, DateTime min, DateTime max, DateTime defalutValue)
        {
            var r = obj.ToDateTime(defalutValue);
            return r.ToDateTime(min, max, defalutValue);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0001/1/1 0:00:00
        /// </summary>
        /// <param name="o"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object o, DateTime min, DateTime max)
        {
            return o.ToDateTime(min, max, DateTime.MinValue);
        }

        #endregion


        #region string ->  ToNullDateTime() DateTime 类型处理


        /// <summary>
        /// 转换失败时为 返回默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static DateTime? ToNullDateTime(this string p, DateTime? defaultValue = null)
        {
            DateTime result;
            return DateTime.TryParse(p, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static DateTime? ToNullDateTime(this string p, DateTime min, DateTime max, DateTime? defalutValue = null)
        {
            var r = p.ToNullDateTime(defalutValue);
            return r.HasValue ? r.Value.ToNullDateTime(min, max, defalutValue) : defalutValue;
        }


        #endregion


        #region object -> ToNullDateTime() DateTime 类型处理


        /// <summary>
        /// 空对象或转换失败时为默认值
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static DateTime? ToNullDateTime(this object obj, DateTime? defaultValue = null)
        {
            if (obj == null) return defaultValue;
            if (obj is DateTime) return (DateTime)obj;
            DateTime val;
            return DateTime.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static DateTime? ToNullDateTime(this object obj, DateTime min, DateTime max, DateTime? defalutValue = null)
        {
            var r = obj.ToNullDateTime(defalutValue);
            return r.HasValue ? r.ToNullDateTime(min, max, defalutValue) : defalutValue;
        }


        #endregion


     

    }
}