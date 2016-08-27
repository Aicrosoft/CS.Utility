using System.Collections.Generic;
using System.Linq;

namespace CS.Utils
{
    /// <summary>
    /// Hash辅助类
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        /// 取Hash
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static int GetHashCode<T1, T2>(T1 arg1, T2 arg2)
        {
            unchecked
            {
                return 31*arg1.GetHashCode() + arg2.GetHashCode();
            }
        }

        /// <summary>
        /// 取Hash
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <returns></returns>
        public static int GetHashCode<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            unchecked
            {
                int hash = arg1.GetHashCode();
                hash = 31*hash + arg2.GetHashCode();
                return 31*hash + arg3.GetHashCode();
            }
        }

        /// <summary>
        /// 取Hash
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        /// <returns></returns>
        public static int GetHashCode<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3,
            T4 arg4)
        {
            unchecked
            {
                int hash = arg1.GetHashCode();
                hash = 31*hash + arg2.GetHashCode();
                hash = 31*hash + arg3.GetHashCode();
                return 31*hash + arg4.GetHashCode();
            }
        }
        /// <summary>
        /// 取Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int GetHashCode<T>(T[] list)
        {
            unchecked
            {
                return list.Aggregate(0, (current, item) => 31*current + item.GetHashCode());
            }
        }
        /// <summary>
        /// 取Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int GetHashCode<T>(IEnumerable<T> list)
        {
            unchecked
            {
                return list.Aggregate(0, (current, item) => 31*current + item.GetHashCode());
            }
        }

        /// <summary>
        /// Gets a hashcode for a collection for that the order of items 
        /// does not matter.
        /// So {1, 2, 3} and {3, 2, 1} will get same hash code.
        /// </summary>
        public static int GetHashCodeForOrderNoMatterCollection<T>(
            IEnumerable<T> list)
        {
            unchecked
            {
                int hash = 0;
                int count = 0;
                foreach (var item in list)
                {
                    hash += item.GetHashCode();
                    count++;
                }
                return 31*hash + count.GetHashCode();
            }
        }

        /// <summary>
        /// Alternative way to get a hashcode is to use a fluent 
        /// interface like this:<br />
        /// return 0.CombineHashCode(field1).CombineHashCode(field2).
        ///     CombineHashCode(field3);
        /// </summary>
        public static int CombineHashCode<T>(this int hashCode, T arg)
        {
            unchecked
            {
                return 31*hashCode + arg.GetHashCode();
            }
        }
    }
}