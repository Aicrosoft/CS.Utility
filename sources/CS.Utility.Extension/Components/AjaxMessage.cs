using Newtonsoft.Json;

namespace CS.Components
{
    /// <summary>
    /// Ajax返回的基本信息
    /// </summary>
    public class AjaxMessage
    {
        public AjaxMessage()
        {
        }

        public AjaxMessage(AjaxMessageType type, string msg)
        {
            Message = msg;
            MessageType = type;
            if (type == AjaxMessageType.Error || type == AjaxMessageType.Warning) Code = -1;
        }

        /// <summary>
        /// 0未知，大于0正提示，小于0出错提示
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        /// <summary>
        /// 消息的级别，如普通，错误，警告等
        /// </summary>
        [JsonProperty("type")]
        public string Type => MessageType.ToString().ToLower();

        private AjaxMessageType _type;
        [JsonIgnore]
        public AjaxMessageType MessageType
        {
            get { return _type; }
            set
            {
                _type = value;
                if (_type == AjaxMessageType.Error || _type == AjaxMessageType.Warning) Code = -1;
            }
        }
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// 携带的具体消息对象
        /// </summary>
        [JsonProperty("ext")]
        public object Ext { get; set; }

        /// <summary>
        /// 根据结果给出成功或者警告的提示信息
        /// </summary>
        /// <param name="result"></param>
        /// <param name="successMsg"></param>
        /// <param name="failMsg"></param>
        public void Tip(bool result, string successMsg, string failMsg)
        {
            if (result)
            {
                Code = 1;
                MessageType = AjaxMessageType.Success;
                Message = successMsg;
            }
            else
            {
                Code = -1;
                MessageType = AjaxMessageType.Warning;
                Message = failMsg;
            }
        }

        /// <summary>
        /// 根据结果给出成功或者警告的提示信息
        /// </summary>
        /// <param name="result"></param>
        /// <param name="msgTemplate">{0}占位成功或失败位置</param>
        public void Tip(bool result, string msgTemplate)
        {
            if (result)
            {
                Code = 1;
                MessageType = AjaxMessageType.Success;
                Message = string.Format(msgTemplate, "成功");
            }
            else
            {
                Code = -1;
                MessageType = AjaxMessageType.Warning;
                Message = string.Format(msgTemplate, "失败");
            }
        }

        public void Success(string msg)
        {
            MessageType = AjaxMessageType.Success;
            Message = msg;
        }

        public void Info(string msg)
        {
            MessageType = AjaxMessageType.Info;
            Message = msg;
        }

        public void Warning(string msg)
        {
            MessageType = AjaxMessageType.Warning;
            Message = msg;
        }

        public void Error(string msg)
        {
            MessageType = AjaxMessageType.Error;
            Message = msg;
        }

    }

    public enum AjaxMessageType
    {
        Info = 0,

        Success,

        Warning,

        Error,
    }

}