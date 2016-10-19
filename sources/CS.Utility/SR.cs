using System.Globalization;
using System.Resources;
using System.Threading;

namespace CS
{
    /// <summary>
    /// 字符串资源获取
    /// </summary>
    internal sealed class SR
    {
        /// <summary>
        /// ;
        /// </summary>
        internal SR()
        {
            //_resources = new ResourceManager("CS", GetType().Assembly);
            _resources = new ResourceManager(typeof(Res)); //这儿的Res是内置到Lib里编译的，不用去其它DLL查找了
        }

        private readonly ResourceManager _resources;

        /// <summary>
        /// 
        /// </summary>
        public static ResourceManager Resources => GetLoader()._resources;


        private static SR _loader;

        private static SR GetLoader()
        {
            if (_loader != null) return _loader;
            var value = new SR();
            Interlocked.CompareExchange(ref _loader, value, null);
            return _loader;
        }

        private static CultureInfo Culture => null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetString(string name, params object[] args)
        {
            var sR = GetLoader();
            if (sR == null)
            {
                return null;
            }
            var @string = sR._resources.GetString(name, Culture);
            if (args == null || args.Length == 0) return @string;
            for (int i = 0; i < args.Length; i++)
            {
                var text = args[i] as string;
                if (text != null && text.Length > 1024)
                {
                    args[i] = text.Substring(0, 1021) + "...";
                }
            }
            return string.Format(CultureInfo.CurrentCulture, @string, args);
        }

    }
}