using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class WorkingdaySupplementConfigurationEntity : BaseEntity
    {
        public long RequesterId { get; set; }
        public long PrevStatus { get; set; }
        public long NextStatus { get; set; }
        public long Action { get; set; }
        public long IsLastStep { get; set; }
    }
}
