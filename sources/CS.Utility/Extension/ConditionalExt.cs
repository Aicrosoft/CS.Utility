namespace CS.Extension
{
    /// <summary>
    /// 条件扩展
    /// </summary>
    public static class ConditionalExt
    {

        /// <summary>
        /// Note:注意，如果结果字符串是需要计算的函数时，在传入参数的的时候会先计算结果，所以会有性能上的损失。
        /// </summary>
        /// <param name="express"></param>
        /// <param name="trueStr"></param>
        /// <param name="falseStr"></param>
        /// <returns></returns>
        public static string Iff(this bool express, string trueStr, string falseStr = null)
        {
            return express ? trueStr : falseStr;
        }




    }
}