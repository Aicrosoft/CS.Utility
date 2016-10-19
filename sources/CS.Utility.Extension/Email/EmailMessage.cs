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
        /// ������
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Cc { get; set; }
        /// <summary>
        /// �ܳ�
        /// </summary>
        public string Bcc { get; set; }
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// �ظ�
        /// </summary>
        public MailAddress[] ReplyTos { get; set; }
        /// <summary>
        /// ��ʽ��Ĭ��Ϊ�ı�����
        /// </summary>
        public Format Format { get; set; } = Format.Text;
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        /// <summary>
        /// �ʼ����ȼ���
        /// </summary>
        public MailPriority Priority { get; set; } = MailPriority.Normal;
        /// <summary>
        /// �ʼ���ͷ����
        /// </summary>
        public IDictionary Headers { get; } = new HybridDictionary();

        //public IDictionary Fields { get; } = new HybridDictionary();
        /// <summary>
        /// ����
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