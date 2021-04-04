using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Web.ViewModels
{
    public class WorkingdaySupplementDetailViewModel
    {
        public WorkingdaySupplementModel WorkingdaySupplement { get; set; } = new WorkingdaySupplementModel();
        public List<WorkingdaySupplementApprovalModel> WorkingdaySupplementApprovals { get; set; } = new List<WorkingdaySupplementApprovalModel>();

    }
}