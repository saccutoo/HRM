using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class StaffBenefitsType : IUserDefinedType
    {
        public long Id { get; set; }
        public long WorkingProcessId { get; set; }
        public long StaffId { get; set; }
        public long BenefitType { get; set; }
        public decimal Amount { get; set; }
        public long CurrencyId { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public long Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
