using System.Text;
using System.Web;

namespace CS.Extension
{
    public static class UrlExt
    {

        /// <summary>
        ///  采用UTF8 编码URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string EncodeUrl(this string url) => HttpUtility.UrlEncode(url, Encoding.UTF8);

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string EncodeUrl(this string url, Encoding e) => HttpUtility.UrlEncode(url, e);


    }
}