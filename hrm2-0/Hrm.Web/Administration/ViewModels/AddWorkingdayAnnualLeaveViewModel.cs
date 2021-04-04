using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
namespace Hrm.Admin.ViewModels
{
    public class AddWorkingdayAnnualLeaveViewModel
    {
        public WorkingdayAnnualLeaveModel WorkingdayAnnualLeave { get; set; } = new WorkingdayAnnualLeaveModel();
        public List<StaffModel> Staffs { get; set; } = new List<StaffModel>();
        public List<dynamic> PeriodApplys { get; set; } = new List<dynamic>();
        public List<dynamic> Status { get; set; } = new List<dynamic>();
        public List<dynamic> Regimes { get; set; } = new List<dynamic>();
        public List<int> ListCheckbox { get; set; } = new List<int>();
    }
}