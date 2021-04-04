using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdaySupplementConfigurationModel : BaseModel
    {
        public long RequesterId { get; set; }
        public long PrevStatus { get; set; }
        public long NextStatus { get; set; }
        public long Action { get; set; }
        public long IsLastStep { get; set; }
        public bool IsLastStepChecked { get; set; }
    }
    public class WorkingdaySupplementConfigurationExceptionModel : BaseModel
    {
        public long StaffId { get; set; }
        public string StaffName { get; set; }
        public long OrganizationId { get; set; }
        public long PreApprovalStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public string Note { get; set; }
        public string ListStaffId { get; set; }
        public List<string> ListStaffApply { get; set; } = new List<string>();
        public List<string> ListApprovedBy { get; set; } = new List<string>();
    }
}
