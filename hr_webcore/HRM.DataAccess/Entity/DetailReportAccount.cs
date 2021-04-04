using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class DetailReportAccount
    {
        public int CustomerId { set; get; }
        public string CustomerCode { set; get; }
        public int StaffID { set; get; }
        public int MCCAccountID { set; get; }
        public Int64 AccountNumber { set; get; }
        public int AccountLevelId { set; get; }
        public DateTime FirstDateLinked { set; get; }
        public  DateTime PayDate { set; get; }
        public int AccountTypeId { set; get; }
        public bool IsPartner { set; get; }
        public DateTime LastTimeChangeStatus { set; get; }
        public int StatusId { set; get; }
        public string BD { set; get; } // BD (y.fullname)
        public int OrganizationUnitID { set; get; }
        public string Department { set; get; } //Department(z.name)
        public string StatusAccount { set; get; } //StatusAccount(m.Name)
        public DateTime TrialDate { set; get; }
        public string AccountLevel { set; get;}//AccountLevel(t.Name)
        public string AccountType { set; get; }//AccountType(n.Name)
        public double AccountConversion { set; get; } //dbo.F_CAL_AccountConversion_By_AccountType(n.Name,x.T) erp_v2
        public string Email { set; get; }
        public double T { set; get; }
    }
}
