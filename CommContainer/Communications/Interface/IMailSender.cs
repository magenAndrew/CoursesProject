using Communications.Models;
using Communications.Settings;
using Notify.DTO;

namespace Communications
{
    public interface IMailSender
    {
        void SendEmail(MailRmq message, MailSettings settings);
    }
}
