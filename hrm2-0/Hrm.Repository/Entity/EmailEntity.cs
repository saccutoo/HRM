using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class EmailEntity:BaseEntity
    {
        public string Title { get; set; }
        public string ContentTemplate { get; set; }
        public string MailTo { get; set; }
        public string MailCc { get; set; }
        public string MailBcc { get; set; }
        public string Note { get; set; }
        public long CountAttach { get; set; }
    }
}
