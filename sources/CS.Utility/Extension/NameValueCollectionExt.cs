using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace CS.Extension
{
    public static class NameValueCollectionExt
    {
        public static IDictionary<string, string> ToDictionary(this NameValueCollection source)
        {
            return source.AllKeys.ToDictionary(k => k, k => source[k]);
        }
    }
}