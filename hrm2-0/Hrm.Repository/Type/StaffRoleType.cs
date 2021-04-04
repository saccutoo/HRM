using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class StaffRoleType : IUserDefinedType
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long StaffId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
