using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class ShareRateForPerformanceReport
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int OrganizationUnitId { get; set; }
        public string OrganizationUnitName { get; set; }
        public float ShareRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Note { get; set; }
        public int PerformanceReportId { get; set; }
        public string PerformanceReportName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreateddBy { get; set; }






    }
}
