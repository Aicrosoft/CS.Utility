using System.Text;
using System.Web.Mvc;
using CS.Logging;

namespace CS.Web.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    public  abstract class ControllerBase:Controller
    {
        protected readonly ILog Log;

        protected ControllerBase()
        {
            Log = LogManager.GetLogger(GetType());
        }

        /// <summary>
        /// 直接返回AjaxJson 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult AjaxJson(object data) => Json(data, "application/json", Encoding.UTF8);

        /// <summary>
        /// 使用Json.Net进行序列化的结果输出
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new AjaxResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

    }
}