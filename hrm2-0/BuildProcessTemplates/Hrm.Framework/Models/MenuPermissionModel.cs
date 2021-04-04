using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class MenuPermissionModel:BaseModel
    {
        public long MenuId { get; set; }
        public long PermissionId { get; set; }
        public string MenuName { get; set; }
        public string PermissionName { get; set; }
        public bool IsCheck { get; set; }
        public long MenuParentId { get; set; }

    }
}
