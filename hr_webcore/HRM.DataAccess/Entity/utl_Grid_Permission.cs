using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class utl_Grid_Permission
    {
        public int Id { get; set; }
        public int GridId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionType { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public bool IsFilterButton { get; set; }
        public bool IsExportExcel { get; set; }
        public bool IsImportExcel { get; set; }
        public bool IsSubmit { get; set; }
        public bool IsApproval { get; set; }
        public bool IsDisApproval { get; set; }
        public bool IsCopy { get; set; }
        public string GridName { get; set; }
        public string PermissionName { get; set; }

    }
}
