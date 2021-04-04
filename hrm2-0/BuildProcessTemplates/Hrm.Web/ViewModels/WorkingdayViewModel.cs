using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class WorkingdayViewModel : BaseViewModel
    {
        public WorkingdaySummaryViewModel WorkingdaySummary { get; set; }
        public WorkingdaySupplementViewModel WorkingdaySupplement { get; set; } = new WorkingdaySupplementViewModel();
        public WorkingdaySupplementApprovalViewModel WorkingdaySupplementApproval { get; set; }
        public WorkingdayAllStaffViewModel WorkingdayAllStaff { get; set; }
        public WorkingdayFurloughViewModel WorkingdayFurlough { get; set; }
        public long StaffId { get; set; }
        public int ActiveTab { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int ActionTab { get; set; }
        public long OrganizationId { get; set; }
        public long StaffIdFilter { get; set; }
        public int CountWorkingdaySupplementNeedApproval { get; set; }
        // dropdowm list
        public List<dynamic> DataDropdownReasonType { get; set; }
        public List<dynamic> DataDropdownSupplementType { get; set; }
        public List<dynamic> StaffMangers { get; set; }
    }
    
}