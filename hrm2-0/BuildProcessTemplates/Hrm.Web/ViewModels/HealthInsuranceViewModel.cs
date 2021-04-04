using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class HealthInsuranceViewModel
    {
        public HealthInsuranceViewModel()
        {
            HealthInsurance = new HealthInsuranceModel();
            DataDropdownType = new List<dynamic>();
            HealthInsuranceTable = new TableViewModel();
        }
        public HealthInsuranceModel HealthInsurance { get; set; }
        public List<dynamic> DataDropdownType { get; set; }
        public TableViewModel HealthInsuranceTable { get; set; }
    }
}