using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class AddWorkingdayShiftViewModel
    {
        public WorkingdayShiftModel WorkingdayShift = new WorkingdayShiftModel();
        public List<WorkingdayShiftDetailModel> WorkingdayShiftDetails = new List<WorkingdayShiftDetailModel>();
        public List<dynamic> Status = new List<dynamic>();
        public List<dynamic> Works = new List<dynamic>();
        public List<dynamic> Periods = new List<dynamic>();
        public TableContentAddShiftViewModel TableContentAddShift = new TableContentAddShiftViewModel();
    }
}