using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CS.Utils
{
    public class DebugHelper
    {
        /// <summary>
        /// 测试处理一批数据的性能结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">处理给出集合的方法</param>
        /// <param name="list">待处理的对象集合</param>
        /// <param name="stopwatch"></param>
        public static void Watcher<T>(Func<IEnumerable<T>, int> func, IEnumerable<T> list, Stopwatch stopwatch = null)
        {
            if (stopwatch == null) stopwatch = new Stopwatch();
            stopwatch.Reset();
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss ffffff}] {func.Method.Name} BEGIN =====================================================");
            stopwatch.Start();
            var val = func(list);
            stopwatch.Stop();
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss ffffff}] {func.Method.Name}  [{stopwatch.Elapsed}  IN<{list.Count()}> -> RS<{val}>]  END====");
        }
    }
}