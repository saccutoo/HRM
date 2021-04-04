using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework;
using Hrm.Framework.Models;

namespace Hrm.Admin.ViewModels
{
    public class MenuPermissionViewModel
    {
        public List<dynamic> MenuPermissions { get; set; }
        public List<dynamic> Menus { get; set; }
        public List<dynamic> Staffs { get; set; }
        public List<dynamic> Roles { get; set; }
        public StaffModel Staff { get; set; }
        public RoleModel Role { get; set; }
        public bool isView { get; set; }
    }
}