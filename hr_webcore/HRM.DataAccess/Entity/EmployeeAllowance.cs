using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class EmployeeAllowance
    {
        public int AutoID { get; set; }
        public int WPID { get; set; }
        public int DS_StaffID { get; set; }
        public int AllowanceID { get; set; }

        public string AllowanceName { set; get; }
        public string FullName { set; get; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public DateTime? StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public string CompanyName { set; get; }
    }

}
