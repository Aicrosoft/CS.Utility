using Newtonsoft.Json;

namespace CS.Extension
{
    public static class JsonExt
    {
        public static string ToJsonByJc(this object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static T FromJsonByJc<T>(this string j)
        {
            return JsonConvert.DeserializeObject<T>(j);
        }
    }
}