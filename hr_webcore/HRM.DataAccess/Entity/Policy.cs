using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Policy
    {
        public Policy()
        {
            ListSpendingAdjustmentRate = new List<SpendingAdjustmentRate>();
        }
        public int PolicyID { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedOn { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Modifiedby { get; set; }
        public string Note { get; set; }
        public string StatusName { get; set; }
        public int PolicyincludeID { get; set; }
        public string PolicyIncludeName { get; set; }
        public int [] ListKPI { get; set; }
        public string KPI { get; set; }
        public int PolicyBonusID { get; set; }
        public string PolicyBonusName { get; set; }
        public List<SpendingAdjustmentRate> ListSpendingAdjustmentRate { get; set; }

    }
}
