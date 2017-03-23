using System;
using System.Reflection;
using System.Web.Mvc;
using CS.Attribute;
using CS.Extension;

namespace CS.Html
{
    public class TextAreaRender : HtmlElementRender
    {
        public override string ToHtml()
        {
            //return $"<input type=\"text\"  {Item.Item2.Name.ToHtmlProperty("name")}  {Item.Item1.Description.ToHtmlProperty("placeholder")}  {DefaultValue.ToString().ToHtmlProperty("value")}  {ClassNames.ToHtmlProperty("class")} />";

            return $"<textarea {Item.Item2.Name.ToHtmlProperty("name")}  {Item.Item1.Description.ToHtmlProperty("placeholder")}  {ClassNames.ToHtmlProperty("class")}  rows=\"6\">{DefaultValue}</textarea>";

        }

       

        public TextAreaRender(Tuple<HtmlExtAttribute, PropertyInfo> item, object defaultValue = null, string classNames = null) : base(item, defaultValue, classNames)
        {
        }
    }
}