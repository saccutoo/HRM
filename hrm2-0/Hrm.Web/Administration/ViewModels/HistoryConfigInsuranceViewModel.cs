using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;

namespace Hrm.Admin.ViewModels
{
    public class HistoryConfigInsuranceViewModel
    {
        public HistoryConfigInsuranceViewModel()
        {
            Insurance = new List<ConfigInsuranceViewModel>();
        }
        public List<ConfigInsuranceViewModel> Insurance { get; set; } = new List<ConfigInsuranceViewModel>();
    }
}