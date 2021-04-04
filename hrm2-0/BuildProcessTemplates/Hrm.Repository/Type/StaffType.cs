using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class StaffType : IUserDefinedType
    {
        public long Id { get; set; }
        public string StaffCode { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public long GenderId { get; set; }
        public long CurrentWorkingProcessId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhoneCompany { get; set; }
        public string Skype { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime? IdIssuedDate { get; set; }
        public string IdIssuedBy { get; set; }
        public string TaxId { get; set; }
        public DateTime? TaxDate { get; set; }
        public string TaxBy { get; set; }
        public string UserName { get; set; }
        public string BankNumber { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string Address { get; set; }
        public string ContactAddress { get; set; }
        public long NationalId { get; set; }
        public long ProvinceId { get; set; }
        public string Domicile { get; set; }
        public long ProvinceDomicile { get; set; }
        public long NationalDomicile { get; set; }
        public string LinkFacebook { get; set; }
        public string ImageLink { get; set; }
        public string EmailCompany { get; set; }
        public string PresentationContactName { get; set; }
        public string PresentationContactPhone { get; set; }
        public string Note { get; set; }
        public bool IsSendChecklist { get; set; }
        public DateTime? SendCheckListDate { get; set; }
        public long SendCheckListBy { get; set; }
        public int TimeLateLimit { get; set; }
        public int TimeLeaveEarlyLimit { get; set; }
        public long TimekeepingForm { get; set; }
        public long HRAdditionId { get; set; }
        public long AdditionManagerId { get; set; }
        public bool IsDeleted { get; set; }
        public long EthnicityId { get; set; }
        public long MaritalStatus { get; set; }
        public bool IsActivated { get; set; }
        public long Status { get; set; }
        public long CurrencyId { get; set; }
        public string Password { get; set; }
    }
}
