using System.IO;
using System.Net;

namespace CS.Http
{
    /// <summary>
    /// 同步Http请求
    /// 2011-12-1 自动识别内容编码，默认为UTF-8
    /// </summary>
    public class SyncHttp:HttpBase
    {
        /// <summary>
        /// 默认构造，30秒超时
        /// </summary>
        public SyncHttp()
            : this(30000)
        {
        }

        /// <summary>
        /// 超时构造，毫秒
        /// </summary>
        /// <param name="timeout"></param>
        public SyncHttp(int timeout)
        {
            RequestTimeout = timeout;
        }


        /// <summary>
        /// 同步方式发起http get请求
        /// </summary>
        /// <param name="url">基础Url链接,已包含查询字符串</param>
        /// <returns>Http的Response结果</returns>
        public string HttpGet(string url)
        {
            var webRequest = CreateRequest(url, HttpMethod.GET);
            var response = webRequest.GetResponse() as HttpWebResponse;
            StatusCode = response.StatusCode;

            //var charset = response.CharacterSet;   //页面编码
            //var encoding = string.IsNullOrEmpty(charset) ? Encoding.UTF8 : Encoding.GetEncoding(charset);
            //CurrEncoding = encoding;
            var stream = response.GetResponseStream();
            using (var responseReader = new StreamReader(stream,CurrEncoding))
            {
                string responseData = responseReader.ReadToEnd();
                stream.Close();
                return responseData;
            }
        }

        /// <summary>
        /// 同步方式发起http post请求
        /// </summary>
        /// <param name="url">Url请求链接</param>
        /// <param name="postData">要被Post过去的数据字符串</param>
        /// <returns></returns>
        public string HttpPost(string url, string postData)
        {
            var webRequest = CreateRequest(url, HttpMethod.POST);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            if (!string.IsNullOrEmpty(postData))
            {
                using (var requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(postData);  //将Post数据写入到webRequest对象的RequestStream流中
                    requestWriter.Close();
                }
            }

            //获取服务器返回的流信息
            var response = webRequest.GetResponse() as HttpWebResponse;
            StatusCode = response.StatusCode;

            //var charset = response.CharacterSet;   //页面编码
            //var encoding = string.IsNullOrEmpty(charset) ? Encoding.UTF8 : Encoding.GetEncoding(charset);
            var stream = response.GetResponseStream();
            using (var responseReader = new StreamReader(stream))
            {
                string responseData = responseReader.ReadToEnd();
                stream.Close();
                return responseData;
            }
        }


        #region obsoled code
        ///// <summary>
        ///// 同步方式发起http post请求，可以同时上传文件
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="queryString"></param>
        ///// <param name="files"></param>
        ///// <returns></returns>
        //public string HttpPostWithFiles(string url, string queryString, List<UrlParameter> files)
        //{
        //    Stream requestStream = null;
        //    StreamReader responseReader = null;
        //    string responseData = null;
        //    string boundary = DateTime.Now.Ticks.ToString("x");

        //    //url += '?' + queryString;    //Note:Form里已有，这儿不是必须的
        //    var webRequest = WebRequest.Create(url) as HttpWebRequest;
        //    if (webRequest == null)
        //        throw new OperationCanceledException(string.Format("根据URL[{0}]创建HttpWebRequest对象失败。", url));
        //    webRequest.ServicePoint.Expect100Continue = false;
        //    webRequest.Timeout = RequestTimeout;
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
        //            string formitem = string.Format(formdataTemplate, param.Name, HttpRequestUtil.DecodeFormParam(param.Value));
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

        //        responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
        //        responseData = responseReader.ReadToEnd();
        //    }
        //    finally
        //    {
        //        if (requestStream != null) requestStream.Close();
        //        if (responseReader != null) responseReader.Close();
        //        //webRequest.GetResponse().GetResponseStream().Close();
        //    }

        //    return responseData;
        //}
        #endregion

    }
}