using System;
using System.Reflection;
using System.Web.Mvc;
using CS.Attribute;
using CS.Extension;
using CS.Logging;

namespace CS.Html
{
    public class InputTextRender: HtmlElementRender
    {
        public override string ToHtml()
        {
            //if (Item.Item1 == null || Item.Item2 == null) return "ERROR";
            //Tracer.Trace($"DefaultValue:{DefaultValue};ClassNames:{ClassNames}");
            return $"<input type=\"text\"  {Item.Item2.Name.ToHtmlProperty("name")}  {Item.Item1.Description.ToHtmlProperty("placeholder")}  {DefaultValue?.ToString().ToHtmlProperty("value")}  {ClassNames.ToHtmlProperty("class")} />";
        }
      

        public InputTextRender(Tuple<HtmlExtAttribute, PropertyInfo> item, object defaultValue = null, string classNames = null) : base(item, defaultValue, classNames)
        {
        }
    }
}