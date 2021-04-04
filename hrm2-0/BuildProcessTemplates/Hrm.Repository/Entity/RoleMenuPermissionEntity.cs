using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class RoleMenuPermissionEntity: IUserDefinedType
    {
        public string MenuName { get; set; }
        public string PermissionName { get; set; }
    }
}
