using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class ReportIncreaseSpending
    {
        public string OrganizationUnitName { get; set; }
        public string StaffName { get; set; }
        public double IncreaseSpending { get; set; }
        public double CompletionRate { get; set; }
        public double ShortfallComparedTo30 { get; set; }
        public double SpendingIncreasedStandard { get; set; }
        public double AssignedSpending { get; set; }
        public double SpendingInMonth { get; set; }
        public string StaffStatus { get; set; }

    }
}
