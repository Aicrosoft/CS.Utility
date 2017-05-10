using System;

namespace CS.Caching
{
    /// <summary>
    /// FuncCache的管理器
    /// </summary>
    public class FuncCacher<TV> where TV : class
    {
        private static readonly TinyCache<FuncCacheItem<TV>> DicCache;

        static FuncCacher()
        {
            DicCache = new TinyCache<FuncCacheItem<TV>>();
        }

        private FuncCacher()
        {
        }

        /// <summary>
        /// 注册一个新的缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function"></param>
        /// <param name="value"></param>
        public static void Register(string key,Func<TV> function,TV value = null)
        {
            var item = new FuncCacheItem<TV>(function, value);
            DicCache.Add(key,item);
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TV GetValue(string key)
        {
            var item = DicCache[key];


            return null;
        }

    }


    
}