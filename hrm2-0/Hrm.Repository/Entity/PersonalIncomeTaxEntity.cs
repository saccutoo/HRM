using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class PersonalIncomeTaxEntity : BaseEntity
    {
        public long Order { get; set; }
        public string Description { get; set; }
        public decimal DeductionItself { get; set; }
        public decimal TardinessReduction { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long Status { get; set; }
        public long CurrencyId { get; set; }
    }
}
