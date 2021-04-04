using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class PersonalIncomeTax
    {
        public int AutoID { get; set; }
        public int TaxNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public double? FromAmount { get; set; }
        public double? ToAmount { get; set; }
        public int CurrencyID { get; set; }
        public double ProgressiveTax { get; set; }
        public double RateTax { get; set; }
        public int CountryID { get; set; }
        public string Note { get; set; }
        public int Status1 { get; set; }
        public string StatusName { get; set; }
        public string CurrencyName { get; set; }
        public string CountryName { get; set; }

    }
}
