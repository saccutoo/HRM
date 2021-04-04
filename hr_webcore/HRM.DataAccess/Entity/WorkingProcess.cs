using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class WorkingProcess
    {
        public int WPID { get; set; }
        public int StaffID { get; set; }
        public int? WPTypeID { get; set; }
        public int WorkingStatus { get; set; }
        public string DecisionNo { get; set; }
        public int Status { get; set; }
        public DateTime WPStartDate { get; set; }
        public DateTime? WPEndDate { get; set; }
        public int? CompanyID { get; set; }
        public int OrganizationUnitID { get; set; }
        public int? OfficeID { get; set; }
        public int? OfficePositionID { get; set; }
        public int? OfficeRoleID { get; set; }
        public int? StaffLevelID { get; set; }
        public int? ContractTypeID { get; set; }
        public string ContractNo { get; set; }
        public DateTime? StartDateContract { get; set; }
        public DateTime? EndDateContract { get; set; }
        public int? ManagerID { get; set; }
        public string HRIDs { get; set; }
        public int? PolicyID { get; set; }
        public int? CurrencyID { get; set; }
        public double? BasicPay { get; set; }
        public double? EfficiencyBonus { get; set; }
        public string WPNote { get; set; }

        public bool Lock { set; get; }
        public int TeamLeadLevelID { set; get; }
    }
}
