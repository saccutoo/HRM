using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class HR_WorkingDaySummary
    {
        public int AutoID { get; set; }
        public int StaffID { get; set; }
        public System.DateTime? Date { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public double? WorkingDay { get; set; }
        public double? Furlough { get; set; }
        public System.DateTime? LateDuration { get; set; }
        public System.DateTime? EarlyDuration { get; set; }
        public double? Overtime { get; set; }
        public double? OvertimeRate { get; set; }
        public double? TotalWorkingDay { get; set; }
        public double? WorkingDaySupplement { get; set; }
        public double? WorkingDayLeave { get; set; }
        public virtual Staff Staff { get; set; }
        public double? DaysOfMonth { get; set; }
        public string DayOfWeeks { get; set; }
        public double? DayWork { get; set; }
        public int DayNumber { get; set; }
        public string HourCheckIn { get; set; }
        public string HourCheckOut { get; set; }
        public string StartTimeSupplement { get; set; }
        public string EndTimeSupplement { get; set; }
        public string HourLate { get; set; }
        public string HourEarly { get; set; }
        public string TotalHourDuration { set; get; }
        public double? WorkingAdjusted { set; get; }
        public string NoteAdjusted { set; get; }
    }
}
