using System.Collections.Generic;
using System.Threading;

namespace CS.Caching
{
    /// <summary>
    /// 线程安全的TinyCache
    /// <remarks>注：该缓存无缓存策略</remarks>
    /// </summary>
    /// <typeparam name="T">缓存内容</typeparam>
    public class TinyCache<T>
    {
        /// <summary>
        /// 缓存读写锁
        /// </summary>
        protected ReaderWriterLockSlim CacheLock = new ReaderWriterLockSlim(); // mutex 
        /// <summary>
        /// 内部缓存项
        /// </summary>
        protected Dictionary<string, T> InnerCache = new Dictionary<string, T>();  // the cache itself

        /// <summary>
        /// 无时增加有则替换
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void Add(string key, T val)
        {
            CacheLock.EnterWriteLock();
            try
            {
                InnerCache[key] = val;
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 直接通过索引的访问
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T this[string key]
        {
            get { return Get(key); }
        }

        /// <summary>
        /// 所有的缓存键
        /// </summary>
        public Dictionary<string,T>.KeyCollection Keys
        {
            get { return InnerCache.Keys; }
        }

        /// <summary>
        /// 找不到缓存时返回null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(string key)
        {
            CacheLock.EnterReadLock();
            try
            {
                T val;
                InnerCache.TryGetValue(key, out val);
                return val;
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public void Delete(string key)
        {
            CacheLock.EnterWriteLock();
            try
            {
                InnerCache.Remove(key);
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public void Clear()
        {
            CacheLock.EnterWriteLock();
            try
            {
                InnerCache.Clear();
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 判断缓存的存在性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exist(string key)
        {
            CacheLock.EnterReadLock();
            try
            {
                return InnerCache.ContainsKey(key);
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 缓存数量
        /// </summary>
        public int Count
        {
            get
            {
                CacheLock.EnterReadLock();
                try
                {
                    return InnerCache.Count;
                }
                finally
                {
                    CacheLock.ExitReadLock();
                }
            }
        }

    }
}