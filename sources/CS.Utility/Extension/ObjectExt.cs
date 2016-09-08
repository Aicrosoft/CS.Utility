namespace CS.Extension
{
    public static class ObjectExt
    {
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