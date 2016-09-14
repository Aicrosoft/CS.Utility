using System;

namespace CS.Extension
{
    public static class EnumExt
    {


        #region string  ToEnum  T ToEnum<T>(string)

        /// <summary>
        /// 将字符串转换为 枚举
        /// <remarks>
        /// 如果是数字字符串时，枚举转换失败时输出原输入的字符串。
        /// 如果是非纯数字字符串时，有默认值时输出默认值，无时输出枚举中的第一项。
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">目标枚举类型</typeparam>
        /// <param name="param">需要转换的文本</param>
        /// <returns>转换结果</returns>
        public static T ToEnum<T>(this string param)
        {
            return param.ToEnum(default(T));
        }

        /// <summary>
        /// 将某值转为对应的枚举的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static int ToEnum(this string value, Type enumType)
        {
            return (int)Enum.Parse(enumType, value, true);
        }

        /// <summary>
        /// 将字符串转换为 枚举
        /// <remarks>
        /// 如果是数字字符串时，枚举转换失败时输出原输入的字符串。
        /// 如果是非纯数字字符串时，有默认值时输出默认值，无时输出枚举中的第一项。
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">目标枚举类型</typeparam>
        /// <param name="param">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static T ToEnum<T>(this string param, T defaultValue)
        {
            T result;
            try
            {
                result = (T)Enum.Parse(typeof(T), param, true);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 将字符串转换为 枚举
        /// <remarks>
        /// 当mustDefined==False时:
        /// 如果是数字字符串时，枚举转换失败时输出原输入的字符串。
        /// 如果是非纯数字字符串时，有默认值时输出默认值，无时输出枚举中的第一项。
        /// 如果是多个枚举值用，号隔开，那么只返回最后一个枚举值,非Flags时(Flags并且成员已经赋值1，2，4，8....时返回所有值)
        /// http://msdn.microsoft.com/zh-cn/library/vstudio/kxydatf9.aspx
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">目标枚举类型</typeparam>
        /// <param name="param">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="mustDefined">是否必须为内部定义成员</param>
        /// <returns>转换结果</returns>
        public static T ToEnum<T>(this string param, T defaultValue, bool mustDefined)
        {
            T result;
            try
            {
                result = (T)Enum.Parse(typeof(T), param, true);
                if (mustDefined)
                {
                    var isDefined = Enum.IsDefined(typeof(T), result);
                    return isDefined ? result : defaultValue;
                }
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }



        #endregion



    }
}