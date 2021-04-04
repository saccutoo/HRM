using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class PersonalIncomeTaxModel : BaseModel
    {
        public string Description { get; set; }
        public decimal DeductionItself { get; set; }
        public decimal TardinessReduction { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long Status { get; set; }
        public long CurrencyId { get; set; }
    }
}
