using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdayHolidayMapperModel:BaseModel
    {
        public long HolidayId { get; set; }
        public long DesistRegimeId { get; set; }
        public DateTime? Date { get; set; }
    }
}
