using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class HR_WorkingDay
    {
        public int WorkingDayID { get; set; }
        public int WorkingDayMachineID { get; set; }
        public string WorkingDayMachineName { get; set; }
        public TimeSpan MorningHourStart { get; set; }
        public TimeSpan? MorningHourMid { get; set; }
        public TimeSpan MorningHourEnd { get; set; }
        public TimeSpan AfernoonHourStart { get; set; }
        public TimeSpan? AfernoonHourMid { get; set; }
        public TimeSpan AfternoonHourEnd { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
