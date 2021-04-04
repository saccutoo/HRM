using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class ChecklistDetailModel:BaseModel
    {
        public long ChecklistId { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistNote { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ColumnLink { get; set; }
        public long ChecklistDetailTypeId { get; set; }
        public long TypeControlId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSendMail { get; set; }
        public long MailTemplateId { get; set; }
        public long ReceiverMailId { get; set; }
        public string Note { get; set; }
        public long ParentId { get; set; }
        public string ChecklistDetailTypeName { get; set; }
        public string ControlTypeName { get; set; }
        public bool IsFinished { get; set; }
        public long AssigneeId { get; set; }
        public string AssigneeName { get; set; }
        public long AssigneeViewId { get; set; }
        public long Index { get; set; }
        public long AssignId { get; set; }

        public List<dynamic>  ListTabelColumnId{ get; set; }
    }
}
