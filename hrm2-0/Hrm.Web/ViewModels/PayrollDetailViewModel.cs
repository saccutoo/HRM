using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;
using System;
using System.Collections.Generic;

namespace Hrm.Web.ViewModels
{
    public class PayrollDetailViewModel : BaseViewModel
    {
        public PayrollDetailViewModel()
        {
            Payroll = new PayrollModel();
        }

        public PayrollModel Payroll { get; set; }

        // dropdowm list
        public List<dynamic> DataDropdownSalaryType { get; set; }
        public List<dynamic> DataDropdownPayrollStatus { get; set; }
    }
}