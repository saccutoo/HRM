using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Employee1
    {
        public string StaffCode { set; get; }
        public string OrganizationUnit { set; get; }
        public string Fullname { set; get; }
        public  string OfficeName { set; get; }
        public string OfficePositionName { set; get; }
        public string Rank { set; get; }
        public string Status { set; get; }
        public DateTime? StartWorkingDate { set; get; }
        public DateTime? OfficialDate { set; get; }
        public DateTime? BirthDay { set; get; }
        public string Gender { set; get; }
        public string Mobile { set; get; }
        public string Address { set; get; }
        public string ContactAddress { set; get; }
        public string IdentityNumber { set; get; }
        public DateTime? IDIssuedDate { set; get; }
        public string IDIssuedBy { set; get; }
        public string EmailCompany { set; get; }
        public string EmailPersonal { set; get; }
        public string CCEmail { set; get; }
        public string BankNumber { set; get; }
        public string AccountName { set; get; }
        public string BankName { set; get; }
        public double? SeniorityAllowances { set; get; }
        public DateTime? SeniorityAllowancesDate { set; get; }
        public string EndWorkingDate { set; get; }
    }
}
