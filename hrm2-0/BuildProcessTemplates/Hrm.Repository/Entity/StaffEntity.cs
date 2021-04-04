using System;

namespace Hrm.Repository.Entity
{
    public class StaffEntity : BaseEntity
    {
        public string StaffCode { get; set; }
        public DateTime? Birthday { get; set; }
        public long GenderId { get; set; }
        public long CurrentWorkingProcessId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ContactAddress { get; set; }
        public long WorkingStatus { get; set; }
        public long StatusId { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime? IdIssuedDate { get; set; }
        public string IdIssuedBy { get; set; }
        public string TaxId { get; set; }
        public DateTime? TaxDate { get; set; }
        public string TaxBy { get; set; }
        public string UserName { get; set; }
        public string BankNumber { get; set; }
        public string BankName { get; set; }
        public long NationalId { get; set; }
        public string LinkFacebook { get; set; }
        public string ImageLink { get; set; }
        public string EmailCompany { get; set; }
        public string PresentationContactName { get; set; }
        public string PresentationContactPhone { get; set; }
        public string Note { get; set; }
        public long ParentId { get; set; }
        public int NumberOfChild { get; set; }
        public long RoleId { get; set; }
        public long OfficePositionId { get; set; }
        public long OrganizationId { get; set; }
        public long ContractStatusId { get; set; }
        public long PipelineId { get; set; }
        public long PipelineStepId { get; set; }
        public DateTime? OnboardingDate { get; set; }
        public string OnboardingNote { get; set; }
        public long OfficeId { get; set; }
        public bool IsSendWelcomeKit { get; set; }
        public bool IsSendChecklist { get; set; }
        public DateTime? SendCheckListDate { get; set; }
        public long SendCheckListBy { get; set; }
        public int TotalCurrentChecklistChild { get; set; }
        public int TotalCurrentCheckListDetailCompleted { get; set; }
        public int TimeLateLimit { get; set; }
        public int TimeLeaveEarlyLimit { get; set; }
        public long TimekeepingForm { get; set; }
        public long HRAdditionId { get; set; }
        public long AdditionManagerId { get; set; }
        public string ManagerName { get; set; }
        public long ManagerOfficePositionId { get; set; }
        public long ManagerOrganizationId { get; set; }
        public long ManagerId { get; set; }
        public long ContractTypeId { get; set; }
        public long EthnicityId { get; set; }
        public string BankBranch { get; set; }
        public string BankAccount { get; set; }
        public string Skype { get; set; }
        public long MaritalStatus { get; set; }
        public string PhoneCompany { get; set; }
        public long ClassificationId { get; set; }
        public bool IsActivated { get; set; }
        public long Status { get; set; }
        public string Password { get; set; }
        public long ProvinceId { get; set; }
        public string Domicile { get; set; }
        public long ProvinceDomicile { get; set; }
        public long NationalDomicile { get; set; }
        public long CurrencyId { get; set; }
        public bool IsOnboarding { get; set; }
        public string NextRequestStatusId { get; set; }
    }
}
