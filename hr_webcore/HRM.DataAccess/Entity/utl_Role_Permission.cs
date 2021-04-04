using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class utl_Role_Permission
    {
        public int Id { get; set; }
        public int GridId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionType { get; set; }
        public string Condition { get; set; }
        public string Delim { get; set; }
        public string GridName { get; set; }
        public string PermissionName { get; set; }

    }
}
