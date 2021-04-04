using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class AddWorkingdayHolidayViewModel
    {
        public WorkingdayHolidayModel WorkingdayHoliday { get; set; } = new WorkingdayHolidayModel();
        public List<WorkingdayHolidayMapperModel> WorkingHolidayMappers = new List<WorkingdayHolidayMapperModel>();
        public List<WorkingdayHolidayShiftModel> WorkingHolidayShifts = new List<WorkingdayHolidayShiftModel>();
        public List<dynamic> WorkingdayShifts = new List<dynamic>(); 
        public List<dynamic> Classifys = new List<dynamic>();
        public List<dynamic> SalaryRegimes = new List<dynamic>();
        public List<dynamic> DesistRegimes = new List<dynamic>();

    }
}