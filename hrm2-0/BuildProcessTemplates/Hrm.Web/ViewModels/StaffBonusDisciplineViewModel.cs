using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class StaffBonusDisciplineViewModel
    {
        public StaffBonusDisciplineViewModel()
        {
            StaffBonusDiscipline = new StaffBonusDisciplineModel();
            ListStaffBonusDiscipline = new List<StaffBonusDisciplineModel>();
            DataDropdownRewardType = new List<dynamic>();
            DataDropdownStatusAprove = new List<dynamic>();
            DataDropdownCurrency = new List<dynamic>();
            DataDropdownRewardForm = new List<dynamic>();
            DataDropdownDisciplineForm = new List<dynamic>();
            DataDropdownDisciplineType = new List<dynamic>();
            Attachments = new List<AttachmentModel>();
        }
        public int ViewType { get; set; } = 0;
        public StaffBonusDisciplineModel StaffBonusDiscipline { get; set; }
        public List<StaffBonusDisciplineModel> ListStaffBonusDiscipline { get; set; }
        public List<AttachmentModel> Attachments { get; set; }
        public List<dynamic> DataDropdownRewardType { get; set; }
        public List<dynamic> DataDropdownDisciplineType { get; set; }
        public List<dynamic> DataDropdownStatusAprove { get; set; }
        public List<dynamic> DataDropdownCurrency { get; set; }
        public List<dynamic> DataDropdownRewardForm { get; set; }
        public List<dynamic> DataDropdownDisciplineForm { get; set; }
    }
}