using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class EmailConfigModel
    {
        public string SmtpServer { get; set; }
        public string MailUserName { get; set; }
        public string MailPassword { get; set; }
        public string MailFrom { get; set; }
        public string MailPort { get; set; }
        public string EnableSsl { get; set; }
    }
}
