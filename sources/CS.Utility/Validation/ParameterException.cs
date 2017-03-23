using System;

namespace CS.Validation
{
    /// <summary>
    /// 参数异常
    /// </summary>
    public class ParameterException:Exception
    {
        public ParameterException(string msg) : base(msg)
        {
        }
    }
}