using System.Linq;
using System.Text;
using System.Web;
using CS.Extension;

namespace CS.Componets
{
    public class PageNav
    {
        public PageNav(string pageIndexParamName) : this(pageIndexParamName, 3)
        {
        }

        public PageNav(string pageIndexParamName, int pageSize)
        {
            PageParamName = pageIndexParamName;
            Request = HttpContext.Current.Request;
            PageIndex = Request.QueryString[pageIndexParamName].ToInt(1);
            PageSize = pageSize;
        }

        public HttpRequest Request { get; }

        internal string PageParamName { get; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        private int _records;
        /// <summary>
        ///     总记录条数
        /// </summary>
        public int Records
        {
            get { return _records; }
            set
            {
                _records = value;
                TotalPages = (_records + PageSize - 1) / PageSize; //共分页，新算法
                if (PageIndex > TotalPages) PageIndex = TotalPages == 0 ? 1 : TotalPages; //较正分页索引PageIndex
            }
        }
        /// <summary>
        ///     总页数
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// 左右的页面数量
        /// </summary>
        public int NavPages { get; set; } = 4;
        /// <summary>
        /// 开始页
        /// </summary>
        public int BeginPage { get; private set; }

        /// <summary>
        /// 结束页
        /// </summary>
        public int EndPage { get; private set; }

        /// <summary>
        /// 计算分页信息
        /// </summary>
        public void Build()
        {

            if (PageIndex == 0) PageIndex = 1;
            if (Records == 0) Records = int.MaxValue - PageSize - 1;//记录结果，不能太大，否则会导致TotalPages溢出
            TotalPages = (Records + PageSize - 1) / PageSize; //Records / PageSize + 1;
            BeginPage = (PageIndex - NavPages).ToInt(1, 1, int.MaxValue);
            EndPage = (BeginPage + NavPages * 2).ToInt(TotalPages, 0, TotalPages);
            BeginPage = (EndPage == TotalPages) ? (EndPage - NavPages * 2).ToInt(1, 1, int.MaxValue) : BeginPage; //修正
        }

        public string RequestUrlTemplate
        {
            get
            {
                var keys = Request.QueryString.AllKeys.Where(x => x != PageParamName);
                var list = keys.Select(key => $"{key}={Request.QueryString[key]}").ToList();
                list.Add($"{PageParamName}={{0}}");
                return $"?{string.Join("&", list)}";
            }
        }

        /// <summary>
        /// 渲染成HTML代码片断 UL片断
        /// </summary>
        /// <returns></returns>
        public virtual string RenderNav()
        {
            var urlTemplate = RequestUrlTemplate;
            Build();
            var sb = new StringBuilder();
            sb.Append("<ul>");
            sb.Append(PageIndex == 1
                ? "<li class=\"disabled\"><a href=\"javascript:;\">«</a></li>"
                : $"<li><a href=\"{string.Format(urlTemplate, 1)}\">«</a></li>");
            for (int i = BeginPage; i <= EndPage; i++)
            {
                sb.Append(i == PageIndex
                    ? $"<li class=\"active\"><a href=\"javascript:;\">{i}</a></li>"
                    : $"<li><a href=\"{string.Format(urlTemplate, i)}\">{i}</a></li>");
            }
            sb.Append(PageIndex == TotalPages
                ? "<li class=\"disabled\"><a href=\"javascript:;\">»</a></li>"
                : $"<li><a href=\"{string.Format(urlTemplate, TotalPages)}\">»</a></li>");

            sb.Append("</ul>");
            return sb.ToString();
        }

    }
}