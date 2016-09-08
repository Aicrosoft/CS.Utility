using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CS.Attribute
{

    /// <summary>
    /// 枚举文本描述
    /// <remarks>
    /// 仅支持int类型
    /// </remarks>
    /// </summary>
    //[AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class EnumExtAttribute : TextAttribute
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nativeName"></param>
        public EnumExtAttribute(string nativeName) : base(nativeName)
        {
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nativeName"></param>
        /// <param name="ignore"></param>
        public EnumExtAttribute(string nativeName, bool ignore = false) : base(nativeName)
        {
            Ignore = ignore;
        }

        /// <summary>
        /// 是否忽略当前项
        /// </summary>
        public bool Ignore { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EnumOrderExtAttribute : EnumExtAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nativeName"></param>
        public EnumOrderExtAttribute(string nativeName) : base(nativeName)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public EnumOrderExtAttribute(string nativeName, int order) : base(nativeName)
        {
            Order = order;
        }

        /// <summary>
        /// 排序
        /// <remarks>
        /// 默认等于Value值
        /// </remarks>
        /// </summary>
        public int Order { get; set; }
    }


    /// <summary>
    /// 针对枚举的扩展
    /// <remarks>
    /// TODO：枚举不能继承short等非Int或者转换出错
    /// </remarks>
    /// </summary>
    public static class EnumExt
    {

        /// <summary>
        /// 返回枚举类型的自定义描述特性集合 - 泛型版
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetItems<T>(this Type type) where T : IEnumInfo, new()
        {
            var list = new List<T>();
            if (type.IsEnum)
            {
                var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
                list = (from fi in fields select fi.GetValue(null) into value let name = Enum.GetName(type, value) where name != null select new T { Name = name, Value = (int)value, NativeName = name }).ToList();
                var mbs = type.GetMembers();
                foreach (var info in mbs)
                {
                    var attr = System.Attribute.GetCustomAttribute(info, typeof(EnumExtAttribute)) as EnumExtAttribute;
                    if (attr == null) continue;
                    //Console.WriteLine("{0}",  info.Name);
                    var item = list.FirstOrDefault(x => x.Name == info.Name);
                    if (item == null) continue;
                    item.NativeName = attr.NativeName;
                    item.Ignore = attr.Ignore;
                    //扩展的初始化
                    item.Init(attr);
                }
            }
            return list.Where(x => !x.Ignore);
        }

        /// <summary>
        /// 返回枚举类型的自定义描述特性集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<EnumInfo> GetItems(this Type type)
        {
            var list = new List<EnumInfo>();
            if (type.IsEnum)
            {
                var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
                list = (from fi in fields select fi.GetValue(null) into value let name = Enum.GetName(type, value) where name != null select new EnumInfo { Name = name, Value = (int)value, NativeName = name }).ToList();
                //var vs = Enum.GetValues(type);
                //list.AddRange(from object v in Enum.GetValues(type) let name = Enum.GetName(type, v) select new EnumInfo() {Value = v, Name = name, NativeName = name});

                var mbs = type.GetMembers();
                foreach (var info in mbs)
                {
                    var attr = System.Attribute.GetCustomAttribute(info, typeof(EnumExtAttribute)) as EnumExtAttribute;
                    if (attr == null) continue;
                    //Console.WriteLine("{0}",  info.Name);
                    var item = list.FirstOrDefault(x => x.Name == info.Name);
                    if (item == null) continue;
                    //item.Name = attr.NativeName;//Note:不要改变Name，这个是原始的值，对应英文的Value和Name
                    item.NativeName = attr.NativeName;
                    item.Ignore = attr.Ignore;
                    //扩展的初始化
                    item.Init(attr);
                    //item.BgColor = attr.BgColor;
                    //item.Order = attr.Order;
                }
            }
            //return list.Where(x => !x.Ignore).OrderBy(x => x.Order).Cast<EnumInfo>().ToList();
            return list.Where(x => !x.Ignore);
        }

        /// <summary>
        /// 返回该枚举的<see cref="EnumInfo"/>信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static EnumInfo ToEnumInfo(this Enum en)
        {
            var items = en.GetType().GetItems();
            return en.ToEnumInfo(items);
        }

        /// <summary>
        /// 返回该枚举的<see cref="EnumInfo"/>信息
        /// </summary>
        /// <param name="en"></param>
        /// <param name="items">可以是缓存的集合，提高效率</param>
        /// <returns></returns>
        public static EnumInfo ToEnumInfo(this Enum en, IEnumerable<EnumInfo> items)
        {
            var item = items?.FirstOrDefault(x => x.Name == en.ToString());
            return item;
        }

        /// <summary>
        /// 返回该枚举的扩展信息集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="en"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static T ToEnumInfo<T>(this Enum en, List<T> items) where T: class,IEnumInfo
        {
            var item = items?.FirstOrDefault(x => x.Name == en.ToString());
            return item;
        }

    }


}