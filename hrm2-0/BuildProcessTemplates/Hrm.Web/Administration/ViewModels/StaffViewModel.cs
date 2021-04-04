using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Admin.ViewModels
{
    public class StaffViewModel
    {
        public List<dynamic> Staff { get; set; }
        public List<dynamic> TableColum { get; set; }
        public long Index { get; set; }
        public string ActionName { get; set; }
        public ChecklistDetailModel ChecklistDetailEdit { get; set; } = new ChecklistDetailModel();
    }
}