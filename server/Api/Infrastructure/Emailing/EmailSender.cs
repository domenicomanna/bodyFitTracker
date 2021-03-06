using System.Security.Authentication;
using Api.Common;
using Api.Common.Interfaces;
using Api.Configurations;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Api.Infrastructure.Emailing
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(EmailMessage emailMessage)
        {
            MimeMessage message = CreateMimeMessage(emailMessage);

            Send(message);
        }

        private MimeMessage CreateMimeMessage(EmailMessage emailMessage)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(MailboxAddress.Parse(_emailSettings.From));
            mimeMessage.To.AddRange(emailMessage.To);
            mimeMessage.Subject = emailMessage.Subject;

            BodyBuilder builder = new BodyBuilder();
            builder.TextBody = emailMessage.TextBody;
            builder.HtmlBody = emailMessage.HtmlBody;

            mimeMessage.Body = builder.ToMessageBody();

            return mimeMessage;
        }

        private void Send(MimeMessage mimeMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
                    client.Authenticate(_emailSettings.UserName, _emailSettings.Password);
                    client.Send(mimeMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}