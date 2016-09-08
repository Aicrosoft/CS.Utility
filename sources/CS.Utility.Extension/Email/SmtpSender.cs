using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using CS.Logging;

namespace CS.Email
{
    /// <summary>
    ///     Uses Smtp to send emails.
    /// </summary>
    public class SmtpSender : IRichMessageEmailSender
    {
        private readonly NetworkCredential _credentials = new NetworkCredential();
        private readonly SmtpClient _smtpClient;
        private bool _configured;
        private readonly ILog _log = LogManager.GetLogger(typeof(SmtpSender));

        /// <summary>
        ///     This service implementation
        ///     requires a host name in order to work
        /// </summary>
        /// <param name="hostname">The smtp server name</param>
        public SmtpSender(string hostname)
        {
            Hostname = hostname;
            _smtpClient = new SmtpClient(hostname);
        }

        /// <summary>
        ///     Gets or sets the port used to
        ///     access the SMTP server
        /// </summary>
        public int Port { get; set; } = 25;

        /// <summary>
        ///     Gets the hostname.
        /// </summary>
        /// <value>The hostname.</value>
        public string Hostname { get; }

        /// <summary>
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        ///     Gets or sets a value which is used to
        ///     configure if emails are going to be sent asyncrhonously or not.
        /// </summary>
        public bool AsyncSend { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value that specifies
        ///     the amount of time after which a synchronous Send call times out.
        /// </summary>
        public int Timeout
        {
            get { return _smtpClient.Timeout; }
            set { _smtpClient.Timeout = value; }
        }

        /// <summary>
        ///     Gets or sets the domain.
        /// </summary>
        /// <value>The domain.</value>
        public string Domain
        {
            get { return _credentials.Domain; }
            set { _credentials.Domain = value; }
        }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get { return _credentials.UserName; }
            set { _credentials.UserName = value; }
        }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get { return _credentials.Password; }
            set { _credentials.Password = value; }
        }

        /// <summary>
        ///     Gets a value indicating whether credentials were informed.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance has credentials; otherwise, <see langword="false" />.
        /// </value>
        protected bool HasCredentials => _credentials.UserName != null && _credentials.Password != null ? true : false;

        /// <summary>
        ///     Sends a EmailMessage.
        /// </summary>
        /// <exception cref="ArgumentNullException">If any of the parameters is null</exception>
        /// <param name="from">From field</param>
        /// <param name="to">To field</param>
        /// <param name="subject">e-mail's subject</param>
        /// <param name="messageText">EmailMessage's body</param>
        public void Send(string from, string to, string subject, string messageText)
        {
            if (from == null) throw new ArgumentNullException(nameof(@from));
            if (to == null) throw new ArgumentNullException(nameof(to));
            if (subject == null) throw new ArgumentNullException(nameof(subject));
            if (messageText == null) throw new ArgumentNullException(nameof(messageText));

            Send(new EmailMessage(from, to, subject, messageText));
        }

