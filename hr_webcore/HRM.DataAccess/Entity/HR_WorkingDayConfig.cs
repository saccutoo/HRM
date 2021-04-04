using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class HR_WorkingDayConfig
    {

        public int AutoID { get; set; }
        public int? DateFromNumber { get; set; }
        public int? DateToNumber { get; set; }
        public int WorkingDayMachineID { get; set; }
        public DateTime? StartMonth { get; set; }
        public DateTime? EndMonth { get; set; }
        public bool? NoTimeChecking { get; set; }
        public string WorkingDayMachineName { get; set; }
    }
}
