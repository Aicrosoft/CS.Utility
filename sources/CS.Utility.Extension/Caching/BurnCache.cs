using System;
using System.Threading;

namespace CS.Caching
{
    /// <summary>
    /// 只存活一定时间的缓存，过期后自动删除
    /// <remarks>
    /// 这个缓存内容，只为很大内存使用的缓存来设计的
    /// </remarks>
    /// </summary>
    public class BurnCache<T>
    {
        ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim(); // mutex  



    }
}