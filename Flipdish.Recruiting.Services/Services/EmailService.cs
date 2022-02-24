using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using System.IO;

namespace Flipdish.Recruiting.Services.Services
{
    public interface IEmailService
    {
        Task Send(string from, IEnumerable<string> to, string subject, string body, Dictionary<string, Stream> attachements, IEnumerable<string> cc = null);
    }

    internal class EmailService : IEmailService
    {
        public async Task Send(string from, IEnumerable<string> to, string subject, string body, Dictionary<string, Stream> attachements, IEnumerable<string> cc = null)
        {
            using MailMessage mailMessage = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(from),
                Subject = subject,
                Body = body
            };
            foreach (var t in to)
            {
                mailMessage.To.Add(t);
            }

            if (cc != null)
            {
                foreach (var t in cc)
                {
                    mailMessage.To.Add(t);
                }
            }
            foreach (var nameAndStreamPair in attachements)
            {
                var attachment = new Attachment(nameAndStreamPair.Value, nameAndStreamPair.Key);
                attachment.ContentId = nameAndStreamPair.Key;
                mailMessage.Attachments.Add(attachment);
            }

            using SmtpClient mailer = new SmtpClient
            {
                Host = "",
                //Port = 587,
                Credentials = new System.Net.NetworkCredential("", "")
            };
            await mailer.SendMailAsync(mailMessage);
        }
    }
}
