using Communications.Enums;
using Communications.Settings;

namespace Communication.Settings
{
    public class ApplicationSettings
    {
        public WorkRegim Regim { get; set; }
        public RmqSettings RmqSettings { get; set; }
        public MailSettings MailServerSettings { get; set; }
        public int MailConsumers { get; set; }
        public int SmsConsumers { get; set; }
    }
}