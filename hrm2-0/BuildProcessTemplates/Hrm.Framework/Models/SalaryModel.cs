using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class SalaryModel :BaseModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public long PayrollId { get; set; }
        public long StaffId { get; set; }
        public long WorkingProcessId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal Netsalary { get; set; }
        public long CurentcyId { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        public decimal TotalWorkingday { get; set; }
        public decimal TotalPay { get; set; }
        public decimal TotalNotPay { get; set; }
        public decimal OrganizationId { get; set; }

    }
}
