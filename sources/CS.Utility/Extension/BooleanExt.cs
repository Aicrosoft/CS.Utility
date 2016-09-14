namespace CS.Extension
{
    /// <summary>
    /// bool结构扩展
    /// </summary>
    public static class BooleanExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="express"></param>
        /// <param name="trueStr"></param>
        /// <param name="falseStr"></param>
        /// <returns></returns>
        public static string If(this bool express, string trueStr, string falseStr)
        {
            return express ? trueStr : falseStr;
        }

    }
}