using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
namespace Hrm.Admin.ViewModels
{
    public class AddSalaryElementViewModel
    {
        public SalaryElementModel SalaryElement { get; set; } = new SalaryElementModel();
        public List<SalaryElementModel> SalaryElements { get; set; } = new List<SalaryElementModel>();
        public List<dynamic> Types { get; set; } = new List<dynamic>();
        public List<dynamic> DataTypes { get; set; } = new List<dynamic>();
        public List<LanguageModel> Languages { get; set; } = new List<LanguageModel>();
        public CheckSalaryElementFormula CheckFormula { get; set; } = new CheckSalaryElementFormula();
    }
}