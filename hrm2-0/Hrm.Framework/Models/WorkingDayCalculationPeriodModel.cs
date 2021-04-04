using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingDayCalculationPeriodModel : BaseModel
    {
        public long AutoId { get; set; }
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public long MaximumEdition { get; set; }
        public string Note { get; set; }
        public bool IsDefault { get; set; }
        public long Status { get; set; }
    }
}
