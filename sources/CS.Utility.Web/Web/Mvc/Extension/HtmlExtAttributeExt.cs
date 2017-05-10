using System;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;
using CS.Attribute;
using CS.Html;

namespace CS.Web.Mvc.Extension
{
    public static class HtmlExtAttributeExt
    {
        public static MvcHtmlString ToMvcString(this Tuple<HtmlExtAttribute,PropertyInfo> item,object defaultValue=null,string classNames = null)
        {
            //if (item == null) return null;
            return HtmlElementRender.Create(item, defaultValue, classNames).ToMvcHtml();
        }
    }
}