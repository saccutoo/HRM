using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class WorkingdayHolidayViewModel
    {
        public TableViewModel Table { get; set; }
        public List<WorkingdayHolidayModel> WorkingdayHolidays { get; set; } = new List<WorkingdayHolidayModel>();

    }
}