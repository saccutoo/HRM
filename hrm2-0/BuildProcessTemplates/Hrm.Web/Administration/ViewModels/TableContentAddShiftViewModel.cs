using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class TableContentAddShiftViewModel
    {
        public List<WorkingdayShiftDetailModel> WorkingdayShiftDetails { get; set; } = new List<WorkingdayShiftDetailModel>();
        public List<dynamic> ListOvertimeRate { get; set; } = new List<dynamic>();
    }
}