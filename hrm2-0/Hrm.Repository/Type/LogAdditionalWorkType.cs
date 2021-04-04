using Hrm.Repository.Entity;
using System;

namespace Hrm.Repository.Type
{
    public class LogAdditionalWorkType : IUserDefinedType
    {
        public long ShiftId { get; set; }
        public long Month { get; set; }
        public long Year { get; set; }
        public bool IsLock { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
