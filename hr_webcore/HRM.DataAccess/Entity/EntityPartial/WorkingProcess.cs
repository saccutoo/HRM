using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class WorkingProcess
    {
        public string WorkingStatusName { set; get; }
        public string StatusName { set; get; }
        public string OrganizationUnitName { set; get; }
        public string OfficeName { set; get; }
        public string OfficePositionName { set; get; }
        public string OfficeRoleName { set; get; }
        public string ManagerName { set; get; }
        public string HRNames { set; get; }
        public string CurrencyName { set; get; }
        public string StaffLevelName { set; get; }
        public string ContractTypeName { set; get; }
        public string CompanyName { set; get; }
        public double? Allowance { set; get; }
        public double? TotalSalary { get; set; }
        public List<EmployeeAllowance> EmployeeAllowanceList { set; get; }
        public List<EmployeeAllowance> EmployeeAllowanceDeleteList { set; get; }
        public string WPTypeName { set; get; }
        public int Iscopy { set; get; }
        public bool isShowSalary { set; get; }
        public double? AllowanceView { set; get; }
        public double? TotalSalaryView { set; get; }
        public  string FullName { set; get; }
        public string PolicyName { set; get; }
        public bool IsLatchWorkingDay { set; get; }
    }
}
