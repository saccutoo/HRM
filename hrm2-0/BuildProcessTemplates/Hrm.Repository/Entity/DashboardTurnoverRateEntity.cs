using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class DashboardTurnoverRateEntity : BaseEntity
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long DatePeriod { get; set; }
        public float TurnoverRate { get; set; }
    }
}
