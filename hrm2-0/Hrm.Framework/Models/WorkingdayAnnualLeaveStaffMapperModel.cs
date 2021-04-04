using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdayAnnualLeaveStaffMapperModel : BaseModel
    {
        public long StaffId { get; set; }
        public long AnnualLeaveId { get; set; }
    }
}
