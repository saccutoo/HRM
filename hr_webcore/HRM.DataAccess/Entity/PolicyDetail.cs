using System;
using System.Collections.Generic;

namespace HRM.DataAccess.Entity
{
    public class PolicyDetail
    {
        public int Id { get; set; }
        public int? StaffLevelId { get; set; }
        public int?  PolicyID { get; set; }
        public decimal? StandardSpendingAmount { get; set; }
        public decimal? BasicSalaryTo{ get; set; }
        public decimal? BasicSalaryFrom { get; set; }
        public decimal? Margincompensation { get; set; }
        public decimal? EfficiencyBonus { get; set; }
        public decimal? Commission { get; set; }
        public decimal? TotalIncome { get; set; }
        public decimal? MinSpending { get; set; }
        public int? MinPerson { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SFormular { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? IsDelete { get; set; }
        public string StaffLevel { get; set; }
        public string PolicyName { get; set; }
        public int UserId { get; set; }
        public string UserCreatedName { get; set; }
        public string UserModifiedName { get; set; }
        public string SFormularCompensation { get; set; }
        public string SFormularAllowances { get; set; }
        public string StandardProbation { get; set; } // chuyển từ định mực thử việc -> công thức tính  KPI để tính quotarate 12/09/2019 lấy tạm trường StandardProbation.
        public string SFormularProbation { get; set; }
        public string SFormularBonus { get; set; }
        public string SFormularDecemberbonus { get; set; }
        public string SFormularKPIYear { get; set; }
    }
}
