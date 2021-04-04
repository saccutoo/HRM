using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class SocialInsuranceDetail
    {
        public int AutoID { get; set; }
        public int InsuranceID { get; set; }
        public int StaffID { get; set; }
        public DateTime? FromMonth { get; set; }
        public DateTime? ToMonth { get; set; }
        public int Status { get; set; }
        public string PlaceHealthCare { get; set; }
        public string Regime { get; set; }
        public string Note { get; set; }
        public double? BasicSalary { get; set; }
        public double? RateCompany { get; set; }
        public double? RatePerson { get; set; }
        public DateTime? DateReturn { set; get; }
        public int ApproveStatus { set; get; }
        public string ApproveStatusName { set; get; }
    }
}
