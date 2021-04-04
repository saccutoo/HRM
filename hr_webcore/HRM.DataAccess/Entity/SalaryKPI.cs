using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity.UserDefinedType
{
    public class SalaryKPI: IUserDefinedType
    {
        public int AutoID { get; set; }
        public int StaffID { get; set; }
        public int OrganizationUnitID { get; set; }
        public int? Month { get; set; }
        public int? Quarter { get; set; }
        public int? Year { get; set; }
        public int KpiID { get; set; }
        public string KpiCode { get; set; }
        public string KpiValue { get; set; }
        public decimal? KpiAmount { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int PolicyBonusID { get; set; }
        public int ParentKpiID { get; set; }
        public int StatusInput { get; set; }
        public int IsDelete { get; set; }
        public string CreatedName { get; set; }
        public string ModifiedName { get; set; }
        public string StaffName { get; set; }
        public string OrganizationUnitName { get; set; }
        public string KPIName { get; set; }
        public string StaffCode { get; set; }
        public string OrganizationUnitCode { get; set; }
        public string PolicyBonusName { get; set; }
        public string ParentKpiName { get; set; }
        public string StatusInputName { get; set; }
        public int StatusOfUse { get; set; }
        public string StatusOfUseName { get; set; }

    }
}
