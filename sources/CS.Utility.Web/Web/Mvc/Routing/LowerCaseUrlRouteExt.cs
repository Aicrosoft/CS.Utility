using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace CS.Web.Mvc.Routing
{
    /// <summary>
    /// 针对小写URL的扩展
    /// </summary>
    public static class LowerCaseUrlRouteExt
    {
        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url)
        {
            return routes.MapLowerCaseUrlRoute(name, url, null, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url,
            object defaults)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url,
            string[] namespaces)
        {
            return routes.MapLowerCaseUrlRoute(name, url, null, null, namespaces);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url,
            object defaults, object constraints)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, constraints, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url,
            object defaults, string[] namespaces)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, null, namespaces);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url,
            object defaults, object constraints, string[] namespaces)
        {
            if (routes == null) throw new ArgumentNullException(nameof(routes));
            if (url == null) throw new ArgumentNullException(nameof(url));
            var route2 = new LowerCaseUrlRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };
            var item = route2;
            if ((namespaces != null) && (namespaces.Length > 0))
                item.DataTokens["Namespaces"] = namespaces;
            routes.Add(name, item);
            return item;
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name,
            string url)
        {
            return context.MapLowerCaseUrlRoute(name, url, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name,
            string url, object defaults)
        {
            return context.MapLowerCaseUrlRoute(name, url, defaults, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name,
            string url, string[] namespaces)
        {
            return context.MapLowerCaseUrlRoute(name, url, null, namespaces);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name,
            string url, object defaults, object constraints)
        {
            return context.MapLowerCaseUrlRoute(name, url, defaults, constraints, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name,
            string url, object defaults, string[] namespaces)
        {
            return context.MapLowerCaseUrlRoute(name, url, defaults, null, namespaces);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name,
            string url, object defaults, object constraints, string[] namespaces)
        {
            if ((namespaces == null) && (context.Namespaces != null))
                namespaces = context.Namespaces.ToArray();
            var route = context.Routes.MapLowerCaseUrlRoute(name, url, defaults, constraints, namespaces);
            route.DataTokens["area"] = context.AreaName;
            var flag = (namespaces == null) || (namespaces.Length == 0);
            route.DataTokens["UseNamespaceFallback"] = flag;
            return route;
        }
    }
}