using Hrm.Common;
using System;

namespace Hrm.Framework.Models
{
    public class StaffBonusDisciplineModel : BaseModel
    {
        public StaffBonusDisciplineModel()
        {
            CurrencyId = CurrentUser.CurrencyId;
        }
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
        public long FileId { get; set; }
        public bool PaySamePeriod { get; set; } = false;
        public string FileName { get; set; }
        public bool IsDeletedFile { get; set; } = false;
    }
}
