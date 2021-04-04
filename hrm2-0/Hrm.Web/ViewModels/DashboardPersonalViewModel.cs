using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class DashboardPersonalViewModel
    {
        public DashboardPersonalViewModel()
        {
            Workingdays = new List<WorkingdayModel>();
            WorkingdaySupplements = new List<WorkingdaySupplementModel>();
        }

        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public List<WorkingdayModel> Workingdays { get; set; }
        public List<WorkingdaySupplementModel> WorkingdaySupplements { get; set; }
        public List<DashboardWorkingdayChartModel> DashboardWorkingdayChart { get; set; }
        public List<dynamic> StaffMangers { get; set; }
        public long StaffId { get; set; }
    }
}