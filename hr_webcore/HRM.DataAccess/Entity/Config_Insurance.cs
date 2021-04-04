using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Config_Insurance
    {

        public int AutoID { get; set; }
        public int InsuranceTypeID { get; set; }
        public string InsuranceTypeName { get; set; }
        public string DecisionCode { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ApplyDate { get; set; }

        public double RateCompany { get; set; }
        public double RatePerson { get; set; }

        public double Total { get; set; }
        public DateTime EndDate { get; set; }
        public string Note { get; set; }
        public string CreatedName { get; set; }
    }
}
