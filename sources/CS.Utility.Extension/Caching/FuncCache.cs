using System;
using System.Collections.Concurrent;
using System.Linq;
using CS.Diagnostics;

namespace CS.Caching
{
    /// <summary>
    ///   FuncCache 针对缓存值方法调用的缓存
    /// <remarks>
    /// 过期同步或异步更新数据
    /// </remarks>
    /// </summary>
    /// 
    /// <description class = "CS.Caching.FuncCache">
    ///   1. 被动模式下会先耗光缓存设定的数量再进行移去过期检查。该模式下以创建时间为过期判断时间点。
    ///   2. 主动模式下每次获取缓存内容时都全面检查。该模式下以最后访问时间为过期判断时间点。
    ///   <para>Note:主动模式按缓存创建时间过期，被动模式按缓存访问时间过期（经常使用的缓存，很难过期）。</para>
    /// </description>
    /// 
    /// <history>
    ///   2010-3-22 14:23:37 , atwind ,  创建	
    ///   2010-6-24 atwind 加入缓存依赖项，可以依赖一个具体的文件，缺点，文件里有关联时并不能依赖关联。   
    ///   2013-9-22 atwind 移除System.Web 空间依赖，将Dependency改为Funtion来执行获取缓存项
    ///   2013-9-24 atwind 修改原Dictionary为ConcurrentDictionary
    ///   2015-08-04 atwind 精简处理修改设计
    ///  </history>
    public class FuncCache<TKey, TValue>
    {

        #region 属性

       
       


        #endregion


        /// <summary>
        /// 初始化构造
        /// </summary>
        /// <param name="lifeTime">生命周期，秒</param>
        /// <param name="maxCache">最大缓存数量</param>
        /// <param name="expireMode">过期模式</param>

        internal FuncCache(int lifeTime, int maxCache, CacheExpireMode expireMode)
        {
            LifeTime = lifeTime;
            CacheName = "FuncCache_";
            CacheDic = new ConcurrentDictionary<TKey, CacheBag<TKey, TValue>>();
            ExpireMode = expireMode;
        }

     

        

        /// <summary>
        /// 把对象按指定的键值保存到缓存中
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">保存的对象</param>
        /// <param name="function">缓存的依赖项</param>
        public virtual void Add(TKey key, TValue value, Func<TValue> function)
        {
            if (function == null) throw new ArgumentException("Invoke function delegate is indispensable.", "function");
            SetCache(key, value, function);
        }

        /// <summary>
        /// 把对象按指定的键值保存到缓存中
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">保存的对象</param>
        /// <param name="function">缓存的依赖项</param>
        protected virtual void SetCache(TKey key, TValue value, Func<TValue> function)
        {
            CacheDic[key] = new CacheBag<TKey, TValue>(key, value, function);
        }


        
      
      

        

    }

    
}