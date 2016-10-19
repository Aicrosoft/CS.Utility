namespace CS.Components
{
    /// <summary>
    /// 操作结果返回的一般提示
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 默认的操作结果是成功的
        /// </summary>
        public Result()
        {
            Success = true;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 提示消息
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// 不成功的理由
        /// </summary>
        public virtual string[] Errors { get; set; }
    }
}