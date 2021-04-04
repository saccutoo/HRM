using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdayCheckInOutModel : BaseModel
    {
        public long StaffId { get; set; }
        public long SSN { get; set; }
        public long WorkingDayMachineId { get; set; }
        public long TypeHolidayId { get; set; }
        public DateTime CheckTime { get; set; }
        public string CheckType { get; set; }
        public string Emotion { get; set; }
        public string ImageUrl { get; set; }
        public string DbName { get; set; }
    }
}
