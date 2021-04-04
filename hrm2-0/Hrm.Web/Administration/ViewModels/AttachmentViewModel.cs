using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class AttachmentViewModel
    {
        public AttachmentModel attachment { get; set; } = new AttachmentModel();
        public List<AttachmentModel> attachments { get; set; } = new List<AttachmentModel>();
        public bool isAdd { get; set; } = false;
        public int ActiveTab { get; set; }
        public List<AttachmentJs> AttachmentJs { get; set; } = new List<AttachmentJs>();

    }
}