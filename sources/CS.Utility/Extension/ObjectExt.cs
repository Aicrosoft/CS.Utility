using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CS.Extension
{
    public static class ObjectExt
    {

        /// <summary>
        /// 是否有某个属性
        /// </summary>
        /// <param name="o"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool Has(this object o, string propertyName)
        {
            return ((IDictionary<string, object>)o).ContainsKey(propertyName);
        }


        /// <summary>
        /// 将被解析的对像的所有加上了扩展特性的属性返回
        /// </summary>
        /// <typeparam name="T">关于<see cref="System.Attribute"/>的扩展类型</typeparam>
        /// <param name="o">被解析的对像</param>
        /// <returns></returns>
        public static List<Tuple<T, PropertyInfo>> GetProperties<T>(this object o) where T: System.Attribute
        {
            var ps = o.GetType().GetProperties();
            return (from p in ps let ext = p.GetCustomAttribute<T>() where ext != null select new Tuple<T, PropertyInfo>(ext, p)).ToList();
        }


        ///// <summary>
        ///// 填充集合中Key所对应的所有属性值
        ///// </summary>
        ///// <param name="o"></param>
        ///// <param name="dic"></param>
        ///// <param name="comparison">默认忽略大小写</param>
        //public static void FillProperties(this object o, SortedDictionary<string, object> dic, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        //{
        //    var ps = o.GetType().GetProperties();
        //    foreach (var o1 in dic)
        //    {
        //        dic[o1.Key] = ps.FirstOrDefault(x => x.Name.Equals(o1.Key, comparison))?.GetValue(o);
        //    }
        //}

        /// <summary>
        /// 返回属性的值
        /// </summary>
        /// <param name="o"></param>
        /// <param name="propertyName"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object o, string propertyName, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            var val = o.GetType().GetProperties().FirstOrDefault(x => x.Name.Equals(propertyName, comparison))?.GetValue(o);
            return val;
        }

        /// <summary>
        /// 获取集合中Keys所对应的所有属性值
        /// 注：只针对是get set 这样的属性才能获取
        /// </summary>
        /// <param name="o"></param>
        /// <param name="keys"></param>
        /// <param name="comparison"></param>
        public static SortedDictionary<string, object> GetPropertiesSortedDictionary(this object o, string[] keys, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            var dic = new SortedDictionary<string, object>();
            var ps = o.GetType().GetProperties();
            foreach (var key in keys)
            {
                dic[key] = ps.FirstOrDefault(x => x.Name.Equals(key, comparison))?.GetValue(o);
            }
            return dic;
        }

        /// <summary>
        /// 获取集合中Keys所对应的所有属性值
        /// 注：针对直接定义为 public string Name; 这种，没有get set 的定义
        /// </summary>
        /// <param name="o"></param>
        /// <param name="keys"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static SortedDictionary<string, object> GetFieldsSortedDictionary(this object o, string[] keys, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            var dic = new SortedDictionary<string, object>();
            var ps = o.GetType().GetFields();
            foreach (var key in keys)
            {
                dic[key] = ps.FirstOrDefault(x => x.Name.Equals(key, comparison))?.GetValue(o);
            }
            return dic;
        }


        #region others tests

        ////static MethodInfo byteArrayToHexString;

        ///// <summary>
        ///// 反射调用该方法, 并且缓存.
        ///// </summary>
        ///// <returns></returns>
        //static MethodInfo ByteArrayToHexStringMethod()
        //{
        //    if (byteArrayToHexString == null)
        //    {
        //        Type type = typeof(System.Web.Configuration.MachineKeySection);
        //        byteArrayToHexString = type.GetMethod("ByteArrayToHexString", BindingFlags.Static | BindingFlags.NonPublic);
        //    }
        //    return byteArrayToHexString;
        //}



        //public static K[] ParseIDArray<T, K>(ComponentCollection<T> obj, ParseAction<T, K> action) where T : IComponent
        //{
        //    if (obj.Count == 0) return null;
        //    if (obj.Count == 1) return new K[] { action(obj[0]) };
        //    List<K> result = new List<K>();
        //    foreach (T item in obj)
        //        result.Add(action(item));
        //    return result.ToArray();
        //}

        #endregion
    }
}