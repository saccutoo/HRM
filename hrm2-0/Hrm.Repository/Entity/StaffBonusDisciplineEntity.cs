using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class StaffBonusDisciplineEntity : BaseEntity
    {
        public long StaffId { get; set; }
        public long TypeId { get; set; }
        public long GroupId { get; set; }
        public string DecisionNo { get; set; }
        public string Content { get; set; }
        public long ActionId { get; set; }
        public decimal Amount { get; set; }
        public long CurrencyId { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? ApplyDate { get; set; }
        public string Note { get; set; }
        public long Status { get; set; }
        public bool PaySamePeriod { get; set; }
    }
}
