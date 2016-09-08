using System;
using System.Net;
using System.Text;

namespace CS.Http
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class HttpBase
    {
        /// <summary>
        /// 请求超时30秒
        /// </summary>
        public int RequestTimeout { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// 域
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 接受的类型
        /// </summary>
        public string Accept { get; set; }
        /// <summary>
        /// Cookie字符串
        /// </summary>
        public string CookieString { get; set; }

        /// <summary>
        /// 只读，返回当前请求的URL
        /// </summary>
        public string RequestUrl { get; protected set; }

        private Encoding _encoding;
        /// <summary>
        /// 当前请求编码
        /// </summary>
        public Encoding CurrEncoding
        {
            get { return _encoding ?? (_encoding = Encoding.UTF8); }
            set { _encoding = value; }
        }

        /// <summary>
        /// Http返回状态，只读
        /// </summary>
        public HttpStatusCode StatusCode { get; protected set; }


        /// <summary>
        /// 构造一个请求，并初始化相关信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        protected HttpWebRequest CreateRequest(string url, HttpMethod method)
        {
            RequestUrl = url;
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest == null)
                throw new OperationCanceledException($"根据URL[{url}]创建HttpWebRequest对象失败。");
            webRequest.ServicePoint.Expect100Continue = false;//true时会询问Server使用愿意接受数据,接收到Server返回的100-continue应答以后, 才把数据POST给Server,http://msdn.microsoft.com/zh-cn/library/system.net.servicepoint.expect100continue(v=vs.80).aspx
            webRequest.Timeout = RequestTimeout;
            webRequest.Method = method.ToString();
            if (!string.IsNullOrEmpty(UserAgent))
                webRequest.UserAgent = UserAgent;
            if (!string.IsNullOrEmpty(Accept))
                webRequest.Accept = Accept;
            if (!string.IsNullOrEmpty(CookieString))
                webRequest.CookieContainer = MakeCookieContainer(CookieString);
            return webRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public CookieContainer MakeCookieContainer(string cookies)
        {
            var cookieContainer = new CookieContainer();
            var arrCookie = cookies.Split(';');
            foreach (string str in arrCookie)
            {
                string[] cookieNameValue = str.Split('=');
                var cookie = new Cookie(cookieNameValue[0].Trim(), cookieNameValue[1].Trim().Replace(",", "%2C")) { Domain = Domain };
                cookieContainer.Add(cookie);
            }
            return cookieContainer;
        }

    }

    #region HttpMethod enum

    /// <summary>
    /// 请求方法（所有方法全为大写）
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// 请求获取Request-URI所标识的资源
        /// </summary>
        GET,
        /// <summary>
        ///  在Request-URI所标识的资源后附加新的数据
        /// </summary>
        POST,
        /// <summary>
        /// 请求获取由Request-URI所标识的资源的响应消息报头
        /// </summary>
        HEAD,
        /// <summary>
        ///  请求服务器存储一个资源，并用Request-URI作为其标识
        /// </summary>
        PUT,
        /// <summary>
        /// 请求服务器删除Request-URI所标识的资源
        /// </summary>
        DELETE,
        /// <summary>
        /// 请求服务器回送收到的请求信息，主要用于测试或诊断
        /// </summary>
        TRACE,
        /// <summary>
        /// 保留将来使用
        /// </summary>
        CONNECT,
        /// <summary>
        /// 请求查询服务器的性能，或者查询与资源相关的选项和需求
        /// </summary>
        OPTIONS,
    }

    #endregion


}