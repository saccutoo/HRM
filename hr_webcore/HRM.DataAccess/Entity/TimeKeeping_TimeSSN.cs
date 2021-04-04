using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class TimeKeeping_TimeSSN
    {
        public string DayOfWeeks { set; get; }
        public DateTime? CheckTime { set; get; }
        public string HourCheckIn { set; get; }
        public string HourCheckOut { set; get; }
        public float DayWork { set; get; }
        public string HourLate { set; get; }
        public string HourEarly { set; get; }
        public int DayNumber { set; get; }
    }
}
