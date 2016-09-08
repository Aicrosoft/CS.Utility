using System;

namespace CS.Extension
{
    public static class ConvertExt
    {

        #region T  Convert<T>(string)

        /// <summary>
        /// 将字符串转换为 目标类型 (有装箱拆箱的损失)
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="p">需要转换的文本</param>
        /// <returns>转换结果</returns>
        public static T Convert<T>(this string p)
        {
            T result;
            switch (Type.GetTypeCode(typeof(T)))
            {
                //case TypeCode.Byte:
                //    result = (T)(object)Byte(p);
                //    break;
                //case TypeCode.Int32:
                //    result = (T)(object)Int(p);
                //    break;
                default:
                    result = default(T);
                    break;
            }
            return result;
        }

        #endregion

    }
}