using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class GeneralnformationAndSalaryViewModel
    {
        public SalaryViewModel Salary { get; set; } = new SalaryViewModel();
        public int Month { get; set; }
        public int Year { get; set; }
        public long StaffId { get; set; }
        public int ActiveTab { get; set; }

    }
}