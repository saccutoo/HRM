using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Admin.ViewModels
{
    public class CheckSalaryElementFormula
    {
        public decimal Value { get; set; }
        public string Formula { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }
    }
}