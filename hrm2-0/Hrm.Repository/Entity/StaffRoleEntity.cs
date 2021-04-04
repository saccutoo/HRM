using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class StaffRoleEntity : BaseEntity
    {
        public long RoleId { get; set; }
        public long StaffId { get; set; }
        public string Description { get; set; }
    }
}
