using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CS.Utils
{

    /// <summary>
    /// 类型辅助
    /// </summary>
    public class TypeHelper
    {

        /// <summary>
        /// 返回某了父类下所有子类的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetSubclassObjects<T>()
        {
            var types = GetSubclasses(typeof (T));
            return types.Select(tp => (T) Activator.CreateInstance(tp)).Where(instance => instance != null).ToList();
        }


        /// <summary>
        /// 返回 参数types 所在程序集中所有实现了T类型的子类集合
        /// <remarks>
        /// <![CDATA[
        /// Example:    TypeHelper.GetSubClasses<CmsManageController>(new[] {typeof (DebugController)});
        /// ]]>        
        /// </remarks>
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetSubClasses<T>(Type[] types) where T :class 
        {
            var asses = types.Select(x => x.Assembly).Distinct();
            var subTypes = new List<Type>();
            foreach (var assembly in asses)
            {
                subTypes.AddRange(assembly.GetTypes().Where(x=>x.IsSubclassOf(typeof(T))));
            }
            return subTypes.Distinct();
        }

        /// <summary>
        /// 返回T类型所在程序集中所有实现了T类型的子类集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetSubClasses<T>() where T : class
        {
            var type = typeof (T);
            return type.Assembly.GetTypes().Where(x => x.IsSubclassOf(type));
        }

        /// <summary>
        /// 返回某了父类下所有子类的类型<see cref="Type"/>集合
        /// </summary>
        /// <returns></returns>
        public static List<Type> GetSubclasses(Type baseType)
        {
            var types = Assembly.GetAssembly(baseType).GetTypes().Where(x => x.IsSubclassOf(baseType)).ToList();
            return types;
            //try
            //{
            //    var types = Assembly.GetAssembly(baseType).GetTypes().Where(x => x.IsSubclassOf(baseType)).ToList();
            //    return types;
            //}
            //catch (ReflectionTypeLoadException ex)
            //{
            //    foreach (var exception in ex.LoaderExceptions)
            //    {
            //        Tracer.Error("LoaderExceptions[]", ex);
            //    }
            //    Tracer.Error("InnerException", ex.InnerException);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        /// <summary>
        /// 转换值为目标类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static  object ChangeType(object value, Type type)
        {
            //if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, (string) value);
                return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, innerValue);
            }
            if (value is string && type == typeof(Guid)) return new Guid((string) value);
            if (value is string && type == typeof(Version)) return new Version((string) value);
            if (!(value is IConvertible)) return value;
            return Convert.ChangeType(value, type);
        }

        /// <summary>
        /// 根据类型名称： 类型名,所在程序集名  或 简单类型名   找到具体的类型
        /// </summary>
        /// <param name="typename"></param>
        /// <returns></returns>
        public static Type FindType(string typename)
        {
            if (string.IsNullOrWhiteSpace(typename)) throw new TypeLoadException($"[Type:{typename}]类型名不能为空。");
            var arrType = typename.Split(',');
            try
            {
                var tp = GetType(arrType[0]);
                if (tp != null)
                    return tp;
            }
            catch (TypeLoadException)
            {
                //Tracer.Warn("获取类型时异常", ex);
                //异常时继续下面的其它程序集查找,所以这儿的日志也没有必要了
            }
            if (arrType.Length < 2) throw new TypeLoadException($"Type:{typename}无法查找到类型，请加上类型所在的程序集名称。");
            var assembly = Assembly.Load(arrType[1]);
            return assembly.GetType(arrType[0]);
        }



        /// <summary>
        /// 获取字符串对应的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetType(string type)
        {
            switch (type.ToLower())
            {
                case "bool":
                    return Type.GetType("System.Boolean", true, true);
                case "byte":
                    return Type.GetType("System.Byte", true, true);
                case "sbyte":
                    return Type.GetType("System.SByte", true, true);
                case "char":
                    return Type.GetType("System.Char", true, true);
                case "decimal":
                    return Type.GetType("System.Decimal", true, true);
                case "double":
                    return Type.GetType("System.Double", true, true);
                case "float":
                    return Type.GetType("System.Single", true, true);
                case "int":
                    return Type.GetType("System.Int32", true, true);
                case "uint":
                    return Type.GetType("System.UInt32", true, true);
                case "long":
                    return Type.GetType("System.Int64", true, true);
                case "ulong":
                    return Type.GetType("System.UInt64", true, true);
                case "object":
                    return Type.GetType("System.Object", true, true);
                case "short":
                    return Type.GetType("System.Int16", true, true);
                case "ushort":
                    return Type.GetType("System.UInt16", true, true);
                case "string":
                    return Type.GetType("System.String", true, true);
                case "date":
                case "datetime":
                    return Type.GetType("System.DateTime", true, true);
                case "guid":
                    return Type.GetType("System.Guid", true, true);
                default:
                    return Type.GetType(type, true, true);
            }
        }



    }
}