using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class AnnualLeaveModel : BaseModel
    {
        public decimal AnnualLeave { get; set; }
        public decimal AnnualLeaveSeniority { get; set; }
        public decimal NumberOfLeaveGranted { get; set; }
        public long HandlingAnnualLeaveBacklog { get; set; }
        public DateTime? DateRemoveSurplusAnnualLeave { get; set; }
    }
}
