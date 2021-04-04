using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;

namespace Hrm.Admin.ViewModels
{
    public class ConfigInsuranceDetailViewModel
    {
        public ConfigInsuranceDetailModel ConfigInsuranceDetail { get; set; } = new ConfigInsuranceDetailModel();
        public TableViewModel Table = new TableViewModel();
    }
}