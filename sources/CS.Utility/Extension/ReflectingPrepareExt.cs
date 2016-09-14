using System;

namespace CS.Extension
{
    /// <summary>
    /// 为反射而做的相关准备工作
    /// </summary>
    public static class ReflectingPrepareExt
    {

        #region 类相关


        /// <summary>
        /// 返回方法调用信息
        /// </summary>
        /// <param name="fullName">名字空间.类名.方法名,程序集</param>
        /// <returns>{程序集,类全名,方法名}</returns>
        public static string[] ToFuncInfo(this string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return null;
            var arrStr = fullName.Split(',');
            if (arrStr.Length < 2) throw new ArgumentException($"{nameof(fullName)} 参数必须为： 名字空间.类名.方法名,程序集 的形式。");
            var dotIndex = arrStr[0].LastIndexOf(".", StringComparison.Ordinal);
            var className = arrStr[0].Substring(0, dotIndex);
            var methodName = arrStr[0].Substring(dotIndex + 1);
            return new[] { arrStr[1].Trim(), className.Trim(), methodName.Trim() };
        }

        /// <summary>
        /// 返回方法调用信息
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static MethodDefinition ToDefinition(this string fullName)
        {
            var info = fullName.ToFuncInfo();
            var def = new MethodDefinition
            {
                AssemblyName = info[0],
                ClassName = info[1],
                MethodName = info[2]
            };
            return def;
        }



        #endregion 
    }

    /// <summary>
    /// 方法定义
    /// </summary>
    public struct MethodDefinition
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; set;}
        /// <summary>
        /// 类全名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; }

    }

}