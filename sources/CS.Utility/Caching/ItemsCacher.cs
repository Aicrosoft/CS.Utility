using System;

namespace CS.Caching
{
    /// <summary>
    /// 可枚举集合的缓存类
    /// <remarks>
    /// 实例化该类时为静态对像，否则无法缓存
    /// </remarks>
    /// </summary>
    public class ItemsCacher<T>
    {
        public ItemsCacher(Func<string, T[]> func)
        {
            _key = $"CacheKEY:{typeof(T).FullName}";
            _func = func;
        }

        private readonly Func<string, T[]> _func;
        private readonly string _key;
        private TinyCache<T[]> _cache;

        /// <summary>
        /// 缓存项集合
        /// </summary>
        public T[] Items
        {
            get
            {
                if (_cache == null) _cache = new TinyCache<T[]>();
                return _cache.Get(_key, _func);
            }
        }
        /// <summary>
        /// 清除缓存项
        /// </summary>
        public void Clear()
        {
            _cache?.Delete(_key);
        }
        /// <summary>
        /// 缓存项数量
        /// </summary>
        public int Count => Items.Length;
    }
}