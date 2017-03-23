using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using CS.Components;
using CS.Logging;
using CS.Validation;

namespace CS.Web.Mvc.Attribute
{
    /// <summary>
    /// WebApi用到的异常捕获
    /// <remarks>
    /// 注意：该类要增加到WebApiConfig中的过滤器中，而不是全局的过滤器中
    /// </remarks>
    /// </summary>
    public class WebApiExceptionAttribute : System.Web.Mvc.FilterAttribute, IExceptionFilter// ExceptionFilterAttribute, IExceptionFilter
    {

        private readonly ILog _log = LogManager.GetLogger(typeof(WebApiExceptionAttribute));

        //public override void OnException(HttpActionExecutedContext actionExecutedContext)
        //{
        //    var guid = Guid.NewGuid().ToString().Replace("-", "");
        //    _log.Error($"全局异常:[{guid}]", actionExecutedContext.Exception);
        //    actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK,
        //        new AjaxMessage()
        //        {
        //            Ext = guid,
        //            Message = actionExecutedContext.Exception.Message
        //        });
        //}

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var guid = Guid.NewGuid().ToString().Replace("-", "");
                var ex = actionExecutedContext.Exception;
                var ajax = new AjaxMessage();
                if (ex is ParameterException)
                {
                    ajax.Tip(false, ex.Message);
                }
                else
                {
                    _log.Error($"全局异常:[{guid}]", ex);
                    ajax.Error("Server internal error...");
                    ajax.Code = -999;
                    ajax.Ext = guid;
#if DEBUG

                    ajax.Ext = $"{ex.Message}\r\n{ex.StackTrace}";
#endif
                }
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, ajax);

            }, cancellationToken);
        }
    }
}