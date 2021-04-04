using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class PipelineStepType: IUserDefinedType
    {
        public long Id { get; set; }
        public string PipelineStepName { get; set; }
        public long PositionId { get; set; }
        public string PipelineStepSymbol { get; set; }
        public string PipelineRule { get; set; }
        public long PipelineId { get; set; }
        public long OrderNo { get; set; }
    }
}
