using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class SalaryWorkingday
    {
        public int AutoID { get; set; }
        public int StaffID { get; set; }
        public DateTime? Date { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int? Type { get; set; }
        public double? Standardworkingday { get; set; }
        public double? Workingday { get; set; }
        public double? OverTime { get; set; }
        public double? TotalHourDuration { get; set; }
        public double? BasicSalary { get; set; }
        public double? Bonus { get; set; }
        public string NoteAdjusted { get; set; }
        public string Fullname { set; get; }
        public double? WorkingAdjusted { set; get; }
    }
}
