using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class ShortStaffEntity : BaseEntity
    {
        public long StaffId { get; set; }
        public long StaffCode { get; set; }
        public long PhoneCompany { get; set; }
        public long EmailCompany { get; set; }
        public long OnboardingDate { get; set; }
        public long OfficePositionId { get; set; }
        public long OrganizationId { get; set; }
        public long Note { get; set; }
    }
}
