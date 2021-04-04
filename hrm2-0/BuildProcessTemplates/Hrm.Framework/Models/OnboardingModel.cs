using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class OnboardingModel : BaseModel
    {
        public PipelineStepModel Pipeline { get; set; }
        public List<StaffModel> Staffs { get; set; }
    }
}
