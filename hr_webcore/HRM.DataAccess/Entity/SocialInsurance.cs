using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class SocialInsurance
    {
        public int InsuranceID { get; set; }
        public int StaffID { get; set; }
        public string InsuranceCode { get; set; }
        public string InsuranceNumber { get; set; }
        public string HealthNumber { get; set; }
        public string FamilyCode { get; set; }
        public DateTime? MonthStart { get; set; }
        public string PlaceHold { get; set; }
    }
}
