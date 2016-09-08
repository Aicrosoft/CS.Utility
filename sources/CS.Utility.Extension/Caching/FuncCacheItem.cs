using System;
using System.Runtime.InteropServices;

namespace CS.Caching
{
    /// <summary>
    /// 自动调用方法更新缓存项
    /// <remarks>
    /// 主要用于API调用，并且缓存一定时间
    /// </remarks>
    /// 
    /// <example>
    /// <code>
    /// <![CDATA[
    /// class TestCacheItem:FuncCacheItem<Json>
    /// {
    ///   //MockService.GetMockJson 返回Json实例的方法
    ///    public TestCacheItem() : base(MockService.GetMockJson, null)
    ///    {
    ///    }
    /// 
    ///    //返回结果时更新过期时间
    ///   protected override void UpdateExpires(Json json)
    ///   {
    ///        Tracer.Debug(json.Token);
    ///       SetExpiresTime(json.ExpiresIn);
    ///   }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    /// </summary>
    public class FuncCacheItem<TV> : CacheItem<TV> where TV : class
    {
        /// <summary>
        /// 初始化缓存项
        /// </summary>
        /// <param name="function"></param>
        /// <param name="value">可能的话给出初始的值</param>
        public FuncCacheItem(Func<TV> function, TV value = null)
        {
            base.Item = value;
            FuncCallback = function;
        }

        /// <summary>
        /// 当值过期时取值的方法
        /// <remarks>
        /// 该方法返回的Item项目要更新过期时间
        /// </remarks>
        /// </summary>
        public Func<TV> FuncCallback { get; set; }

        /// <summary>
        /// 返回值，如有可能自动更新值
        /// </summary>
        public override TV Item
        {
            get
            {
                if (FuncCallback == null)
                    throw new NullReferenceException("Please use set FuncCacheItem.FuncCallback init.");
                if (base.Item != null && !Expired) return base.Item;
                base.Item = FuncCallback.Invoke();
                UpdateExpires(base.Item);
                return base.Item;
            }
        }

        /// <summary>
        /// 更新过期时间
        /// <remarks>对时间有要求的必须重写该方法，以更新过期时间</remarks>
        /// </summary>
        /// <param name="item"></param>
        protected virtual void UpdateExpires(TV item)
        {
        }
    }
}