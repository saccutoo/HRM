using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class StaffAllowanceViewModel : BaseViewModel
    {
        public StaffAllowanceViewModel()
        {
            StaffAllowance = new StaffAllowanceModel();
            DataDropdownAllowanceType = new List<dynamic>();
            DataDropdownStatusAprove = new List<dynamic>();
            DataDropdownCurrency = new List<dynamic>();
        }
        public int ViewType { get; set; } = 0;
        public StaffAllowanceModel StaffAllowance { get; set; }
        public List<dynamic> DataDropdownAllowanceType { get; set; }
        public List<dynamic> DataDropdownStatusAprove { get; set; }
        public List<dynamic> DataDropdownCurrency { get; set; }
    }
}