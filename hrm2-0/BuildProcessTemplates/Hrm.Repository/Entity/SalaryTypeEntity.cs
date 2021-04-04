using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class SalaryTypeEntity : BaseEntity
    {
        public long StatusId { get; set; }
        public string Description { get; set; }
        public bool IsAutoLock { get; set; }
        public int DayLock { get; set; }
        public int Apply { get; set; }
        public int MaximumDay { get; set; }
    }
}
