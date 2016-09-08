namespace System.Windows.Forms
{
    /// <summary>
    /// 尽量不用继承类来实现一些功能
    /// </summary>
    public static class FromExt
    {

        /// <summary>
        /// 线程操作UI上的控件的值变更
        /// </summary>
        /// <param name="form"></param>
        /// <param name="value"></param>
        /// <param name="func"></param>
        public static void ChangeValue(this Form form,string value, Action<string> func)
        {
            if (form.InvokeRequired)
                form.Invoke(func, value);
            else
                func(value);
        }
    }
}