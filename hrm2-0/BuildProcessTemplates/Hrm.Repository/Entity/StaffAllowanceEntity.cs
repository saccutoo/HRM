using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class StaffAllowanceEntity : BaseEntity
    {
        public long WorkingProcessId { get; set; }
        public long StaffId { get; set; }
        public long AllowanceType { get; set; }
        public long CurrencyId { get; set; }
        public long Status { get; set; }
        public decimal Amount { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long OrderNo { get; set; }
        public bool TaxExemption { get; set; }
    }
}
