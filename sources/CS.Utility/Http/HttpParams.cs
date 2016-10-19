using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace CS.Http
{
    #region HttpParams 是 HttpParam 的集合

    /// <summary>
    /// 自定义的Http请求时便于处理的参数集合。
    /// <par>可显示将键值对的字符串转为HttpParams</par>
    /// <par>可隐式将HttpParams转换为字符串形式的键值对</par>
    /// </summary>
    public class HttpParams : List<HttpParam>
    {
        /// <summary>
        /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            var arrQ = Array.ConvertAll<HttpParam, string>(ToArray(), s => s);
            return string.Join("&", arrQ);
        }

        /// <summary>
        /// 隐式转换，将HttpParams隐式转换成形如：Name=Value&amp;Name2=Value2 ... 形式的字符串
        /// </summary>
        /// <param name="httpParams">Http请求的参数集合<see cref="HttpParams"/></param>
        /// <returns></returns>
        public static implicit operator string(HttpParams httpParams)
        {
            return httpParams.ToString();
        }

        /// <summary>
        /// 强制将查询字符串转换为<see cref="HttpParams"/>。
        /// </summary>
        /// <param name="nvs">含有查询字符串的部分或全部，形如：Name=Value&amp;Name2=Value2 ... </param>
        /// <returns>可能为null</returns>
        /// <exception cref="NullReferenceException">非法的名时值有也无效，此时无法转换合法的HttpParams。只会抛出Null引用的异常。</exception>
        public static explicit operator HttpParams(string nvs)
        {
            var index = nvs.IndexOf("?", StringComparison.Ordinal) + 1;
            var nvString = nvs.Substring(index);
            if (string.IsNullOrEmpty(nvString)) return null;

            var arrNv = nvString.Split('&');
            if (arrNv.Length < 1) return null;
            var result = new HttpParams();
            result.AddRange(arrNv.Select(s => (HttpParam)s));
            return result;
        }

        /// <summary>
        /// name素引器实现
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                var y = Find(x => x.Name == name);
                return y?.Value;
            }
        }


        /// <summary>
        /// 有则改之，无则加之
        /// <remarks>链式</remarks>
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        public HttpParams SetValue(string paramName, object value)
        {
            var y = Find(x => x.Name == paramName);
            //var y = this.FirstOrDefault(x => x.Name == paramName);
            if (y == null)
                Add(new HttpParam(paramName, value.ToString()));
            else
            {
                y.Value = value.ToString();
            }
            return this;
        }

    }

    #endregion


    #region HttpParam Http的参数(URL参数，POST等参数键值对)键值对


    #endregion


    #region HttpParam  比较功能，用于Http参数(URL,POST等里的参数)集合按名称排序时使用

    /// <summary>
    /// Comparer class used to perform the sorting of the query parameters
    /// <remarks>
    /// 同名参数比较其值(按值排)，否则按名字比较(排序)。
    /// </remarks>
    /// </summary>
    public class HttpParamComparer : IComparer<HttpParam>
    {
        #region IComparer<UrlParameter> Members

        int IComparer<HttpParam>.Compare(HttpParam x, HttpParam y)
        {
            return x.Name == y.Name ? String.CompareOrdinal(x.Value, y.Value) : String.CompareOrdinal(x.Name, y.Name);
        }

        #endregion
    }

    #endregion

    /// <summary>
    /// 针对键值集合的进行扩展
    /// </summary>
    public static class NameValueCollectionExtension
    {
        /// <summary>
        /// 转为HttpParam参数
        /// </summary>
        /// <param name="kvs"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static HttpParam ToParam(this NameValueCollection kvs, string key)
        {
            return new HttpParam(key, kvs[key]);
        }
    }
}