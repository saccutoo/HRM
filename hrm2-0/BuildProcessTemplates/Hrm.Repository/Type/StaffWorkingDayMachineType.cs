using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class StaffWorkingDayMachineType : IUserDefinedType
    {
        public long Id { get; set; }
        public long StaffId { get; set; }
        public long WorkingDayMachineId { get; set; }
        public string SSN { get; set; }
        public string Note { get; set; }
        public bool IsDeleted { get; set; }
    }
}
