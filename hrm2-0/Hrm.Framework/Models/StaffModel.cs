using System;
using System.Collections.Generic;

namespace Hrm.Framework.Models
{
    public class StaffModel : BaseModel
    {
        public StaffModel()
        {
            DataDropdownOfficePosition = new List<dynamic>();
            DataDropdownClassification = new List<dynamic>();
            Organizations = new List<dynamic>();
        }
        public string StaffCode { get; set; }
        public string Name { get; set; }
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
        public long PipelineId { get; set; }
        public long PipelineStepId { get; set; }
        public string PipelineRule { get; set; }
        public DateTime? OnboardingDate { get; set; }
        public string OnboardingNote { get; set; }
        public long OfficeId { get; set; }
        public bool IsSendWelcomeKit { get; set; }
        public bool IsSendChecklist { get; set; }
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
        public long Status { get; set; }
        public string Password { get; set; }

        public List<dynamic> DataDropdownOfficePosition { get; set; }
        public List<dynamic> DataDropdownClassification { get; set; }
        public List<dynamic> Organizations { get; set; }
        public bool IsActivated { get; set; }
        public long ProvinceId { get; set; }
        public string Domicile { get; set; }
        public long ProvinceDomicile { get; set; }
        public long NationalDomicile { get; set; }
        public bool IsOnboarding { get; set; }
        public List<dynamic> ListChecklist { get; set; }
        public long CurrencyId { get; set; }
        public bool isSendWelcomkit { get; set; }
        public string ImageAvataSrc { get; set; }
        public string NextRequestStatusId { get; set; }
    }
}