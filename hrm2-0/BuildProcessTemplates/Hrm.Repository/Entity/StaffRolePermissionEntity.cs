using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class StaffRolePermissionEntity:BaseEntity
    {
        public long DataId { get; set; }
        public long DataTypeId { get; set; }
        public string DataTypeName { get; set; }
        public long MenuPermissionId { get; set; }
        public string Description { get; set; }
    }
}
