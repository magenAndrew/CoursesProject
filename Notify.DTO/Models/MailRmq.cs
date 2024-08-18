

namespace Notify.DTO
{
    public class MailRmq
    {
        public IEnumerable<string> Receivers { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public 
        MailRmq() {
            Receivers = new List<string>();
        }
    }
}
