using Hrm.Framework.Models;
using System.Collections.Generic;

namespace Hrm.Web.ViewModels
{
    public class OnboardingColumnViewModel : BaseModel
    {
        public OnboardingModel Onboarding { get; set; }
        public int Index { get; set; }
        public int Total { get; set; }
        public string ViewType { get; set; }
    }
}
