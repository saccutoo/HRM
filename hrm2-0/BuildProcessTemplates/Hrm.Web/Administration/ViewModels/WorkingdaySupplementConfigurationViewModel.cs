using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Admin.ViewModels
{
    public class WorkingdaySupplementConfigurationViewModel
    {
        public List<dynamic> Roles { get; set; } = new List<dynamic>();
        public List<RoleModel> ListRoles { get; set; } = new List<RoleModel>();
        public List<dynamic> ListStaffs { get; set; } = new List<dynamic>();

        public List<dynamic> ListApprovedByStaffs { get; set; } = new List<dynamic>();

        public List<StaffModel> Staffs { get; set; } = new List<StaffModel>();
        public RoleModel Role { get; set; }
        public long RoleId { get; set; }
        public WorkingdaySupplementConfigurationModel SupplementConfiguration { get; set; } = new WorkingdaySupplementConfigurationModel();
        public WorkingdaySupplementConfigurationExceptionModel SupplementConfigurationException { get; set; } = new WorkingdaySupplementConfigurationExceptionModel();
        public bool isView { get; set; }
        public TableViewModel Table { get; set; }
        public List<dynamic> SupplementConfigurationStatusApprove { get; set; } = new List<dynamic>();

        public List<dynamic> SupplementConfigurationActions { get; set; } = new List<dynamic>();
    }
}