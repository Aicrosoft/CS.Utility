using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace CS.Email
{
    /// <summary>
    ///     Abstracts an e-mail message
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EmailMessage" /> class.
        /// </summary>
        public EmailMessage()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EmailMessage" /> class.
        /// </summary>
        /// <param name="from">From header.</param>
        /// <param name="to">To header.</param>
        /// <param name="subject">The subject header.</param>
        /// <param name="body">The message body.</param>
        public EmailMessage(string from, string to, string subject, string body)
        {
            To = to;
            From = from;
            Body = body;
            Subject = subject;
        }

        /// <summary>
        /// 收信人
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 发信人
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 抄送
        /// </summary>
        public string Cc { get; set; }
        /// <summary>
        /// 密抄
        /// </summary>
        public string Bcc { get; set; }
        /// <summary>
        /// 邮件正文
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 回复
        /// </summary>
        public MailAddress[] ReplyTos { get; set; }
        /// <summary>
        /// 格式，默认为文本内容
        /// </summary>
        public Format Format { get; set; } = Format.Text;
        /// <summary>
        /// 邮件编码
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        /// <summary>
        /// 邮件优先级别
        /// </summary>
        public MailPriority Priority { get; set; } = MailPriority.Normal;
        /// <summary>
        /// 邮件标头集合
        /// </summary>
        public IDictionary Headers { get; } = new HybridDictionary();

        //public IDictionary Fields { get; } = new HybridDictionary();
        /// <summary>
        /// 附件
        /// </summary>
        public MessageAttachmentList Attachments { get; } = new MessageAttachmentList();

        /// <summary>
        ///     You can add any number of inline attachments to this mail message. Inline attachments
        ///     differ from normal attachments in that they can be displayed withing the email body,
        ///     which makes this very handy for displaying images that can be viewed without having to
        ///     access an external server.
        ///     Provide an unique identifier (id) and use it with a &lt;img src="cid:my_id" /> tag from
        ///     within your view code.
        /// </summary>
        public IDictionary<string, LinkedResource> Resources { get; } = new Dictionary<string, LinkedResource>();
    }
}