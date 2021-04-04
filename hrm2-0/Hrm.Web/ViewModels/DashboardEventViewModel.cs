using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class DashboardEventViewModel
    {
        public long StaffId { get; set; }
        public string StaffName { get; set; }
        public string OrganizationName { get; set; }
        public bool IsBirthday { get; set; }
        public bool IsOnboard { get; set; }
    }
}