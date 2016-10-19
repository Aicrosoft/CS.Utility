using System;

namespace CS.Attribute
{
    /// <summary>
    /// 忽略标识
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class IgnoreAttribute : System.Attribute
    {
    }
}