using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class MenuPermissionType : IUserDefinedType
    {
        public long MenuId { get; set; }
        public long PermissionId { get; set; }
        public bool IsCheck { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }
}
