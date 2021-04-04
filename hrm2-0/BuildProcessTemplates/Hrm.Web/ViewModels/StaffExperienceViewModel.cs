using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class StaffExperienceViewModel
    {
        public StaffExperienceViewModel()
        {
            StaffExperience = new StaffExperienceModel();
            DataDropdownPositionId = new List<dynamic>();
            DataDropdownClassification = new List<dynamic>();
            DataDropdownCurrency = new List<dynamic>();
        }
        public int ViewType { get; set; } = 0;
        public StaffExperienceModel StaffExperience { get; set; }
        public List<dynamic> DataDropdownPositionId { get; set; }
        public List<dynamic> DataDropdownClassification { get; set; }
        public List<dynamic> DataDropdownCurrency { get; set; }
    }
}