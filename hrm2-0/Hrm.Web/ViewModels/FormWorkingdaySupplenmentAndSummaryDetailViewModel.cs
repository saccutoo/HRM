using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
using Hrm.Repository.Type;

namespace Hrm.Web.ViewModels
{
    public class FormWorkingdaySupplenmentAndSummaryDetailViewModel
    {
        public List<WorkingdaySupplementModel> WorkingdaySupplements { get; set; } = new List<WorkingdaySupplementModel>();
        public WorkingdayModel WorkingdaySummary { get; set; } = new WorkingdayModel();
        public List<WorkingdayCheckInOutModel> WorkingdayCheckInOuts { get; set; } = new List<WorkingdayCheckInOutModel>();
        public DateTime Date { get; set; }
        public List<LongType> longTypes { get; set; } = new List<LongType>();
        public long StaffId { get; set; }
        public int ActiveTap  { get; set; }
    }
}