using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{

    public class utl_Control_Permission
    {
        public int ControlId { get; set; }
        public int GridColumnId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionType { get; set; }
        public bool IsActive { get; set; }
        public string CustomHtml { get; set; }
        public int DisplayOrder { get; set; }
        public string DataCondition { get; set; }
        public string  ColumnName { get; set; }
        public string PermissionName { get; set; }

    }
}
