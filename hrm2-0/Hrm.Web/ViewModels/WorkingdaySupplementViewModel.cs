using Hrm.Framework.Models;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class WorkingdaySupplementViewModel:BaseModel
    {
        public List<WorkingdaySupplementModel> WorkingdaySupplements { get; set; }
        public WorkingdaySupplementModel WorkingdaySupplement { get; set; }
        public WorkingdaySupplementDetailViewModel WorkingdaySupplementDetail { get; set; } = new WorkingdaySupplementDetailViewModel();
        public NeedApprovalWorkingdaySupplementDetailViewModel NeedApprovalWorkingdaySupplementDetail { get; set; } = new NeedApprovalWorkingdaySupplementDetailViewModel();
        public TableViewModel Table { get; set; }
        public List<LongType> longTypes { get; set; }
        public string ViewType { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public long StaffId { get; set; }
        public long organizationId { get; set; }
        public long StaffIdFilter { get; set; }
        public long SupplementTypeIdSelected { get; set; } /*Dùng làm cơ hiển thị bodo modal thêm supplement*/
        public bool IsApproval { get; set; }
    }
}