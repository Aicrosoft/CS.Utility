using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CS.Web.Mvc.Attribute
{
    /// <summary>
    ///     Newtonsoft.json 的过滤属性
    /// </summary>
    public class JsonNetFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is JsonResult == false)
                return;

            filterContext.Result = new CustomJsonResult((JsonResult) filterContext.Result);
        }

        private class CustomJsonResult : JsonResult
        {
            public CustomJsonResult(JsonResult jsonResult)
            {
                ContentEncoding = jsonResult.ContentEncoding;
                ContentType = jsonResult.ContentType;
                Data = jsonResult.Data;
                JsonRequestBehavior = jsonResult.JsonRequestBehavior;
                MaxJsonLength = jsonResult.MaxJsonLength;
                RecursionLimit = jsonResult.RecursionLimit;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));

                if (JsonRequestBehavior == JsonRequestBehavior.DenyGet
                    && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("GET not allowed! Change JsonRequestBehavior to AllowGet.");

                var response = context.HttpContext.Response;

                response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

                if (ContentEncoding != null)
                    response.ContentEncoding = ContentEncoding;

                if (Data != null)
                {
                    var json = JsonConvert.SerializeObject(
                        Data,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });

                    response.Write(json);
                }
            }
        }
    }
}