        /// <summary>
        /// 异步发邮件
        /// </summary>
        /// <param name="emailMessage"></param>
        /// <returns></returns>
        public async Task<bool> BeginSend(EmailMessage emailMessage)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Send(emailMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    _log.Debug(ex);
                    //throw;
                    return false;
                }
            });
        }


        /// <summary>
        ///     Sends a EmailMessage.
        /// <remarks>
        /// TODO:异步发送时无法发送成功
        /// </remarks>
        /// </summary>
        /// <exception cref="ArgumentNullException">If the EmailMessage is null</exception>
        /// <param name="emailMessage">EmailMessage instance</param>
        public void Send(EmailMessage emailMessage)
        {
            if (emailMessage == null) throw new ArgumentNullException(nameof(emailMessage));

            ConfigureSender(emailMessage);

            if (AsyncSend)
            {
                // The MailMessage must be diposed after sending the email.
                // The code creates a delegate for deleting the mail and adds
                // it to the smtpClient.
                // After the mail is sent, the EmailMessage is disposed and the
                // eventHandler removed from the smtpClient.
                var msg = CreateMailMessage(emailMessage);
                //var msgGuid = new Guid();//ERROR
                var msgGuid = Guid.NewGuid();
                SendCompletedEventHandler sceh = null;
                sceh = delegate (object sender, AsyncCompletedEventArgs e)
                {
                    if (msgGuid == (Guid)e.UserState)
                        msg.Dispose();
                    // The handler itself, cannot be null, test omitted
                    _smtpClient.SendCompleted -= sceh;
                };
                _smtpClient.SendCompleted += sceh;
                _smtpClient.SendAsync(msg, msgGuid);
            }
            else
            {
                using (var msg = CreateMailMessage(emailMessage))
                {
                    _smtpClient.Send(msg);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailMessages"></param>
        public void Send(EmailMessage[] emailMessages)
        {
            foreach (var message in emailMessages)
            {
                Send(message);
            }
        }

        /// <summary>
        ///     Converts a EmailMessage from Castle.Components.Common.EmailSender.EmailMessage  type
        ///     to System.Net.Mail.MailMessage
        /// </summary>
        /// <param name="emailMessage">The EmailMessage to convert.</param>
        /// <returns>The converted EmailMessage .</returns>
        private static MailMessage CreateMailMessage(EmailMessage emailMessage)
        {
            var mailMessage = new MailMessage(emailMessage.From, emailMessage.To.Replace(';', ','));

            if (!string.IsNullOrEmpty(emailMessage.Cc))
            {
                mailMessage.CC.Add(emailMessage.Cc);
            }

            if (!string.IsNullOrEmpty(emailMessage.Bcc))
            {
                mailMessage.Bcc.Add(emailMessage.Bcc);
            }

            mailMessage.Subject = emailMessage.Subject;
            mailMessage.Body = emailMessage.Body;
            mailMessage.BodyEncoding = emailMessage.Encoding;
            mailMessage.IsBodyHtml = (emailMessage.Format == Format.Html);
            mailMessage.Priority = emailMessage.Priority;
            if (emailMessage.ReplyTos != null)
            {
                foreach (var replyTo in emailMessage.ReplyTos)
                {
                    mailMessage.ReplyToList.Add(replyTo);
                }
            }

            foreach (DictionaryEntry entry in emailMessage.Headers)
            {
                mailMessage.Headers.Add((string)entry.Key, (string)entry.Value);
            }

            foreach (var mailAttach in emailMessage.Attachments.Select(attachment => attachment.Stream != null
                ? new Attachment(attachment.Stream, attachment.FileName, attachment.MediaType)
                : new Attachment(attachment.FileName, attachment.MediaType)))
            {
                mailMessage.Attachments.Add(mailAttach);
            }

            if (emailMessage.Resources == null || emailMessage.Resources.Count <= 0) return mailMessage;
            var htmlView = AlternateView.CreateAlternateViewFromString(emailMessage.Body, emailMessage.Encoding, "text/html");
            foreach (var id in emailMessage.Resources.Keys)
            {
                var r = emailMessage.Resources[id];
                r.ContentId = id;
                if (r.ContentStream != null)
                {
                    htmlView.LinkedResources.Add(r);
                }
            }
            mailMessage.AlternateViews.Add(htmlView);
            return mailMessage;
        }

        /// <summary>
        ///     Configures the EmailMessage or the sender
        ///     with port information and eventual credential
        ///     informed
        /// </summary>
        /// <param name="emailMessage">EmailMessage instance</param>
        protected virtual void ConfigureSender(EmailMessage emailMessage)
        {
            if (!_configured)
            {
                if (HasCredentials)
                {
                    _smtpClient.Credentials = _credentials;
                }

                _smtpClient.Port = Port;
                _smtpClient.EnableSsl = EnableSsl;
                // REVIEW: might need to do more than this to enable ssl for all smtp servers

                _configured = true;
            }
        }
    }
}