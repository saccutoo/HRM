using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdayShiftModel : BaseModel
    {
        public long ShiftId { get; set; }
        public string Name { get; set; }
        public long PeriodId { get; set; }
        public string DatabaseName { get; set; }
        public bool IsOverNight { get; set; }
        public decimal LateAllowed { get; set; }
        public decimal EarlyAllowed { get; set; }
        public long Status { get; set; }
        public string PeriodName { get; set; }
        public bool IsDefault { get; set; }
        public long WorkId { get; set; }
        public TimeSpan? ToShift { get; set; }
        public TimeSpan? FromShift { get; set; }

    }
}
