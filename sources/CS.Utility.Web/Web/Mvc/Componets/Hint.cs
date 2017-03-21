using CS.Components;
using CS.Extension;

namespace CS.Web.Mvc.Componets
{
    /// <summary>
    /// MVC用到的相关提示
    /// </summary>
    public class Hint
    {
        public Hint() : this("null", "null")
        {
        }

        /// <summary>
        /// 非预期操作+内容
        /// </summary>
        /// <param name="content"></param>
        public Hint(string content) : this("", content)
        {
        }

        public Hint(string title, string content)
        {
            Title = title;
            Content = content;
            Solution = "";
        }

        public bool Result { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 如何解决
        /// </summary>
        public string Solution { get; set; }
        /// <summary>
        /// 调试
        /// </summary>
        public string Debug { get; set; }


        //public static explicit operator  AjaxMessage(Hint hint)
        //{
        //    var msg = new AjaxMessage
        //    {
        //        Message = hint.Content
        //    };
        //    return msg;
        //}

    }

    public static class HintExt
    {
        /// <summary>
        /// 转为AjaxMessage对象
        /// </summary>
        /// <param name="hint"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AjaxMessage ToAjaxMessage(this Hint hint, AjaxMessageType type)
        {
            var ajax = new AjaxMessage(type, hint.Content);
            return ajax;
        }
        /// <summary>
        /// 转为Json字符串
        /// </summary>
        /// <param name="hint"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToMessage(this Hint hint, AjaxMessageType type)
        {
            var ajax = new AjaxMessage(type, hint.Content);
            return ajax.ToJsonByJc();
        }
    }


}