using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class ReportSpendingByAccountNumber
    {
        public string CID { get; set; }
        public string MCC { get; set; }
        public string Unit { get; set; }
        public float Spending { get; set; }
        public float SpendingExclude { get; set; }
        public float SpendingBDAut { get; set; }
        public float Eligible { get; set; }
        public DateTime? StartDateExclude { get; set; }
        public DateTime? DateLink { get; set; }
        public DateTime? EndDateExclude { get; set; }
        public string BD { get; set; }
        public string OrganizationUnit { get; set; }
    }
}
