using System;

namespace Hrm.Framework.Models
{
    public class StaffRoleModel : BaseModel
    {
        public long RoleId { get; set; }
        public long StaffId { get; set; }
        public string Description { get; set; }
    }
}
