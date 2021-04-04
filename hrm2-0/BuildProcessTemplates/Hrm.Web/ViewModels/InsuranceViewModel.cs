using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class InsuranceViewModel
    {
        public InsuranceViewModel()
        {
            StaffSocialInsurance = new StaffSocialInsuranceModel();
            HealthInsurance = new HealthInsuranceModel();
        }
        public StaffSocialInsuranceModel StaffSocialInsurance { get; set; }
        public HealthInsuranceModel HealthInsurance { get; set; }
    }
}