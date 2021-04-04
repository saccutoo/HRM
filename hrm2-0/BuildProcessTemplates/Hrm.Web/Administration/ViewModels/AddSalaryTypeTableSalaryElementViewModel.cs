using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class AddSalaryTypeTableSalaryElementViewModel
    {
        public List<dynamic> DynamicSalaryElements = new List<dynamic>();
        public List<SalaryElementModel> SalaryElements { get; set; } = new List<SalaryElementModel>();

        public SalaryElementModel SalaryElement = new SalaryElementModel();
    }
}