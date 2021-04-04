using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class LogAdditionalWorkEntity : BaseEntity
    {
        public long Month { get; set; }
        public long Year { get; set; }
        public bool IsLock { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
