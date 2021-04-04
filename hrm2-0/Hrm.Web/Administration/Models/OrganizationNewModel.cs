using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Admin.Models
{
    public class OrganizationNewModel
    {
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public long OrganizationType { get; set; }
        public DateTime? StartDate { get; set; }
    }
}