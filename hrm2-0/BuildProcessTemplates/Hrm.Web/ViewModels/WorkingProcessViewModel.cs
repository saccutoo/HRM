using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.ViewModels
{
    public class WorkingProcessViewModel : BaseViewModel
    {
        public WorkingProcessViewModel()
        {
            WorkingProcess = new WorkingProcessModel();
            StaffOnboardInfo = new StaffOnboardInfoModel();
            Contract = new StaffContractModel();
            ListOrganization = new List<OrganizationModel>()
            {
                new OrganizationModel()
            };
            ListAllowance = new TableViewModel() {
                ShowFooter = false
            };
            ListBenefit = new TableViewModel()
            {
                ShowFooter = false
            };
            StaffAllowancePopupWorkingprocess = new List<StaffAllowanceModel>();
            StaffBenefitPopupWorkingprocess = new List<StaffBenefitsModel>();
            DataDropdownWorkingProcessType = new List<dynamic>();
            DataDropdownStatusAprove = new List<dynamic>();
            DataDropdownOrganization = new List<dynamic>();
            DataDropdownOffice = new List<dynamic>();
            DataDropdownPosition = new List<dynamic>();
            DataDropdownClassification = new List<dynamic>();
            DataDropdownStaffLevel = new List<dynamic>();
            DataDropdownManager = new List<dynamic>();
            DataDropdownPolicy = new List<dynamic>();
            DataDropdownCurrency = new List<dynamic>();
            DataDropdownPaymentForm = new List<dynamic>();
            DataDropdownPaymentMethod = new List<dynamic>();
            DataDropdownStatus = new List<dynamic>();
            DataDropdownShift = new List<dynamic>();
            DataDropdownContractTime = new List<dynamic>();
            DataDropdownStaff = new List<dynamic>();
            DataDropdownHR = new List<dynamic>();
            DataDropdownWorkingStatus = new List<dynamic>();
            DataDropdownPipeline = new List<dynamic>();
            DataDropdownPipelineStep = new List<dynamic>();
        }

        public WorkingProcessModel WorkingProcess { get; set; }
        public StaffContractModel Contract { get; set; }
        public List<OrganizationModel> ListOrganization { get; set; }
        public List<StaffAllowanceModel> StaffAllowancePopupWorkingprocess { get; set; }
        public List<StaffBenefitsModel> StaffBenefitPopupWorkingprocess { get; set; }
        public TableViewModel ListAllowance { get; set; }
        public TableViewModel ListBenefit { get; set; }
        public StaffOnboardInfoModel StaffOnboardInfo { get; set; }

        public bool IsContract { get; set; }
        public bool IsPossition { get; set; }
        public bool IsSalary { get; set; }
        public bool IsBennefit { get; set; }

        public List<dynamic> DataDropdownWorkingProcessType { get; set; }
        public List<dynamic> DataDropdownStatusAprove { get; set; }
        public List<dynamic> DataDropdownOrganization { get; set; }
        public List<dynamic> DataDropdownOffice { get; set; }
        public List<dynamic> DataDropdownPosition { get; set; }
        public List<dynamic> DataDropdownClassification { get; set; }
        public List<dynamic> DataDropdownStaffLevel { get; set; }
        public List<dynamic> DataDropdownManager { get; set; }
        public List<dynamic> DataDropdownPolicy { get; set; }
        public List<dynamic> DataDropdownCurrency { get; set; }
        public List<dynamic> DataDropdownPaymentForm { get; set; }
        public List<dynamic> DataDropdownPaymentMethod { get; set; }
        public List<dynamic> DataDropdownStatus { get; set; }
        public List<dynamic> DataDropdownShift { get; set; }
        public List<dynamic> DataDropdownContractTime { get; set; }
        public List<dynamic> DataDropdownContractType { get; set; }
        public List<dynamic> DataDropdownStaff { get; set; }
        public List<dynamic> DataDropdownHR { get; set; }
        public List<dynamic> DataDropdownWorkingStatus { get; set; }
        public List<dynamic> DataDropdownPipeline { get; set; }
        public List<dynamic> DataDropdownPipelineStep { get; set; }


    }
}