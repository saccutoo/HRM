using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class WorkingdayFurloughViewModel
    {
        public List<dynamic> StaffMangers { get; set; } = new List<dynamic>();
        public List<dynamic> Organizations { get; set; } = new List<dynamic>();
        public TableViewModel Table { get; set; } = new TableViewModel();
        public int Month { get; set; }
        public int Year { get; set; }
        public long OrganizationId { get; set; }
        public long StaffIdFilter { get; set; }
    }
}