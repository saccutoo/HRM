using Hrm.Repository.Entity;
using System;

namespace Hrm.Repository.Type 
{
    public class WorkingdaySupplementType : IUserDefinedType
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public long SupplementTypeId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? MissingTimeDuration { get; set; }
        public long ReasonTypeId { get; set; }
        public float? OvertimeRate { get; set; }
        public string Note { get; set; }
        public long RequestStatusId { get; set; }
        public long StaffId { get; set; }

    }
}
