using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.Models
{
    public class AdditionalWorkModel
    {
        public long SupplementTypeId { get; set; }
        public long ReasonTypeId { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}