using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class WorkingdayCheckInOutEntity : BaseEntity
    {
        public long StaffId { get; set; }
        public long SSN { get; set; }
        public long WorkingDayMachineId { get; set; }
        public long TypeHolidayId { get; set; }
        public string DBName { get; set; }
        public DateTime CheckTime { get; set; }
        public string CheckType { get; set; }
        public string Emotion { get; set; }
        public string ImageUrl { get; set; }

    }
}
