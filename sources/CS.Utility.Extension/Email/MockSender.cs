namespace CS.Email
{
    /// <summary>
    /// Mock
    /// </summary>
    public class MockSender : IRichMessageEmailSender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="messageText"></param>
        public virtual void Send(string from, string to, string subject, string messageText)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailMessage"></param>
        public virtual void Send(EmailMessage emailMessage)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailMessages"></param>
        public virtual void Send(EmailMessage[] emailMessages)
        {
        }
    }
}