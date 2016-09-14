using System.Linq;

namespace CS.Extension
{
    /// <summary>
    /// 位运算相关
    /// </summary>
    public static class BitExt
    {
        /// <summary>
        /// 确认val中是否有askVal的一个位域
        /// </summary>
        /// <param name="val"></param>
        /// <param name="askVal"></param>
        /// <returns></returns>
        public static bool HasFlag(this object val, object askVal)
        {
            if (val == null || askVal == null) return false;
            return val.ToInt(0).HasFlag(askVal.ToInt(0));
        }

        /// <summary>
        /// 确认val中是否有askVal的一个位域
        /// </summary>
        /// <param name="val"></param>
        /// <param name="askVal"></param>
        /// <returns></returns>
        public static bool HasFlag(this int val, int askVal)
        {
            return askVal == (val & askVal);
        }

        /// <summary>
        /// 按位与算联合
        /// </summary>
        /// <param name="vals"></param>
        /// <returns></returns>
        public static int CombineByBit(this int[] vals)
        {
            return vals.Aggregate(0, (current, i) => current | i);
        }
    }
}