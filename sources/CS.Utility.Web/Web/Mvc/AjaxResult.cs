using System;
using System.Web.Mvc;
using CS.Extension;

namespace CS.Web.Mvc
{
    /// <summary>
    /// Ajax专用的JsonResult
    /// </summary>
    public class AjaxResult : JsonResult
    {
        /// <summary>
        /// 是否将首字母小写
        /// </summary>
        public bool IsCamelCaseJson { get; set; }  

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                var json = IsCamelCaseJson ? Data.ToCamelCaseJsonByJc() : Data.ToJsonByJc();
                response.Write(json);
            }
        }
    }
}