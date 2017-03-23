using System;
using System.Web.Mvc;
using CS.Extension;
using CS.Logging;

namespace CS.Web.Mvc
{
    /// <summary>
    /// Ajax专用的JsonResult
    /// </summary>
    public class AjaxResult : JsonResult
    {

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
                response.Write(Data.ToJsonByJc());
            }
        }
    }
}