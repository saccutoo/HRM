using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;

namespace Hrm.Web.ViewModels
{
    public class NeedApprovalWorkingdaySupplementDetailViewModel
    {
        public WorkingdaySupplementModel WorkingdaySupplement { get; set; } = new WorkingdaySupplementModel();
        public List<WorkingdaySupplementModel> WorkingdaySupplements { get; set; } = new List<WorkingdaySupplementModel>();
    }
}