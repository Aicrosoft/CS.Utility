using System;
using System.Web;
using System.Web.Mvc;
using CS.Extension;

namespace CS.Web.Mvc
{

    /// <summary>
    /// 通过Newton.soft来序列化
    /// </summary>
    public class JsonResultByJs : JsonResult
    {
        /// <summary>
        /// 重写执行视图
        /// </summary>
        /// <param name="context">上下文</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //string jsonString = jss.Serialize(Data);
                //string p = @"\\/Date\((\d+)\)\\/";
                //MatchEvaluator matchEvaluator = new MatchEvaluator(this.ConvertJsonDateToDateString);
                //Regex reg = new Regex(p);
                //jsonString = reg.Replace(jsonString, matchEvaluator);
                var jsonString = Data.ToJsonByJc();
                response.Write(jsonString);
            }
        }

    }
}