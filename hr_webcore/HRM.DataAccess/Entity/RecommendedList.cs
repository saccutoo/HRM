using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class RecommendedList
    {
        public int AutoID { set; get; }
        public int StaffID { set; get; }
        public int Type { set; get; }
        public string TypeName { set; get; }
        public string Date { set; get; }
        public DateTime? FromTime { set; get; }
        public DateTime? TimeProp { get; set; }
        public DateTime? ToTime { set; get; }
        public DateTime? HourOff { set; get; }
        public double DayOff { set; get; }
        public int Status { set; get; }
        public int? PercentPayrollID { set; get; }
        public string StatusName { set; get; }
        public string ReasonTypeName { set; get; }
        public string Note { set; get; }
        public DateTime? CreatedDate { set; get; }
        public string ManagerNote { set; get; }
        public string MonthVacation { set; get; }
        public string HRNote { set; get; }
        public string CustomerContactName { set; get; }
        public string CustomerReasonTypeName { set; get; }
        public string StaffName { get; set; }
    }
}
