using System;

namespace Hrm.Repository.Entity
{
   public class WorkingdayShiftEntity : BaseEntity
    {
        public long ShiftId { get; set; }
        public long PeriodId { get; set; }
        public string DatabaseName { get; set; }
        public bool IsOverNight { get; set; }
        public decimal LateAllowed { get; set; }
        public decimal EarlyAllowed { get; set; }
        public string PeriodName { get; set; }
        public long Status { get; set; }
        public bool IsDefault { get; set; }
        public long WorkId { get; set; }
        public TimeSpan ToShift { get; set; }
        public TimeSpan FromShift { get; set; }
    }
}
