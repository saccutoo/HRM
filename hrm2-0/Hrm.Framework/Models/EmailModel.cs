using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class EmailModel:BaseModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string ContentTemplate { get; set; }
        public string MailTo { get; set; }
        public string MailCc { get; set; }
        public string MailBcc { get; set; }
        public string Note { get; set; }
        public long CountAttach { get; set; }
        public List<string> ListMailCc { get; set; } = new List<string>();
        public List<string> ListMailBcc { get; set; } = new List<string>();
        public List<string> ListMailTo { get; set; } = new List<string>();

    }
}
