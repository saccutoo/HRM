using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class ChecklistDetailType : IUserDefinedType
    {
        public long Id { get; set; }
        public long ChecklistId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ColumnLink { get; set; }
        public long ChecklistDetailTypeId { get; set; }
        public long TypeControlId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSendMail { get; set; }
        public long MailTemplateId { get; set; }
        public string Note { get; set; }
        public long ParentId { get; set; }
        public long ReceiverMailId { get; set; }
        public long AssigneeId { get; set; }
        public long AssigneeViewId { get; set; }
        public long Index { get; set; }
        public long AssignId { get; set; }
        public bool IsFinished { get; set; }

    }
}
