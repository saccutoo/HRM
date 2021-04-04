using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class StaffOnboardInfoEntity : BaseEntity
    {
        public bool IsOnboarding { get; set; }
        public long StaffId { get; set; }
        public long PipelineId { get; set; }
        public long PipelineStepId { get; set; }
        public DateTime? OnboardingDate { get; set; }
    }
}
