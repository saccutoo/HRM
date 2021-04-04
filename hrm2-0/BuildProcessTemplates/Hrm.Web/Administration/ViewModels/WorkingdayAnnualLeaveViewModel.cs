using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;

namespace Hrm.Admin.ViewModels
{
    public class WorkingdayAnnualLeaveViewModel
    {
        public AnnualLeaveModel AnnualLeave { get; set; } = new AnnualLeaveModel();
        public TableViewModel Table { get; set; } = new TableViewModel();
        public int ActiveTab  {get; set;}

}
}