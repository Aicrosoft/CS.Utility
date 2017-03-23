using System.Collections.Generic;
using System.Linq;

namespace CS.Extension
{
    public static class QueueExt
    {

        /// <summary>
        /// 将集合转成队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Queue<T> ToQueue<T>(this IEnumerable<T> values)
        {
            var queue = new Queue<T>();
            foreach (var value in values)
            {
                queue.Enqueue(value);
            }
            return queue;
        }

   

    }
}