using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class Salary
    {
        public string AmountEncode { get; set; }
        public double Amount { set; get; }
        public string MonthYear { set; get; }
        public string OrganizationUnitName { set; get; }
        public string OfficePositionName { set; get; }
        public string StaffCode { set; get; }
        public string EmailPersonal { set; get; }
        public string AccountNumber { set; get; }
        public string AccountName { set; get; }
        public string BankName { set; get; }
        public string CompanyName { set; get; }
        public double? TotalOtherAllowances { set; get; }
        public double? TotalSalary { set; get; }
        public string FullName { set; get; }
        public string OrganizationUnitCode { set; get; }
        
        public double? Remain { set; get; }
        public string EmployeeStatus { set; get; }
        public string cc { set; get; }
        public int? HoldSalary { set; get; }
        public string StatusContractName { set; get; }
        public string StaffLevelName { set; get; }
        public string PolicyName { set; get; }
        public string NamePB { set; get; }
        public string Currency { set; get; }
        public string ParentName { set; get; }
        public int OrganizationUnitID { set; get; }
        public int WPID { set; get; }
    }
}
