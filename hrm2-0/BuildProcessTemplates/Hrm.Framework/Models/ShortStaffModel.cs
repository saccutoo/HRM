using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class ShortStaffModel : BaseModel 
    {
        public long StaffId { get; set; }
        public string StaffCode { get; set; }
        public string Name { get; set; }
        public string PhoneCompany { get; set; }
        public string EmailCompany { get; set; }
        public bool IsOnboarding { get; set; }
        public DateTime? OnboardingDate { get; set; }
        public long ClassificationId { get; set; }
        public long OrganizationId { get; set; }
        public string Note { get; set; }
    }
}
