using HRM.DataAccess.Entity.UserDefinedType;

namespace HRM.DataAccess.Entity
{
    public class SpendingAdjustmentRate : IUserDefinedType
    {
        public long Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public float ReduceRate { get; set; }
        public float QuotaRate { get; set; }
    }
}
