using System;
using System.Globalization;
using System.Web.Routing;

namespace CS.Web.Mvc.Routing
{
    /// <summary>
    ///     将路由URL转为小写
    /// </summary>
    public class LowerCaseUrlRoute : Route
    {
        private static readonly string[] RequiredKeys = {"area", "controller", "action"};
        

        public LowerCaseUrlRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        public LowerCaseUrlRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        public LowerCaseUrlRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
            IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        public LowerCaseUrlRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
            RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        //TODO:加一个锁，先看看能不能避免同时提交不同页面发生的不能修改集合异常，虽然异常没有了，但是结果会出错，可能显示到了意想不到的页面
        private readonly object _locker = new object();
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            lock (_locker)
            {
                LowerRouteValues(requestContext.RouteData.Values);
                LowerRouteValues(values);
                LowerRouteValues(Defaults);
                return base.GetVirtualPath(requestContext, values);
            }
        }

        private static void LowerRouteValues(RouteValueDictionary values)
        {
            foreach (var key in RequiredKeys)
            {
                if (values.ContainsKey(key) == false) continue;

                var value = values[key];
                if (value == null) continue;

                var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                if (string.IsNullOrEmpty(valueString)) continue;
                values[key] = valueString.ToLower();
            }

            ////不小写Key，或则可能有些Key小写了后获取不到值。
            //var otherKyes = values.Keys
            //    .Except(RequiredKeys, StringComparer.InvariantCultureIgnoreCase)
            //    .ToArray();
            //foreach (var key in otherKyes)
            //{
            //    var value = values[key];
            //    values.Remove(key);
            //    values.Add(key.ToLower(), value);  
            //}
        }
    }
}