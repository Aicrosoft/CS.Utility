using System.Collections.Generic;

namespace CS.Utils
{
    public class DynamicHelper
    {
        /// <summary>
        /// 动态对像是否有某个属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool Have(dynamic obj,string propertyName)
        {
            return ((IDictionary<string, object>)obj).ContainsKey(propertyName);
        }
    }
}