using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Admin.ViewModels
{
    public class OrganizationChartViewModel
    {
        public List<OrganizationByLevel> OrganizationsByLevel { get; set; }
    }
    public class OrganizationByLevel
    {
        public OrganizationModel Organization { get; set; }
        public int Level { get; set; }
    }
}