using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdayAllStaffModel :BaseModel
    {
        public float StandardWorkingday { get; set; }
        public float TotalWorkingday { get; set; }
        public float Workingday { get; set; }
        public float WorkingdayOvertime { get; set; }
        public float WorkingdayAnnualLeave { get; set; }
        public float WorkingdaySupplement { get; set; }
        public float WorkingdayUnpaidLeave { get; set; }
        public float OtherWorkingday { get; set; }
        public float TotalLateEarly { get; set; }
        public string Name { get; set; }
    }
}
