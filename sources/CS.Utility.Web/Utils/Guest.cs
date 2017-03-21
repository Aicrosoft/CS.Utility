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


    }
}