using HRM.DataAccess.Entity.UserDefinedType;

namespace HRM.DataAccess.Entity
{
    public class StaffPlanFundRate : IUserDefinedType
    {
        public int AutoID { get; set; }
        public int StaffID { get; set; }
        public int? DS_OrganizationUnitID { get; set; }
        public int? CurrencyTypeID { get; set; }
        public int Status { get; set; }
        public int Year { get; set; }
        public decimal? Q1 { get; set; }
        public decimal? Q2 { get; set; }
        public decimal? Q3 { get; set; }
        public decimal? Q4 { get; set; }
        public string Description { get; set; }
    }
}
