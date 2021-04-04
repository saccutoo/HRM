using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class StaffSocialInsuranceViewModel
    {
        public StaffSocialInsuranceViewModel()
        {
            StaffSocialInsurance = new StaffSocialInsuranceModel();
            DataDropdownType = new List<dynamic>();
            DataDropdownCurrency = new List<dynamic>();
            StaffSocialInsuranceTable = new TableViewModel();
        }
        public StaffSocialInsuranceModel StaffSocialInsurance { get; set; }
        public List<dynamic> DataDropdownType { get; set; }
        public List<dynamic> DataDropdownCurrency { get; set; }
        public TableViewModel StaffSocialInsuranceTable { get; set; }
    }
}