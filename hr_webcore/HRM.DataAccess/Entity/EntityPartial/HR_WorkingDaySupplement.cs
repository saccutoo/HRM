using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class HR_WorkingDaySummary
    {
        public string Fullname { set; get; }
        public string OrganizationUnitName { set; get; }
        public string StaffCode { set; get; }
        //public bool? HoldSaraly { set; get; }
    }
}
