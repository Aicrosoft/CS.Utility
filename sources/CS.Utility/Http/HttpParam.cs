using System;

namespace CS.Http
{
    /// <summary>
    /// Http的参数名及值
    /// 键值对只读，只能在构造时赋值。
    /// <remarks>Http的参数(URL参数，POST等参数键值对)键值对</remarks>
    /// </summary>
    public class HttpParam
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public HttpParam(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => !string.IsNullOrEmpty(Name) ? $"{Name}={Value}" : string.Empty;

        /// <summary>
        /// 隐式转换，将UrlParamter隐式转换成形如：Name=Value形式的字符串
        /// </summary>
        /// <param name="httpParam"></param>
        /// <returns></returns>
        public static implicit operator string (HttpParam httpParam)
        {
            return httpParam.ToString();
        }

        /// <summary>
        /// 强制将形如Name=Value的字符串转换为HttpParam的形式。
        /// </summary>
        /// <param name="keyValue">形如Name=Value的字符串</param>
        /// <returns>可能为null</returns>
        /// <exception cref="NullReferenceException">非法的名时值有也无效，此时无法转换合法的UrlParamter参数。只会抛出Null引用的异常。</exception>
        public static explicit operator HttpParam(string keyValue)
        {
            var arr = keyValue.Split('=');
            return (arr.Length != 2 || string.IsNullOrEmpty(arr[0])) ? null : new HttpParam(arr[0], arr[1]);
        }

    }

}