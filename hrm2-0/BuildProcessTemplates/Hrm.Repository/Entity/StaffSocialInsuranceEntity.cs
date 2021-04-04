using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class StaffSocialInsuranceEntity : BaseEntity
    {
        public long StaffId { get; set; }
        public long TypeId { get; set; }
        public string InsuranceCode { get; set; }
        public string InsuranceNumber { get; set; }
        public string FamilyCode { get; set; }
        public string Note { get; set; }
        public string PlaceHold { get; set; }
        public DateTime? MonthStart { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long Status { get; set; }
        public DateTime? DateReturn { get; set; }
        public decimal UnionAmount { get; set; }
        public long CurrencyId { get; set; }
        public decimal BasicSalary { get; set; }
        public float RateCompany { get; set; }
        public float RatePerson { get; set; }
    }
}
