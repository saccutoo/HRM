using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Admin.ViewModels
{
    public class AddWorkingDayCalculationPeriodViewModel
    {
        public WorkingDayCalculationPeriodModel WorkingDayCalculationPeriod { get; set; } = new WorkingDayCalculationPeriodModel();
        public List<WorkDayModel> WorkDays { get; set; } = new List<WorkDayModel>();
        public List<dynamic> ListStatus = new List<dynamic>();
        public List<dynamic> ListOrderWeekOfMonth = new List<dynamic>();
    }
}