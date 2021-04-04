using Hrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class StaffBenefitsModel : BaseModel
    {
        public StaffBenefitsModel()
        {
            CurrencyId = CurrentUser.CurrencyId;
        }
        public long WorkingProcessId { get; set; }
        public long StaffId { get; set; }
        public long BenefitType { get; set; }
        public long Status { get; set; }
        public long CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long OrderNo { get; set; }
    }
}
