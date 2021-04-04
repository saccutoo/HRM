using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class WorkingdayAllStaffViewModel
    {
        public List<dynamic> StaffMangers { get; set; } = new List<dynamic>();
        public List<dynamic> Organizations { get; set; } = new List<dynamic>();
        public List<WorkingdayModel> WorkingdayAllStaff { get; set; } = new List<WorkingdayModel>();
        public TableViewModel Table { get; set; } = new TableViewModel();
        public int Month { get; set; }
        public int Year { get; set; }
        public long StaffId { get; set; }
        public long OrganizationId { get; set; }
        public long StaffIdFilter { get; set; }
    }
}