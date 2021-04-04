using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Web.ViewModels
{
    public class SalaryViewModel
    {
        public TableViewModel Table { get; set; } = new TableViewModel();
    }
}