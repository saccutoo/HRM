using Hrm.Repository.Entity;
using System;

namespace Hrm.Repository.Type
{
    public class PayrollType : IUserDefinedType
    {
        public long Id { get; set; }
        public long SalaryTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MonthYear { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public long Status { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int TotalStaff { get; set; }
        public decimal TotalAmount { get; set; }
        public long OrganizationId { get; set; }
        public string CreatedName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
