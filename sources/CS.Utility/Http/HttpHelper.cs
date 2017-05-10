using System.Text;

namespace CS.Http
{
    /// <summary>
    /// Http请求辅助方法
    /// zhouyu,2011-08-11 created.
    /// zhouyu,2011-10-24 add async request method.
    /// </summary>
    public class HttpHelper
    {

        #region 同步处理

        /// <summary>
        /// Get方法请求并返回消息,默认超时 
        /// </summary>
        /// <param name="url">包含查询字符串的Url地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            var sync = new SyncHttp();
            return sync.HttpGet(url);
        }

        /// <summary>
        /// 设定编码的
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Get(string url,Encoding encoding)
        {
            var sync = new SyncHttp {CurrEncoding = encoding};
            return sync.HttpGet(url);
        }

        /// <summary>
        /// Get方法请求并返回消息 ,超时可配
        /// </summary>
        /// <param name="url">包含查询字符串的Url地址</param>
        /// <param name="timeout">超时，豪秒</param>
        /// <returns></returns>
        public static string Get(string url, int timeout)
        {
            var sync = new SyncHttp(timeout);
            return sync.HttpGet(url);
        }

        /// <summary>
        /// Post方法请求并返回消息 ,默认超时
        /// </summary>
        /// <param name="url">包含查询字符串的Url地址</param>
        /// <param name="urlParameters">参数集合</param>
        /// <returns></returns>
        public static string Post(string url, HttpParams urlParameters)
        {
            var sync = new SyncHttp();
            return sync.HttpPost(url, urlParameters);
        }

        /// <summary>
        /// Post方法请求并返回消息 ,可配超时
        /// </summary>
        /// <param name="url">包含查询字符串的Url地址</param>
        /// <param name="urlParameters">参数集合</param>
        /// <param name="timeout">超时，豪秒</param>
        /// <returns></returns>
        public static string Post(string url, HttpParams urlParameters, int timeout )
        {
            var sync = new SyncHttp(timeout);
            return sync.HttpPost(url, urlParameters);
        }

        /// <summary>
        ///  Post方法请求并返回消息 ,可配超时
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string Post(string url, string data, int timeout = 12000)
        {
            var sync = new SyncHttp(timeout);
            return sync.HttpPost(url, data);
        }


        #endregion


        #region 异步处理 Async

        /// <summary>
        /// 异步GET
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static bool AsyncGet(string url, AsyncHttpCallback callback)
        {
            var sync = new AsyncHttp();
            return sync.HttpGet(url, callback);
        }


        /// <summary>
        /// 异步：Get方法请求并返回消息 ,超时可配
        /// </summary>
        /// <param name="url">包含查询字符串的Url地址</param>
        /// <param name="timeout">超时，豪秒</param>
        /// <param name="callback">结束后的回调</param>
        /// <returns></returns>
        public static bool AsyncGet(string url, int timeout, AsyncHttpCallback callback)
        {
            var sync = new AsyncHttp(timeout);
            return sync.HttpGet(url, callback);
        }


        /// <summary>
        /// 异步：Post方法请求并返回消息 ,可配超时
        /// </summary>
        /// <param name="url">包含查询字符串的Url地址</param>
        /// <param name="urlParameters">参数集合</param>
        /// <param name="timeout">超时，豪秒</param>
        /// <param name="callback">结束后的回调</param>
        /// <returns></returns>
        public static bool AsyncPost(string url, HttpParams urlParameters, int timeout, AsyncHttpCallback callback)
        {
            var sync = new AsyncHttp(timeout);
            return sync.HttpPost(url, urlParameters, callback);
        }



        #endregion


    }


}