using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class StaffWorkingDayMachineEntity : BaseEntity
    {
        public long OrderNo { get; set; }
        public long StaffId { get; set; }
        public long WorkingDayMachineId { get; set; }
        public string SSN { get; set; }
        public string Note { get; set; }
    }
}
