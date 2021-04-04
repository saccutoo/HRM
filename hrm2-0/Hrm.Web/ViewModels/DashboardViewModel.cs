using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            DashboardSummary = new DashboardSummaryModel();
            DashboardTurnoverrate = new List<DashboardTurnoverRateModel>();
            DashboardPersonal = new DashboardPersonalViewModel();
            DashboardEvent = new List<DashboardEventViewModel>();
            Staffs = new List<StaffModel>();
            WorkingdaySupplements = new List<WorkingdaySupplementModel>();
        }
        public int ActiveTab { get; set; }
        public DashboardSummaryModel DashboardSummary { get; set; }
        public List<DashboardTurnoverRateModel> DashboardTurnoverrate { get; set; }
        public DashboardPersonalViewModel DashboardPersonal { get; set; }
        public List<DashboardEventViewModel> DashboardEvent { get; set; }
        public List<StaffModel> Staffs { get; set; }
        public List<WorkingdaySupplementModel> WorkingdaySupplements { get; set; }
    }
}