using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Admin.ViewModels
{
    public class PersonalIncomeTaxViewModel : BaseModel
    {
        public PersonalIncomeTaxViewModel()
        {
            PersonalIncomeTax = new PersonalIncomeTaxModel();
            TablePersonalIncomeTaxDetails = new TableViewModel();
            DataDropdownStatus = new List<dynamic>();
        }
        public PersonalIncomeTaxModel PersonalIncomeTax { get; set; }
        public TableViewModel TablePersonalIncomeTaxDetails { get; set; }
        public List<dynamic> DataDropdownStatus { get; set; }
        public List<dynamic> DataDropdownCurrency { get; set; }
    }
}