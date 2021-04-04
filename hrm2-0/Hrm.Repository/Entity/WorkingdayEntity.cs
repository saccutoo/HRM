using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class WorkingdayEntity : BaseEntity
    {
        public long StaffId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float Workingday { get; set; }
        public DateTime LateDuration { get; set; }
        public DateTime EarlyDuration { get; set; }
        public float WorkingdayFurlough { get; set; }
        public float WorkingdayOvertime { get; set; }
        public float WorkingdaySupplement { get; set; }
        public float WorkingdayLeave { get; set; }
        public float TotalWorkingday { get; set; }
        public float StandardWorkingday { get; set; }
        //information for summary workingday by month
        public string StaffName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public float TotalLateEarly { get; set; }
        public long TypeHolidayId { get; set; }
        public long WorkingdayTypeId { get; set; }
        public int CountWorkingdaySupplement { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
}
