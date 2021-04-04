using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class DashboardSummaryEntity : BaseEntity
    {
        public float Total { get; set; }
        public float TotalOff { get; set; }
        public float Official { get; set; }
        public float Onboard { get; set; }
        public float TotalPeriod { get; set; }
        public float TotalOffPeriod { get; set; }
        public float OfficialPeriod { get; set; }
        public float OnboardPeriod { get; set; }
    }
}
