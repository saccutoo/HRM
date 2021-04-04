using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class PipelineModel : BaseModel
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public long MenuId { get; set; }
        public string Description { get; set; }
        public long PositionId { get; set; }
        public long PipelineStepId { get; set; }
        public string PipelineStepName { get; set; }

    }
}
