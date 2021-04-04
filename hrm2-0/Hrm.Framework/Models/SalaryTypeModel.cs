using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class SalaryTypeModel:BaseModel
    {
        public string Name { get; set; }
        public long StatusId { get; set; }
        public string Description { get; set; }
        public bool IsAutoLock { get; set; }
        public int DayLock { get; set; }
        public int Apply { get; set; }
        public int MaximumDay { get; set; }
        public List<string> ListOrganization { get; set; } = new List<string>();
    }
}
