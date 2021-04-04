using Hrm.Repository.Entity;

namespace Hrm.Repository.Type
{
    public class WorkingdayAnnualLeaveStaffMapperType : IUserDefinedType
    {
        public long id { get; set; }
        public long StaffId { get; set; }
        public long AnnualLeaveId { get; set; }
    }
}
