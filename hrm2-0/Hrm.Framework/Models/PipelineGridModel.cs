using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
   public class PipelineGridModel
    {
        public List<PipelineStepModel> PipelineSteps { get; set; }
        public long CurrentStep { get; set; }
    }
}
