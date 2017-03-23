using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CS.Logging;

namespace CS.Attribute
{
    /// <summary>
    /// 关于Mvc的全局异常处理
    /// </summary>
    public class MvcErrorAttribute : FilterAttribute, IExceptionFilter
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(MvcErrorAttribute));

        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            if (filterContext.ExceptionHandled == true)
            {
                return;
            }
            //var httpException = new HttpException(null, exception);
            ////filterContext.Exception.Message可获取错误信息
            ///*
            // * 1、根据对应的HTTP错误码跳转到错误页面
            // * 2、这里对HTTP 404/400错误进行捕捉和处理
            // * 3、其他错误默认为HTTP 500服务器错误
            // */
            //if ((httpException.GetHttpCode() == 400 || httpException.GetHttpCode() == 404))
            //{
            //    filterContext.HttpContext.Response.StatusCode = 404;
            //    filterContext.HttpContext.Response.WriteFile("~/HttpError/404.html");
            //}
            //else
            //{
            //    filterContext.HttpContext.Response.StatusCode = 500;
            //    filterContext.HttpContext.Response.WriteFile("~/HttpError/500.html");
            //}
            /*---------------------------------------------------------
             * 这里可进行相关自定义业务处理，比如日志记录等
             ---------------------------------------------------------*/
            _log.Error("未处理的异常", exception);


            //设置异常已经处理,否则会被其他异常过滤器覆盖
            filterContext.ExceptionHandled = true;

            //在派生类中重写时，获取或设置一个值，该值指定是否禁用IIS自定义错误。
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;




        }
    }
}