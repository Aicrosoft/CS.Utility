namespace CS.Email
{
    /// <summary>
    /// �ʼ����ͽӿ�
    /// </summary>
    public interface IRichMessageEmailSender : IEmailSender
    {
        /// <summary>
        ///     Sends a EmailMessage.
        /// </summary>
        /// <param name="emailMessage">EmailMessage instance</param>
        void Send(EmailMessage emailMessage);

        /// <summary>
        ///     Sends multiple emailMessages.
        /// </summary>
        /// <param name="emailMessages">Array of emailMessages</param>
        void Send(EmailMessage[] emailMessages);
    }
}