using System;
using System.Reflection;
using System.Web.Mvc;
using CS.Attribute;

namespace CS.Html
{
    public abstract class HtmlElementRender
    {
        public static HtmlElementRender Create(Tuple<HtmlExtAttribute, PropertyInfo> item, object defaultValue = null, string classNames = null)
        {
            switch (item.Item1.Element)
            {
                case HtmlElementType.InputText:
                    return new InputTextRender(item,defaultValue,classNames);
                case HtmlElementType.TextArea:
                    return new TextAreaRender(item, defaultValue, classNames);
                case HtmlElementType.Password:
                    return new PasswordRender(item, defaultValue, classNames);
                case HtmlElementType.InputNumber:
                    return new InputNumberRender(item, defaultValue, classNames);
                case HtmlElementType.Label:
                    return new LabelRender(item, defaultValue, classNames);

                default:
                    return new UnknowRender(item, defaultValue, classNames);
            }
        }

        protected HtmlElementRender(Tuple<HtmlExtAttribute, PropertyInfo> item, object defaultValue = null,
            string classNames = null)
        {
            Item = item;
            DefaultValue = defaultValue;
            ClassNames = classNames;
        }

        protected Tuple<HtmlExtAttribute, PropertyInfo> Item { get; set; }

        protected object DefaultValue { get; set; }

        protected string ClassNames { get; set; }

        /// <summary>
        /// 转为普通的HTML代码
        /// </summary>
        /// <returns></returns>
        public abstract string ToHtml();

        /// <summary>
        /// 转为Razor引擎下的CSHTML模板里的HTML代码
        /// </summary>
        /// <returns></returns>
        public virtual MvcHtmlString ToMvcHtml()
        {
            return new MvcHtmlString(ToHtml());
        }
    }

    /// <summary>
    /// HTML元素类型
    /// </summary>
    public enum HtmlElementType
    {

        
        None,

        Label,


        InputText,

        InputNumber,

        Password,

        TextArea,

        CheckBox,
    }



}