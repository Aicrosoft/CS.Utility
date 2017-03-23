using System.Text;
using System.Web;

namespace CS.Utils
{
    /// <summary>
    /// 报表相关操作，比如导出
    /// </summary>
    public class ReportHelper
    {
        /// <summary>
        /// 导出字符串文本文件
        /// <remarks>
        /// csv格式只能意表
        /// </remarks>
        /// </summary>
        public static void ExportText(string fileName, string text)
        {
            HttpContext.Current.Response.AppendHeader("Content-Disposition", $"attachment;filename={fileName}");
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = Encoding.Default;
            HttpContext.Current.Response.ContentType = "application/csv";
            HttpContext.Current.Response.Write(text);
            HttpContext.Current.Response.End();
        }
    }
}