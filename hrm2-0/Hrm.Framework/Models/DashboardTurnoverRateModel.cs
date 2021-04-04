using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class DashboardTurnoverRateModel : BaseModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long DatePeriod { get; set; }
        public float TurnoverRate { get; set; }
    }
}
