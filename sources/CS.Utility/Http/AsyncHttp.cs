using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace CS.Http
{
    /// <summary>
    /// 异步请求委托
    /// </summary>
    /// <param name="asyncHttp"></param>
    /// <param name="content"></param>
    public delegate void AsyncHttpCallback(AsyncHttp asyncHttp, string content);

    /// <summary>
    /// 请求状态
    /// </summary>
    internal class RequestState
    {
        public const int BUFFER_SIZE = 1024;
        public byte[] BufferRead;
        public AsyncHttpCallback AsyncCallBack;

        public HttpWebRequest Request;
        public StringBuilder RequestData;
        public HttpWebResponse Response;
        public Stream StreamResponse;

        public RequestState()
        {
            BufferRead = new byte[BUFFER_SIZE];
            RequestData = new StringBuilder("");
            Request = null;
            StreamResponse = null;
            AsyncCallBack = null;
        }
    }

    /// <summary>
    /// 异步Http请求
    /// </summary>
    public class AsyncHttp:HttpBase
    {
        /// <summary>
        /// 完成事件
        /// </summary>
        public ManualResetEvent AllDone = new ManualResetEvent(false);

        /// <summary>
        /// 默认构造，30秒超时
        /// </summary>
        public AsyncHttp()
            : this(30000)
        {
        }

        /// <summary>
        /// 超时构造 ，毫秒
        /// </summary>
        /// <param name="timeout"></param>
        public AsyncHttp(int timeout)
        {
            RequestTimeout = timeout;
        }


        /// <summary>
        /// 异步方式发起http get请求
        /// </summary>
        /// <param name="url">包含QueryString的URL</param>
        /// <param name="callback">结束后的回调</param>
        /// <returns>请求是否发出，异常时返回false</returns>
        public bool HttpGet(string url, AsyncHttpCallback callback)
        {
            var webRequest = CreateRequest(url, HttpMethod.GET);
            try
            {
                var state = new RequestState { AsyncCallBack = callback, Request = webRequest };
                IAsyncResult result = webRequest.BeginGetResponse(ResponseCallback, state);
                // this line implements the timeout, if there is a timeout, the callback fires and the request becomes aborted
                ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, TimeoutCallback, webRequest, RequestTimeout, true);
                // The response came in the allowed time. The work processing will happen in the callback function.
                AllDone.WaitOne();
                // Release the HttpWebResponse resource.
                state.Response.Close();
            }
            catch
            {
                return false;
                //throw;
            }

            return true;
        }

        /// <summary>
        /// 异步方式发起http post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public bool HttpPost(string url, string postData, AsyncHttpCallback callback)
        {
            StreamWriter requestWriter = null;
            //StreamReader responseReader = null;
            //string responseData = null;

            var webRequest = CreateRequest(url, HttpMethod.POST);
            webRequest.ContentType = "application/x-www-form-urlencoded";

            try
            {
                //POST the data.
                requestWriter = new StreamWriter(webRequest.GetRequestStream());
                requestWriter.Write(postData);
                requestWriter.Close();
                requestWriter = null;

                var state = new RequestState { AsyncCallBack = callback, Request = webRequest };

                IAsyncResult result = webRequest.BeginGetResponse(ResponseCallback, state);

                // this line implements the timeout, if there is a timeout, the callback fires and the request becomes aborted
                ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, TimeoutCallback, webRequest,
                                                       RequestTimeout, true);

                // The response came in the allowed time. The work processing will happen in the 
                // callback function.
                AllDone.WaitOne();

                // Release the HttpWebResponse resource.
                state.Response.Close();
            }
            catch
            {
                return false;
                //throw;
            }
            finally
            {
                if (requestWriter != null)
                    requestWriter.Close();
            }

            return true;
        }

        private void ResponseCallback(IAsyncResult asynchronousResult)
        {
            // State of request is asynchronous.
            var state = (RequestState)asynchronousResult.AsyncState;

            try
            {
                HttpWebRequest webRequest = state.Request;
                state.Response = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);

                StatusCode = state.Response.StatusCode;

                // Read the response into a Stream object.
                Stream responseStream = state.Response.GetResponseStream();
                state.StreamResponse = responseStream;

                IAsyncResult asynchronousInputRead = responseStream.BeginRead(state.BufferRead, 0,
                                                                              RequestState.BUFFER_SIZE, ReadCallBack,
                                                                              state);

                return;
            }
            catch
            {
                //fire back
                FireCallback(state);
            }

            AllDone.Set();
        }

        private void ReadCallBack(IAsyncResult asyncResult)
        {
            var state = (RequestState)asyncResult.AsyncState;
            Stream responseStream = state.StreamResponse;

            try
            {
                int read = responseStream.EndRead(asyncResult);

                if (read > 0)
                {
                    state.RequestData.Append(Encoding.UTF8.GetString(state.BufferRead, 0, read));
                    IAsyncResult asynchronousResult = responseStream.BeginRead(state.BufferRead, 0,
                                                                               RequestState.BUFFER_SIZE, ReadCallBack,
                                                                               state);

                    return;
                }
                //fire back
                FireCallback(state);
                responseStream.Close();
            }
            catch
            {
                //fire back
                FireCallback(state);
                responseStream.Close();
            }

            AllDone.Set();
        }

        // Abort the request if the timer fires.
        private static void TimeoutCallback(object state, bool timedOut)
        {
            if (!timedOut) return;
            var request = state as HttpWebRequest;
            if (request != null)
            {
                request.Abort();
            }
        }

        private void FireCallback(RequestState state)
        {
            //call back
            if (state.AsyncCallBack != null)
            {
                state.AsyncCallBack(this, state.RequestData.ToString());
            }
        }

        #region obsoled code
        ///// <summary>
        ///// 异步方式发起http post请求，可以同时上传文件
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="queryString"></param>
        ///// <param name="files"></param>
        ///// <param name="callback"></param>
        ///// <returns></returns>
        //public bool HttpPostWithFile(string url, string queryString, List<UrlParameter> files,
        //                             AsyncHttpCallback callback)
        //{
        //    Stream requestStream = null;
        //    string boundary = DateTime.Now.Ticks.ToString("x");

        //    //url += '?' + queryString;    //Note:Form里已有，这儿不是必须的
        //    var webRequest = WebRequest.Create(url) as HttpWebRequest;
        //    if (webRequest == null)
        //        throw new OperationCanceledException(string.Format("根据URL[{0}]创建HttpWebRequest对象失败。", url));
        //    webRequest.ServicePoint.Expect100Continue = true;
        //    webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
        //    webRequest.Method = "POST";
        //    webRequest.KeepAlive = true;
        //    webRequest.Credentials = CredentialCache.DefaultCredentials;

        //    try
        //    {
        //        Stream memStream = new MemoryStream();

        //        byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
        //        string formdataTemplate = "\r\n--" + boundary +
        //                                  "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

        //        List<UrlParameter> listParams = HttpRequestUtil.GetQueryParameters(queryString);

        //        foreach (UrlParameter param in listParams)
        //        {
        //            //需要将
        //            string formitem = string.Format(formdataTemplate, param.Name,
        //                                            (HttpRequestUtil.DecodeFormParam(param.Value)));
        //            byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
        //            memStream.Write(formitembytes, 0, formitembytes.Length);
        //        }

        //        memStream.Write(boundarybytes, 0, boundarybytes.Length);

        //        string headerTemplate =
        //            "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: \"{2}\"\r\n\r\n";

        //        foreach (UrlParameter param in files)
        //        {
        //            string name = param.Name;
        //            string filePath = param.Value;
        //            string file = Path.GetFileName(filePath);
        //            string contentType = HttpRequestUtil.GetContentType(file);

        //            string header = string.Format(headerTemplate, name, file, contentType);
        //            byte[] headerbytes = Encoding.UTF8.GetBytes(header);

        //            memStream.Write(headerbytes, 0, headerbytes.Length);

        //            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //            var buffer = new byte[1024];
        //            int bytesRead = 0;

        //            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        //            {
        //                memStream.Write(buffer, 0, bytesRead);
        //            }

        //            memStream.Write(boundarybytes, 0, boundarybytes.Length);
        //            fileStream.Close();
        //        }

        //        webRequest.ContentLength = memStream.Length;

        //        requestStream = webRequest.GetRequestStream();

        //        memStream.Position = 0;
        //        var tempBuffer = new byte[memStream.Length];
        //        memStream.Read(tempBuffer, 0, tempBuffer.Length);
        //        memStream.Close();
        //        requestStream.Write(tempBuffer, 0, tempBuffer.Length);
        //        requestStream.Close();
        //        requestStream = null;

        //        var state = new RequestState();
        //        state.AsyncCallBack = callback;
        //        state.Request = webRequest;

        //        IAsyncResult result = webRequest.BeginGetResponse(ResponseCallback, state);
        //        ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, TimeoutCallback,
        //                                               webRequest, DEFAULT_TIMEOUT, true);
        //        AllDone.WaitOne();

        //        state.Response.Close();
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        if (requestStream != null)
        //            requestStream.Close();
        //    }

        //    return true;
        //}
        #endregion
    }
}