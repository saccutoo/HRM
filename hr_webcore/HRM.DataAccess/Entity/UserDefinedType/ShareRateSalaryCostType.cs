using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity.UserDefinedType
{
    public class ShareRateSalaryCostType : IUserDefinedType
    {
        public Nullable<int> Id { get; set; }
        public Nullable<int> OrganizationUnitID { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<DateTime>  StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public Nullable<Double> ShareRate { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> Status { get; set; }
        public string Note { get; set; }
        public string Result { get; set; }
    }
}
