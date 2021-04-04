using System;

namespace Hrm.Repository.Entity
{
   public class WorkingdayFurlough:BaseEntity
    {
        public DateTime?  StartDate{ get; set; }
        public float AnnualLeave { get; set; }
        public float FurloughSeniority { get; set; }
        public float AnnualTotal { get; set; }
        public float AnnualLeaveLastYear { get; set; }
        public float AnnualLeaveOther { get; set; }
        public float AnnualLeaveUse { get; set; }
        public float AnnualLeaveRemain { get; set; }
    }
}
