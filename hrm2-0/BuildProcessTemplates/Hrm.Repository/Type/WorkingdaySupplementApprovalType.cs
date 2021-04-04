using Hrm.Repository.Entity;

namespace Hrm.Repository.Type
{
    public class WorkingdaySupplementApprovalType : IUserDefinedType
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public long RequestStatusId { get; set; }
        public long RequestActionId { get; set; }
        public string Note { get; set; }
    }
}
