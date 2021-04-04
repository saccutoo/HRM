using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class AddSalaryTypeViewModel
    {
        public List<dynamic> Status = new List<dynamic>();
        public List<dynamic> Organization = new List<dynamic>();
        public List<dynamic> Organizations = new List<dynamic>();
        public SalaryTypeModel SalaryType = new SalaryTypeModel();
        public List<string> ListOrganization { get; set; } = new List<string>();
        public AddSalaryTypeTableSalaryElementViewModel AddSalaryTypeTableSalaryElement { get; set; } = new AddSalaryTypeTableSalaryElementViewModel();
    }
}