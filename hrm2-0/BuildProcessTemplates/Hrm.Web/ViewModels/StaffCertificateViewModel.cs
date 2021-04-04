using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class StaffCertificateViewModel
    {
        public StaffCertificateViewModel()
        {
            StaffCertificate = new StaffCertificateModel();
            DataDropdownRank = new List<dynamic>();
        }
        public int ViewType { get; set; } = 0;
        public StaffCertificateModel StaffCertificate { get; set; }
        public List<dynamic> DataDropdownRank { get; set; }
    }
}