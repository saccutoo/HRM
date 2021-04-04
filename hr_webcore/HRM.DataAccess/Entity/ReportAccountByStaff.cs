using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class ReportAccountByStaff
    {
        public string OrganizationUnitName { get; set; }
        public string Department { get; set; }
        public string StaffName { get; set; }
        public double? VIP { get; set; }
        public double? Advanced { get; set; }
        public double? Standard { get; set; }
        public double? Substandard { get; set; }
        public double? Invalid { get; set; }
        public double? VIPS { get; set; }
        public double? AdvancedS { get; set; }
        public double? StandardS { get; set; }
        public double? SubstandardS { get; set; }
        public double? InvalidS { get; set; }
        public double? VIPC { get; set; }
        public double? AdvancedC { get; set; }
        public double? StandardC { get; set; }
        public double? AccountsConvertedBySpending { get; set; }
        public double? FeeAmount { get; set; }
        public double? Margin { get; set; }
        public double? AccountsConvertedByMargin { get; set; }
        public double? TotalAccountsConverted { get; set; }
        public double? RateACBSPerTAC { get; set; }
        public double? RateOfMarginFeePerTotalMargin { get; set; }






    }
}
