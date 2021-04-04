using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdaySupplementModel : BaseModel
    {
        public long StaffId { get; set; }
        public long SupplementTypeId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? MissingTimeDuration { get; set; }
        public decimal OvertimeRate { get; set; }
        public long ReasonTypeId { get; set; }
        public string Note { get; set; }
        public bool IsCompleted { get; set; }
        public string StaffName { get; set; }
        public long RequestStatus { get; set; }
        public long RequestStatusId { get; set; }
        public string MissingTime { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string ApprovedByName { get; set; }
        public long OrganizationId { get; set; }
        public decimal OutValue { get; set; }
        public bool IsApproval { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public decimal TotalWorkingday { get; set; }
        public string Confirm { get; set; }


    }
}
