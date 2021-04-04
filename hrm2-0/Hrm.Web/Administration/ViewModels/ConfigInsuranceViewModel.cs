using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;

namespace Hrm.Admin.ViewModels
{
    public class ConfigInsuranceViewModel
    {
        public ConfigInsuranceViewModel()
        {
            ConfigInsurance = new ConfigInsuranceModel();
            Table = new TableViewModel();
            DataDropdownStatus = new List<dynamic>();
        }
        public ConfigInsuranceModel ConfigInsurance { get; set; } = new ConfigInsuranceModel();
        public TableViewModel Table = new TableViewModel();
        public int ActiveTab { get; set; }
        public List<dynamic> DataDropdownStatus { get; set; }
        public List<dynamic> DataDropdownCurrency { get; set; }
    }
}