using System.Collections.Generic;
using System.Linq;

namespace CS.Extension
{
    /// <summary>
    /// 可枚举集合扩展
    /// </summary>
    public static class EnumerableExt
    {

        /// <summary>
        /// 带连接字符串的ToString
        /// </summary>
        /// <param name="source"></param>
        /// <param name="split">默认值为 ; </param>
        /// <returns></returns>
        public static string ToString(this IEnumerable<string> source, string split = ";")
        {
            return string.Join(split, source);
        }


        /// <summary>
        /// 是否不为null且有元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool HasAny<T>(this IEnumerable<T> items)
        {
            return items != null && items.Any();
        }

        /// <summary>
        /// Hash分组法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> DistByHash<T>(this IEnumerable<T> list,int count)
        {
            var result = list.GroupBy(i => i.GetHashCode() % count).Select(g => g.ToList());
            return result;
        }

        /// <summary>
        /// 尽量平均分组
        /// <remarks>有可能会小于期待的分组数量</remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count">期待的分组数量</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Dist<T>(this IEnumerable<T> list, int count)
        {
            var dic = new Dictionary<int, T>(list.Count());
            var index = 0;
            foreach (var item in list)
            {
                dic[index] = item;
                index++;
            }
            var result = dic.GroupBy(x => x.Key % count).Select(g => g.Select(x => x.Value));
            return result;
        }
    }
}