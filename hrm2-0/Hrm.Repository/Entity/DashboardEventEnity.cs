namespace Hrm.Repository.Entity
{
  public  class DashboardEventEnity: BaseEntity
    {
        public long StaffId { get; set; }
        public string StaffName { get; set; }
        public string OrganizationName { get; set; }
        public bool IsBirthday { get; set; }
        public bool IsOnboard { get; set; }
    }
}
