using Hrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class StaffWorkingDayMachineModel : BaseModel
    {
        public long OrderNo { get; set; }
        public long StaffId { get; set; }
        public long WorkingDayMachineId { get; set; }
        public string SSN { get; set; }
        public string Note { get; set; }
    }
}
