using System.Web;

namespace CS.Utils
{
    public class Guest
    {

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// 获取当前访问路径的基地址目录（不含最后一个 /  的后面的URL）
        /// </summary>
        /// <returns></returns>
        public static string GetBaseUrl(string subPath)
        {
            var request = HttpContext.Current.Request;
            return $"{request.Url.Scheme}://{request.Url.Host}:{request.Url.Port}{subPath}";
        }
    }
}