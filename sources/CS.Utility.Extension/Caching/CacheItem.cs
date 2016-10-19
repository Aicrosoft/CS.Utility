using System;
using CS.Extension;

namespace CS.Caching
{
    /// <summary>
    /// 缓存项基本定义
    /// </summary>
    public  class CacheItem<TV>
    {
        /// <summary>
        /// 值
        /// </summary>
        public virtual TV Item { get; set; }

        /// <summary>
        /// 过期结束时间 秒
        /// <remarks>
        /// 从1970, 1, 1, 0, 0, 0, 0 开始算起
        /// </remarks>
        /// </summary>
        public virtual int ExpiresTime { get; set; }

        /// <summary>
        /// 缓存内容是否已经过期
        /// </summary>
        public virtual bool Expired => ExpiresTime < DateTime.Now.ToSecondTime();

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="expiresIn">从即时开始的有效时间 ，秒</param>
        public virtual void SetExpiresTime(int expiresIn)
        {
            ExpiresTime = DateTime.Now.ToSecondTime() + expiresIn -1;
        }

    }

}