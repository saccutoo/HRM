using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdayHolidayModel: BaseModel
    {
        public string Name { get; set; }
        public long ClassifyId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? MaxDate { get; set; }
        public DateTime? MinDate { get; set; }
        public string ApproxDate { get; set; }
        public bool IsAnnualLeave { get; set; }
        public long SalaryRegimeId { get; set; }
        public long DesistRegimeId { get; set; }
        public float OvertimeRate { get; set; }
        public string Note { get; set; }
        public string DateFormat { get; set; }
        public long HolidayMapperId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<string> ListShift { get; set; } = new List<string>();
    }
}
