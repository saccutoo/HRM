using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Admin.ViewModels
{
    public class WorkingDayCalculationPeriodViewModel
    {
        public TableViewModel Table { get; set; }
        public List<WorkingDayCalculationPeriodModel> WorkingDayCalculationPeriod { get; set; } = new List<WorkingDayCalculationPeriodModel>();
    }
}