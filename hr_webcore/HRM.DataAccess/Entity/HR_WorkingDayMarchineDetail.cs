using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class HR_WorkingDayMarchineDetail
    {
        public int AutoID { get; set; }
        public int WorkingDayMachineID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public bool? WorkingType { get; set; }
        public double? Timekeeping { get; set; }
        public string Name { get; set; }
        public string StatusName { get; set; }
    }

}
