using Newtonsoft.Json.Linq;

namespace CS.Extension
{
    public static class JObjectExt
    {
        public static bool Has(this JObject jo, string propertyName)
        {
            return jo[propertyName] != null;
        }
    }
}