using System;

namespace Hrm.Repository.Entity
{
    public class WorkingdayPeriodEntity : BaseEntity
    {
        public long AutoId { get; set; }
        public long FromDate { get; set; }
        public long ToDate { get; set; }
        public long FromDay { get; set; }
        public long Today { get; set; }
        public long MaximumEdition { get; set; }
        public string Note { get; set; }
        public bool IsDefault { get; set; }
        public long Status { get; set; }

    }
}
