using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class EmployeeBonus_Discipline
    {
        public int AutoID { get; set; }
        public string StaffCode { set; get; }
        public int Type { get; set; }
        public int GroupID { get; set; }
        public string DecisionNo { get; set; }
        public string Content { get; set; }
        public int ActionID { get; set; }
        public double Amount { get; set; }
        public int? CurrencyID { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? ApplyDate { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
