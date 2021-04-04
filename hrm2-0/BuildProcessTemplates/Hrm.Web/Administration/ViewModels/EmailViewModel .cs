using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class EmailViewModel
    {
        public List<EmailModel> ListEmail { get; set; } = new List<EmailModel>();
        public EmailDetailViewModel EmailDetail { get; set; } = new EmailDetailViewModel();
    }
}