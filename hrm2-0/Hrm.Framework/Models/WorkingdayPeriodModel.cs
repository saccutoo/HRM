using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdayPeriodModel : BaseModel
    {
        public long AutoId { get; set; }
        public string Name { get; set; }
        public long FromDay { get; set; }
        public long Today { get; set; }
        public string Note { get; set; }
    }
}
