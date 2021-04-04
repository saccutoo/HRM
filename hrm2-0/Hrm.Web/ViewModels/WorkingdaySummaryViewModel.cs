using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Hrm.Web.ViewModels
{
    public class WorkingdaySummaryViewModel 
    {
        public List<WorkingdayModel> Workingdays { get; set; }
        public List<WorkingdayCheckInOutModel> WorkingdayMachines { get; set; }
        public List<WorkingdaySupplementModel> WorkingdaySupplements { get; set; }
        public List<WorkingdayFurloughViewModel> WorkingdayFurlough { get; set; }
        public List<MasterDataModel> DayOffTypes { get; set; }
        public WorkingdayPeriodModel WorkingdayPeriod { get; set; } = new WorkingdayPeriodModel();
        public List<DateTime> Dates { get; set; } = new List<DateTime>();
        public TableViewModel Table { get; set; } = new TableViewModel();
        public WorkingdayShiftMapperModel WorkingdayShiftMapper { get; set; } = new WorkingdayShiftMapperModel();

        public string ViewType { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public long StaffId { get; set; }
        public long organizationId { get; set; }
        public long StaffIdFilter { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}