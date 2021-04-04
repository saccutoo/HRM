using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class EmployeeRelationships
    {
        public string StatusName { set; get; }
        public string RelationshipName { set; get; }

        public string StaffName { set; get; }

        public string OrganizationUnit { set; get; }
    }
}
