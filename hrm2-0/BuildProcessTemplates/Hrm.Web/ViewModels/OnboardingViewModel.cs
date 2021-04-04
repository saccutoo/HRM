using Hrm.Framework.Models;
using System.Collections.Generic;

namespace Hrm.Web.ViewModels
{
    public class OnboardingViewModel : BaseModel
    {
        public OnboardingViewModel()
        {
            Table = new TableViewModel();
        }
        public List<OnboardingModel> Onboardings { get; set; }
        public TableViewModel Table { get; set; }
        public string ViewType { get; set; }
    }
}
