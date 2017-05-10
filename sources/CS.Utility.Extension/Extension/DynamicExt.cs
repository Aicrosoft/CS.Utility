using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CS.Extension
{
    public static class DynamicExt
    {
        /// <summary>
        /// 将字符串转为动态对像
        /// </summary>
        /// <param name="anyJsonString"></param>
        /// <returns></returns>
        public static dynamic FromJsonByJc(this string anyJsonString)
        {
            return anyJsonString.IsNullOrWhiteSpace() ? null : JObject.Parse(anyJsonString);
        }

        //public static string ToJsonByJc(this dynamic o)
        //{
        //    return o == null ? null : JsonConvert.SerializeObject(o);

        //    //new JsonSerializerSettings()
        //    //{
        //    //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    //}
        //}

    }
}