using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class AnnualLeaveEntity :BaseEntity
    {
        public decimal AnnualLeave { get; set; }
        public decimal AnnualLeaveSeniority { get; set; }
        public decimal NumberOfLeaveGranted { get; set; }
        public long HandlingAnnualLeaveBacklog { get; set; }
        public DateTime? DateRemoveSurplusAnnualLeave { get; set; }
    }
}
