using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity.UserDefinedType
{
    public class HR_WorkingDaySupplementSaveListType: IUserDefinedType
    {
        public int AutoID { get; set; }
        public int StaffID { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public DateTime? FullHourOff { get; set; }
        public DateTime? HourOff { get; set; }
        public DateTime? TimeOfActual { get; set; }
        public double? DayOff { get; set; }
        public string MonthVacation { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? SuperiorBy { get; set; }
        public DateTime? SuperiorDate { get; set; }
        public int? ConfirmBy { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public string ManagerNote { get; set; }
        public string HRNote { get; set; }
        public int ReasonType { get; set; }
        public int? CustomerID { get; set; }
        public int? PercentPayrollID { get; set; }
        public int? OldId { get; set; }
        public int? CustomerContactID { get; set; }
        public int? CustomerReasonType { get; set; }
        public string StaffName { get; set; }
        public string StatusName { get; set; }
        public string ReasonTypeName { get; set; }
        public string TypeName { get; set; }
        public string CustomerContactName { get; set; }
        public string CustomerReasonTypeName { get; set; }
    }
}
