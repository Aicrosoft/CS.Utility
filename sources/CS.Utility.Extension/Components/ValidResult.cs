using System;
using System.Collections.Generic;

namespace CS.Components
{
    /// <summary>
    /// 带多项验证的结果
    /// </summary>
    public class ValidResult
    {

        public int Code { get; set; }

        public bool HasMessage => _messages != null && _messages.Count>0;

        //public TInstance Instance { get; set; }

        public  void Validate(Func<bool> validateFunc, string message = null)
        {
            if( validateFunc())
                Messages.Add(message);
        }

        private List<string> _messages;
        /// <summary>
        /// 提示消息
        /// </summary>
        public List<string> Messages => _messages ?? (_messages = new List<string>());


        public void PushMessage(string message)
        {
            Messages.Add(message);
        }
    }


    public static class ValidResultExt
    {
        public static AjaxMessage ToAjax(this ValidResult result)
        {
            var ajax = new AjaxMessage();
            if (!result.HasMessage) return ajax;
            ajax.MessageType = AjaxMessageType.Error;
            ajax.Message = string.Join(Environment.NewLine, result.Messages.ToArray());
            return ajax;
        }
    }
}