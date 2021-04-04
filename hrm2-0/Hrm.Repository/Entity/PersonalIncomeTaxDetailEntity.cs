using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class PersonalIncomeTaxDetailEntity : BaseEntity
    {
        public long TaxLevel { get; set; }
        public long PersonalIncomeTaxId { get; set; }
        public decimal FromAmount { get; set; }
        public string AmountRange { get; set; }
        public decimal ToAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal ProgressiveAmount { get; set; }
        public decimal SubtractAmount { get; set; }
    }
}
