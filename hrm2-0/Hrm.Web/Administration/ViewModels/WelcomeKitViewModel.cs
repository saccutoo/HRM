using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;

namespace Hrm.Admin.ViewModels
{
    public class WelcomeKitViewModel
    {
        public string ViewType { get; set; }
        public EmailDetailViewModel EmailDetail { get; set; } = new EmailDetailViewModel();
        public int ActiveTab { get; set; }
        public bool isSendWelcomeKit { get; set; } = false;
        public AttachmentViewModel Attachment { get; set; } = new AttachmentViewModel();
    }
}