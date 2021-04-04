using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class WorkingdaySupplementConfigurationExceptionEntity : BaseEntity
    {
        public long StaffId { get; set; }
        public string ListStaffId { get; set; }
        public string StaffName { get; set; }
        public long OrganizationId { get; set; }
        public long PreApprovalStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public string Note { get; set; }
    }
}
