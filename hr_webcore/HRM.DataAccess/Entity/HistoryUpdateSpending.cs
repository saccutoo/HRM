using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class HistoryUpdateSpending
    {
        public int AutoID { get; set; }
        public string AccountNumber { get; set; }
        public DateTime? Day { get; set; }
        public double? Amount { get; set; }
        public double? AmountVND { get; set; }
        public double? AmountChanged { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string AccountCurrencyCode { get; set; }
        public int? StatusAccount { get; set; }


    }
}
