using System.Web.Mvc;

namespace CS.Web.Mvc.Extension
{
    public static class HtmlHelperExt
    {

        /// <summary>
        /// 属性增加
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="express"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MvcHtmlString SetProperty(this HtmlHelper helper, bool express, string key, string value)
        {
            return express && !string.IsNullOrWhiteSpace(value) ? new MvcHtmlString($" {key}=\"{value}\"") : null;
        }

        /// <summary>
        /// 设置一个class属性的键值对
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MvcHtmlString SetClass(this HtmlHelper helper, string value)
        {
            return helper.SetProperty(true, "class", value);
        }

        /// <summary>
        /// 设置一个class属性的键值对
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="express"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MvcHtmlString SetClass(this HtmlHelper helper, bool express, string value)
        {
            return helper.SetProperty(express, "class", value);
        }


        /// <summary>
        /// 增加css名字的引用
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="classNames"></param>
        /// <returns></returns>
        public static MvcHtmlString ToClassNames(this HtmlHelper helper, string classNames = null)
        {
            classNames = string.IsNullOrWhiteSpace(classNames) ? helper.ViewBag.BodyCssNames : classNames;
            return helper.SetProperty(true, "class", classNames);
            //string.IsNullOrWhiteSpace(classNames) ? null : new MvcHtmlString($" class=\"{classNames}\"");
        }

        /// <summary>
        /// 获了路由路径中的某一值
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetRouteData(this HtmlHelper html, string name)
        {
            return html.ViewContext.RouteData.GetRouteValue(name);
        }


        /// <summary>
        /// 获取控制器名称
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetControllerName(this HtmlHelper html)
        {
            return html.ViewContext.RouteData.GetControllerName();
        }

        /// <summary>
        /// 获取Action名称
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetActionName(this HtmlHelper html)
        {
            return html.ViewContext.RouteData.GetActionName();
        }

        /// <summary>
        /// 获取area名称
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetAreaName(this HtmlHelper html)
        {
            return html.ViewContext.RouteData.GetAreaName();
        }

    }
}