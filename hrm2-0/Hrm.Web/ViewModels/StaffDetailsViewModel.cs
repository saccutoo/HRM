using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;
using System;
using System.Collections.Generic;

namespace Hrm.Web.ViewModels
{
    public class StaffDetailsViewModel : BaseViewModel
    {
        public StaffDetailsViewModel()
        {
            Staff = new StaffModel();
            ListWorkingProcess = new List<WorkingProcessModel>();
            ListRole = new List<StaffRoleModel>();
            ListOrganization = new List<OrganizationModel>();
            ListStaffRelationShips = new TableViewModel();
            ListReward = new TableViewModel();
            ListDiscipline = new TableViewModel();
            ListWorkingProcess = new List<WorkingProcessModel>();
            WorkingProcess = new WorkingProcessModel();
            ListWorkingProcessTable = new TableViewModel();
            ListSalary = new TableViewModel();
            ListAllowance = new TableViewModel();
            ListBenefit = new TableViewModel();
            ListTable = new TableViewModel();
            SocialInsurance = new StaffSocialInsuranceModel();
            HealthInsurance = new HealthInsuranceModel();
            ListExperience = new List<StaffExperienceModel>();
            ListCertificate = new List<StaffCertificateModel>();
            StaffOnboardInfo = new StaffOnboardInfoModel();
            StaffRelationships = new List<StaffRelationShipsModel>();
            StaffWorkingDayMachine = new List<StaffWorkingDayMachineModel>();
            StaffAllowancePopupWorkingprocess = new List<StaffAllowanceModel>();
            StaffBenefitPopupWorkingprocess = new List<StaffBenefitsModel>();
        }
        //Staff Detail info
        public int ActiveTab { get; set; }
        public int ActionTab { get; set; }
        public int ViewType { get; set; }
        public StaffModel Staff { get; set; }
        public List<StaffRelationShipsModel> StaffRelationships { get; set; }
        public List<StaffWorkingDayMachineModel> StaffWorkingDayMachine { get; set; }
        public WorkingProcessModel WorkingProcess { get; set; }
        public StaffContractModel Contract { get; set; }
        public List<StaffRoleModel> ListRole { get; set; }
        public List<OrganizationModel> ListOrganization { get; set; }
        public TableViewModel ListStaffRelationShips { get; set; }
        public TableViewModel ListStaffWorkingDayMachines { get; set; }
        public TableViewModel ListReward { get; set; }
        public TableViewModel ListDiscipline { get; set; }
        public List<WorkingProcessModel> ListWorkingProcess { get; set; }
        public TableViewModel ListWorkingProcessTable { get; set; }
        public TableViewModel ListSalary { get; set; }
        public TableViewModel ListAllowance { get; set; }
        public TableViewModel ListBenefit { get; set; }
        public TableViewModel ListTable { get; set; }
        public StaffSocialInsuranceModel SocialInsurance { get; set; }
        public HealthInsuranceModel HealthInsurance { get; set; }
        public List<StaffExperienceModel> ListExperience { get; set; }
        public List<StaffCertificateModel> ListCertificate { get; set; }
        public ChecklistViewModel Checklist { get; set; }
        public StaffOnboardInfoModel StaffOnboardInfo { get; set; }
        public List<StaffAllowanceModel> StaffAllowancePopupWorkingprocess { get; set; }
        public List<StaffBenefitsModel> StaffBenefitPopupWorkingprocess { get; set; }

        // dropdowm list
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
        public List<dynamic> DataDropdownMaritalStatus { get; set; }
        public List<dynamic> DataDropdownProvince { get; set; }
        public List<dynamic> DataDropdownRole { get; set; }
        public List<dynamic> DataDropdownNationality { get; set; }
        public List<dynamic> DataDropdownEthnicity { get; set; }
        public List<dynamic> DataDropdownTimekeepingForm { get; set; }
        public List<dynamic> DataDropdownPipeline { get; set; }
        public List<dynamic> DataDropdownPipelineStep { get; set; }
        public List<dynamic> DataDropdownInsuranceType { get; set; }

    }
}