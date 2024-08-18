using Communications.Models;
using Communications.Settings;
using MimeKit;
using MailKit.Net.Smtp;
using Notify.DTO;

namespace Communications
{
    public class MailSender : IMailSender
    {
        public  void SendEmail(MailRmq message, MailSettings settings)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(settings.From));
            foreach (var address in message.Receivers)
            {
                email.To.Add(MailboxAddress.Parse(address));
            }
            var body = new BodyBuilder();
            body.HtmlBody = message.Body;
            email.Body = body.ToMessageBody();
            email.Subject = message.Title;

            using (var client = new SmtpClient())
            {
                client.Connect(settings.Host, settings.Port, settings.UseStartTls);
                client.Authenticate(settings.UserName, settings.Password);
                client.Send(email);
                client.Disconnect(true);
            }
        }
    }
}
