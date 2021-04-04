using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class WorkingProcessType : IUserDefinedType
    {
        public long Id { get; set; }
        public long StaffId { get; set; }
        public long WorkingprocessTypeId { get; set; }
        public string DecisionNo { get; set; }
        public long WorkingStatus { get; set; }
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
        public long HRManagerId { get; set; }
        public long PolicyId { get; set; }
        public decimal BasicPay { get; set; }
        public decimal EfficiencyBonus { get; set; }
        public long PaymentForm { get; set; }
        public long PaymentMethod { get; set; }
        public string SalaryNote { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public bool IsDeletedI { get; set; }
        public long Status { get; set; }
        public long ShiftId { get; set; }
        
    }
}
