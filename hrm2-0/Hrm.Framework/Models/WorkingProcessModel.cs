using Hrm.Common;
using System;

namespace Hrm.Framework.Models
{
    public class WorkingProcessModel : BaseModel
    {
        public WorkingProcessModel()
        {
            CurrencyId = CurrentUser.CurrencyId;
        }
        public long StaffId { get; set; }
        public long WorkingprocessTypeId { get; set; }
        public string DecisionNo { get; set; }
        public long WorkingStatus { get; set; }
        public long Status { get; set; }
        public DateTime? DecisionDate  { get; set; }
        public DateTime? StartDate  { get; set; }
        public DateTime? EndDate  { get; set; }
        public long ContractId { get; set; }
        public long OrganizationId { get; set; }
	    public long OfficeId { get; set; }
	    public long PositionId { get; set; }
	    public long ClassificationId { get; set; }
	    public long StaffLevelId { get; set; }
	    public long ManagerId { get; set; }
	    public long PolicyId { get; set; }
	    public long CurrencyId { get; set; }
        public decimal BasicPay { get; set; }
        public decimal EfficiencyBonus { get; set; }
	    public string Note { get; set; }
        public string SalaryNote { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public long ShiftId { get; set; }
        public long ContractTypeId { get; set; }
        public int ContractTime { get; set; }
        public long HRManagerId { get; set; }
        public string HRManagerName { get; set; }
        public string ManagerName { get; set; }
        public string HRAdditionName { get; set; }
        public string AdditionManagerName { get; set; }
        public long PaymentForm { get; set; }
        public long PaymentMethod { get; set; }
        public bool IsPossition { get; set; }
        public bool IsSalary { get; set; }
    }
}
