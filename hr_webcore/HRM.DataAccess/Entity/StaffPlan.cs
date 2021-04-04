using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity.UserDefinedType
{
    public class StaffPlan : IUserDefinedType
    {
        public int AutoID { get; set; }
        public int UserID { get; set; }
        public int? DS_OrganizationUnitID { get; set; }
        public int? CurrencyTypeID { get; set; }
        public int? Status { get; set; }
        public string Comment { get; set; }
        public int? Type { get; set; }
        public int? Year { get; set; }
        public int? ContractType { get; set; }
        public double? M1 { get; set; }
        public double? M2 { get; set; }
        public double? M3 { get; set; }
        public double? M4 { get; set; }
        public double? M5 { get; set; }
        public double? M6 { get; set; }
        public double? M7 { get; set; }
        public double? M8 { get; set; }
        public double? M9 { get; set; }
        public double? M10 { get; set; }
        public double? M11 { get; set; }
        public double? M12 { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string StatusName { get; set; }
        public string OrganizationUnitName { get; set; }
        public string CurrencyName { get; set; }
        public string StaffName { get; set; }
        public string ContractTypeName { get; set; }
        public double? SumValue { get; set; }
        public int StatusInput { get; set; }
        public string StatusInputName { get; set; }
        public string StaffCode { get; set; }
        public string OrganizationUnitCode { get; set; }

        public string Result { get; set; }

    }
}
