using System.Web.Routing;

namespace CS.Web.Mvc.Extension
{
    public static class RouteDataExt
    {
        /// <summary>
        /// 获取控制器名称
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetControllerName(this RouteData ext)
        {
            return ext.GetRouteValue("controller");
        }

        /// <summary>
        /// 获取Action名称
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetActionName(this RouteData ext)
        {
            return ext.GetRouteValue("action");
        }

        /// <summary>
        /// 获取area名称
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetAreaName(this RouteData ext)
        {
            return ext.GetRouteToken("area");
        }

        /// <summary>
        /// 获了路由路径中的某一值
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetRouteValue(this RouteData ext, string name)
        {
            var vs = ext.Values[name];
            return vs?.ToString();
        }

        public static string GetRouteToken(this RouteData ext, string name)
        {
            var vs = ext.DataTokens[name];
            return vs?.ToString();
        }
    }
}