using System;
using System.Collections.Generic;

namespace Hrm.Framework.Models
{
    public class PayrollModel : BaseModel
    {
        public PayrollModel()
        {

        }
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
    }
}
