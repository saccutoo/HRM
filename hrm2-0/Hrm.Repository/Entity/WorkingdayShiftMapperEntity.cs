using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class WorkingdayShiftMapperEntity:BaseEntity
    {
        public long ShifId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal StandardWorkingday { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
