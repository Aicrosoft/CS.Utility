using System;
using System.Reflection;
using System.Web.Mvc;
using CS.Attribute;
using CS.Extension;

namespace CS.Html
{
    public class PasswordRender : HtmlElementRender
    {
        public override string ToHtml()
        {
            return $"<input type=\"password\"  {Item.Item2.Name.ToHtmlProperty("name")}  {Item.Item1.Description.ToHtmlProperty("placeholder")}  {DefaultValue.ToString().ToHtmlProperty("value")}  {ClassNames.ToHtmlProperty("class")} />";
        }

       

        public PasswordRender(Tuple<HtmlExtAttribute, PropertyInfo> item, object defaultValue = null, string classNames = null) : base(item, defaultValue, classNames)
        {
        }
    }
}