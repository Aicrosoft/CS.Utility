using System;
using System.Reflection;
using System.Web.Mvc;
using CS.Attribute;
using CS.Extension;

namespace CS.Html
{
    public class LabelRender : HtmlElementRender
    {
        public override string ToHtml()
        {
            return $" {DefaultValue}";
        }
      

        public LabelRender(Tuple<HtmlExtAttribute, PropertyInfo> item, object defaultValue = null, string classNames = null) : base(item, defaultValue, classNames)
        {
        }
    }
}