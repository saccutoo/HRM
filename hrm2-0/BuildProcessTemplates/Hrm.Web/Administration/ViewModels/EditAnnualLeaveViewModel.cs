using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace Hrm.Admin.ViewModels
{
    public class EditAnnualLeaveViewModel
    {
        public AnnualLeaveModel AnnualLeave { get; set; } = new AnnualLeaveModel();
        public List<dynamic> HandlingAnnualLeaves { get; set; } = new List<dynamic>();
    }
}