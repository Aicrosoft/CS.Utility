using System;
using System.Reflection;
using System.Web.Mvc;
using CS.Attribute;

namespace CS.Html
{
    public class UnknowRender : HtmlElementRender
    {
        public UnknowRender(Tuple<HtmlExtAttribute, PropertyInfo> item, object defaultValue = null, string classNames = null) : base(item, defaultValue, classNames)
        {
        }

        public override string ToHtml()
        {
            return "None";
        }
     
    }
}