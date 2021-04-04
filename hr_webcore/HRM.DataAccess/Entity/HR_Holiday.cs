using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class HR_Holiday
    {
        public int HolidayID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Type { get; set; }
        public string Note { get; set; }
        public string TypeName { get; set; }
        public int WorkingDayMachineID { get; set; }
        public string WorkingDayMachineName { get; set; }
    }

}
