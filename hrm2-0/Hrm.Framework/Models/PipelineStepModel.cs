using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class PipelineStepModel : BaseModel
    {
        public string PipelineStepName { get; set; }
        public long PositionId { get; set; }
        public string PipelineStepSymbol { get; set; }
        public string PipelineRule { get; set; }
        public long PipelineId { get; set; }
        public long OrderNo { get; set; }
        public string PositionName { get; set; }
        public string [] ListStringPipelineRule { get; set; }
        public DateTime? PipelineDate { get; set; }
        public long PipelineStaffStatusId { get; set; }

    }
}
