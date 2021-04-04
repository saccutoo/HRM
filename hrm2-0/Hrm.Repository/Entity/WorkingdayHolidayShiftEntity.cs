using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class WorkingdayHolidayShiftEntity:BaseEntity
    {
        public long ShiftId { get; set; }
        public long HolidayId { get; set; }
    }
}
