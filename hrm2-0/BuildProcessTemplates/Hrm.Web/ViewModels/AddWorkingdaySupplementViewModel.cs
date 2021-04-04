using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class AddWorkingdaySupplementViewModel
    {
        public List<WorkingdayModel> Workingdays { get; set; }
        public WorkingdaySupplementModel WorkingdaySupplement { get; set; }
        public long StaffId { get; set; }
        public long SupplementTypeIdSelected { get; set; } /*Dùng làm cơ hiển thị bodo modal thêm supplement*/
        // dropdowm list
        public List<dynamic> DataDropdownReasonType { get; set; }
        public List<dynamic> DataDropdownSupplementType { get; set; }
        public List<dynamic> DataDropdownOverTimeRate{ get; set; }
        public bool isClickByDate { get; set; } = false;
        public DateTime? Date { get; set; } = null;
        public bool IsView { get; set; } = true;
        public bool isSendApproval { get; set; } = false;

    }
}