using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class PipelineEntity:BaseEntity
    {
        public long MenuId { get; set; }
        public bool IsDefault { get; set; }
        public string Description { get; set; }
        public long PositionId { get; set; }
        public long PipelineStepId { get; set; }
        public string PipelineStepName { get; set; }

    }
}
