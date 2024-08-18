using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communications.Enums
{
    [Flags]
    public enum WorkRegim
    {
        None = 0,
        Email = 1,
        Sms = 2,
        All = Email | Sms
    }
}
