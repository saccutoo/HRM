using Hrm.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class WorkingProcessDetailViewModel : BaseViewModel
    {
        //working process
        public long StaffId { get; set; }
        public long WorkingprocessTypeId { get; set; }
        public string DecisionNo { get; set; }
        public long WorkingStatus { get; set; }
        public long Status { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
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
        public long HRManagerId { get; set; }
        public long HRAdditionId { get; set; }
        public long AdditionManagerId { get; set; }
        public string HRManagerName { get; set; }
        public string HRAdditionName { get; set; }
        public string AdditionManagerName { get; set; }
        public long PaymentForm { get; set; }
        public long PaymentMethod { get; set; }
        //contract
        public long ContractTypeId { get; set; }
        public long ContractTime { get; set; }
        public string ContractCode { get; set; }
        public string Name { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string ContractNote { get; set; }
        public long ContractStatus { get; set; }
        //Control
        public bool IsContract { get; set; }
        public bool IsPossition { get; set; }
        public bool IsSalary { get; set; }

    }
}