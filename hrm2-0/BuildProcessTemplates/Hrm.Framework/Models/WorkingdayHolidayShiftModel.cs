using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdayHolidayShiftModel :BaseModel
    {
        public long ShiftId { get; set; }
        public long HolidayId { get; set; }

    }
}
