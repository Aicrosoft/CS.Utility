using System.Collections.Generic;

namespace CS.Extension
{
    public static class StackExt
    {

        /// <summary>
        /// 将集合转成堆栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Stack<T> ToStack<T>(this IEnumerable<T> values)
        {
            var stack = new Stack<T>();
            foreach (var value in values)
            {
                stack.Push(value);
            }
            return stack;
        }

    }
}