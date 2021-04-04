using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.Models
{
    public class AdditionalWorkOverTimeModel
    {
        public long SupplementTypeId { get; set; }
        public long ReasonTypeId { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public float? OvertimeRate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}