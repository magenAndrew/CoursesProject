using Communications.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communications.Models
{
    public static class InfoApp
    {
        public static WorkRegim Regim { get;set;} 
        public static DateTime? DateStart { get; set; }
        public static DateTime? DateEnd { get; set; }
        public static int MailCount {  get; set; }
        public static int SmsCount { get; set; }
    }
}
