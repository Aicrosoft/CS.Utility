using System;
using System.Collections.Generic;
using System.Linq;

namespace CS.Validation
{
    /// <summary>
    /// 验证，核查参数合法性
    /// </summary>
    public class Validator
    {
        readonly List<ValidPack> _dic;
        /// <summary>
        /// 
        /// </summary>
        public Validator()
        {
            _dic = new List<ValidPack>();
            Message = new List<string>();
        }
        /// <summary>
        /// 验证后的消息
        /// </summary>
        public List<string> Message { get; }

        /// <summary>
        /// 初始化待验证的所有属性
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="function"></param>
        /// <param name="errorMsg"></param>
        public void Init(string paramName, Func<string, bool> function, string errorMsg)
        {
            _dic.Add(new ValidPack()
            {
                ParamName = paramName,
                ErrorMessage = errorMsg,
                Function = function
            });
        }
        /// <summary>
        /// 验证单个值
        /// <remarks>需提前执行Init来初始化要验证的参数相关信息</remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Valid<T>(T model)
        {
            if (_dic.Count == 0) return false;
            var rst = true;
            var tp = model.GetType();
            var ms = tp.GetProperties();
            foreach (var pack in _dic)
            {
                var item = ms.FirstOrDefault(x => x.Name.Equals(pack.ParamName, StringComparison.CurrentCultureIgnoreCase));
                if(item==null) continue;
                var result = pack.Function.Invoke(item.GetValue(model, null)?.ToString());
                if (result) continue;
                rst = false;
                Message.Add(string.Format(pack.ErrorMessage, pack.ParamName));
            }
            return rst;
        }

        /// <summary>
        /// 增加一个验证到验证器里
        /// </summary>
        /// <param name="paramName">参数名称，可以为空</param>
        /// <param name="errorMsg"></param>
        /// <param name="function"></param>
        /// <param name="paramValue"></param>
        public void Add(string paramValue, Func<string, bool> function, string errorMsg, string paramName = null)
        {
            _dic.Add(new ValidPack()
            {
                ParamName = paramName,
                ErrorMessage =  errorMsg,
                Function = function,
                ParamValue = paramValue
            });
        }
        /// <summary>
        /// 返回验证结果
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            if (_dic.Count == 0) return false;
            var rst = true;
            foreach (var pack in _dic)
            {
                var result = pack.Function.Invoke(pack.ParamValue);
                if (result) continue;
                rst = false;
                Message.Add(string.Format(pack.ErrorMessage, pack.ParamName));
            }
            return rst;
        }


        class ValidPack
        {
            public string ParamName { get; set; }

            public string ErrorMessage { get; set; }

            public Func<string, bool> Function { get; set; }

            public string ParamValue { get; set; }
        }


        /// <summary>
        /// 如果验证Func成立，则抛出异常消息
        /// </summary>
        /// <param name="validateFunc"></param>
        /// <param name="message">空时不抛异常</param>
        public static void Validate(Func<bool> validateFunc, string message = null)
        {
            if (validateFunc())
                throw new ParameterException(message);
        }
        /// <summary>
        /// 直接抛出参数相关的异常
        /// <remarks>
        /// 主要用于修改原有异常的，不抛出原有的异常内容
        /// </remarks>
        /// </summary>
        /// <param name="message"></param>
        public static void Throw(string message)
        {
            throw new ParameterException(message);
        }

    }

}