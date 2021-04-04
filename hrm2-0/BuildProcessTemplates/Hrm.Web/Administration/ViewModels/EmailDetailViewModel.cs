using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class EmailDetailViewModel
    {
        public EmailModel Email { get; set; } = new EmailModel();
        public List<AttachmentModel> Attachments { get; set; } = new List<AttachmentModel>();
        public List<AttachmentJs> AttachmentJs { get; set; } = new List<AttachmentJs>();
        public List<dynamic> Staffs { get; set; } = new List<dynamic>();
        public List<dynamic> Columns { get; set; } = new List<dynamic>();
        public bool isSendWelcomeKit { get; set; } = false;
        public bool isAdd { get; set; }
        public StaffModel staff { get; set; } = new StaffModel();

    }
}