using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.Models
{
    public class PleaseLateEarlyModel
    {
        public DateTime? Date { get; set; }
        public string MissingTime { get; set; }
        public long SupplementTypeId { get; set; }
        public long ReasonTypeId { get; set; }
    }
}