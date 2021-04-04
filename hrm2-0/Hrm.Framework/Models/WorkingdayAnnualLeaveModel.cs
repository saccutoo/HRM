using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdayAnnualLeaveModel : BaseModel
    {
        public long AutoId { get; set; }
        public string Name { get; set; }
        public long StaffId { get; set; }
        public decimal AnnualLeave { get; set; }
        public long TypeId { get; set; }
        public long RegimeId { get; set; }
        public long PeriodApplyId { get; set; }
        public long Status { get; set; }
        public DateTime DateApply { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Note { get; set; }
        public long TotalShare { get; set; }
    }
}
