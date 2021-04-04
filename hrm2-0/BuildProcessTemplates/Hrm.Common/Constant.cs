using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Common
{
    public static class Constant
    {
        //Error Code
        public const int AccessDenied = 401;
        public const int NotFound = 404;
        public enum LanguageCode
        {
            VN = 1,
            EN = 2,
        }
        public const int AdminId = 1;
        public const string WebBaseUrl = "WebBaseUrl";
        public const string ConnectionStringKey = "Hrm";
        public const string SystemDbName = "System";
        public const string MasterDbName = "Hrm2.0";
        public const string APP_CURRENT_LANG = "App.Current.Lang";
        public const string ItemRewarDisciplineDefault = "ItemRewarDisciplineDefault";
        public const string ClientDateFormat = "ClientDateFormat";
        public const string DateFormat = "DateFormat";
    }
    public static class UniqField
    {
        //for multiple column, sperate it by ;
        public const string Localization = "ResourceName";
        public const string Language = "LanguageCulture";
        public const string TableConfig = "TableId";
    }
    public static class Sorting
    {
        //Sorting
        public const string Default = "Table.Sorting.Default";
        public const string Asc = "Table.Sorting.Asc";
        public const string Desc = "Table.Sorting.Desc";
    }
    public static class TableConfig
    {
        //for multiple column, sperate it by ;
        public const string Staff = "Staff";
        public const string MasterData = "MasterData";
        public const string Organization = "Organization";
        public const string Onboarding = "Onboarding";
        public const string StaffOrganization = "StaffOrganization";
        public const string StaffRelationships = "StaffRelationships";
        public const string WorkingProcess = "WorkingProcess";
        public const string Reward = "Reward";
        public const string Discipline = "Discipline";
        public const string StaffSalary = "StaffSalary";
        public const string StaffAllowance = "StaffAllowance";
        public const string StaffAllowancePopupWorkingprocess = "StaffAllowancePopupWorkingprocess";
        public const string StaffAllowanceHistory = "StaffAllowanceHistory";
        public const string StaffBenefit = "StaffBenefit";
        public const string StaffBenefitPopupWorkingprocess = "StaffBenefitPopupWorkingprocess";
        public const string StaffBenefitHistory = "StaffBenefitHistory";
        public const string ChecklistDetail = "ChecklistDetail";
        public const string StaffRole = "StaffRole";
        public const string WorkingdayFurlough = "WorkingdayFurlough";
        public const string Pipeline = "Pipeline";
        public const string WorkingdayAllStaff = "WorkingdayAllStaff";
        public const string WorkingDaySupplement = "WorkingdaySupplement";
        public const string WorkingDaySupplementApproval = "WorkingdaySupplementNeedApproval";
        public const string HealthInsurance = "HealthInsurance";
        public const string SocialInsurance = "StaffSocialInsurance";
        public const string WorkingdayHoliday = "WorkingdayHoliday";
        public const string WorkingdayShift = "WorkingdayShift";
        public const string WorkingdayDayCalculationPeriod = "WorkingDayCalculationPeriod";
        public const string WorkingdaySupplementConfiguration = "WorkingdaySupplementConfiguration";
        public const string WorkingdaySupplementConfigurationException = "WorkingdaySupplementConfigurationException";
        public const string Setting = "Setting";
        public const string WorkingdayAnnualLeave = "WorkingdayAnnualLeave";
        public const string StaffWorkingDayMachine = "StaffWorkingDayMachine";
        public const string WorkingdaySummaryFinal = "WorkingdaySummaryFinal";
        public const string SalaryElement = "SalaryElement";
        public const string SalaryType = "SalaryType";
        public const string Salary = "Salary";

    }
    public static class MasterGroup
    {
        public const string ItemsPerPage = "ItemsPerPage";
        public const string Position = "Position";
        public const string Status = "Status";
        public const string Currency = "Currency";
        public const string CategoryCompany = "CategoryCompany";
        public const string Brand = "Branch";
        public const string PipelinePosition = "PipelinePosition";
        public const string PositionName = "Normal";
        public const string WorkingProcessType = "WorkingProcessType";
        public const string StatusAprove = "StatusAprove";
        public const string Office = "Office";
        public const string Classification = "Classification";
        public const string StaffLevel = "StaffLevel";
        public const string Policy = "Policy";
        public const string PaymentForm = "PaymentForm";
        public const string PaymentMethod = "PaymentMethod";
        public const string Shift = "Shift";
        public const string ContractType = "ContractType";
        public const string ChecklistDetailType = "ChecklistDetailType";
        public const string ControlType = "ControlType";
        public const string ContractTime = "ContractTime";
        public const string WorkingStatus = "WorkingStatus";
    }
    public static class Timekeeper
    {
        public const long Beet = 1;
    }
    public static class MasterDataId
    {
        public const long ItemsPerPage = 10;
        public const long Position = 7;
        public const long Status = 316;
        public const long Currency = 19;
        public const long CategoryCompany = 34;
        public const long Brand = 35;
        public const long PipelinePosition = 41;
        public const long WorkingProcessType = 70;
        public const long StatusAprove = 45;
        public const long Classification = 306;
        public const long StaffLevel = 56;
        public const long Policy = 297;
        public const long PaymentForm = 226;
        public const long PaymentMethod = 223;
        public const long Shift = 59;
        public const long ContractType = 51;
        public const long ChecklistDetailType = 271;
        public const long ControlType = 274;
        public const long ContractTime = 301;
        public const long Province = 153;
        public const long OrderWeekOfMonth = 154;
        public const long WorkingStatus = 14;
        public const long MaritalStatus = 69;
        public const long Nationality = 217;
        public const long Group = 272;
        public const long Single = 273;
        public const long Checkbox = 275;
        public const long FieldUpdating = 276;
        public const long TextEditor = 277;
        public const long Datepicker = 278;
        public const long FileAttachment = 279;
        public const long Reward = 266;
        public const long Discipline = 267;
        public const long Ethnicity = 97;
        public const long ReasonType = 346;
        public const long SupplementType = 369;
        public const long TimekeepingForm = 420;
        public const long Male = 39;
        public const long Female = 40;
        public const long LateDuration = 370;
        public const long Vacation = 371;
        public const long NoSalary = 372;
        public const long AdditionalWork = 373;
        public const long AdditionalWorkOverTime = 374;
        public const long EarlyDuration = 459;
        public const long RewardType = 245;
        public const long DisciplineType = 250;
        public const long RewardForm = 261;
        public const long DisciplineForm = 258;
        public const long RequestDraff = 46;
        public const long RequestPending = 47;//Chờ duyệt
        public const long RequestReject = 49;//không duyệt
        public const long RequestApprove = 50;//Đã duyệt
        public const long AllowanceType = 228;
        public const long Onboarding = 461;
        public const long ApprovedStatus = 50;
        public const long WorkingProcessTypeOnboarding = 71;
        public const long WorkingStatusActive = 8;
        public const long Approve = 332;
        public const long Reject = 333;
        public const long Submit = 368;
        public const long ManagerRefusesToApprove = 325;
        public const long HumanResourcesRefusedToApprove = 327;
        public const long TheDirectorRefusedToApprove = 329;
        public const long Draff = 323;
        public const long WaitingForTheManagerToApprove = 324;
        public const long Rank = 291;
        public const long InsuranceType = 466;
        public const long InsuranceTypeIncrease = 467;
        public const long StatusApplying = 281;
        public const long StatusStoped = 282;
        public const long DesistRegimeMorning = 493;
        public const long DesistRegimeAfternoon = 494;
        public const long DesistRegimeAllDay = 495;
        public const long TypeOfWorking = 284;
        public const long SalaryRegime = 496;
        public const long DesistRegime = 492;
        public const long TypeWork = 500;
        public const long WorkingHour = 502;
        public const long WorkingDay = 501;
        public const long Normal = 42;
        public const long EndSuccess = 43;
        public const long EndFailure = 44;
        public const long SupplementConfigurationEndFailure = 10518;
        public const long SupplementConfigurationStatusAprove = 322;
        public const long SupplementConfigurationActions = 330;
        public const long PeriodApply = 10515;
        public const long HandlingSurplus = 10526;
        public const long Type = 10568;
        public const long DataType = 10574;
        public const long TypeOrganization = 10615;
        public const long GeneralInformation = 10569;
        public const long Incomes = 10570;
        public const long Deduction = 10571;
        public const long Pay = 10572;
        public const long Orther = 10573;
        public const long TypeNumber = 10575;
        public const long Coefficient = 10583;
        public const long TypeGroupSalaryElement = 10629;

    }
    public static class TableUrl
    {
        public const string MasterDataUrl = "Admin/MasterData/GetData";
        public const string OrganizationStaffUrl = "Admin/Organization/GetDataStaff";
        public const string OrganizationUrl = "Admin/Organization/GetDataOrganization";
        public const string StaffGetDataUrl = "Staff/GetData";
        public const string WorkingdayGetDataUrl = "Workingday/GetData";
        public const string WorkingProcesssUrl = "Staff/GetWorkingProcessData";
        public const string WorkingDaySupplementUrl = "Workingday/GetData";
        public const string WorkingDaySupplementNeedApprovalUrl = "Workingday/GetData";
        public const string TableWorkingdayHolidayUrl = "Admin/Workingday/GetData";
        public const string TableWorkingdayShiftUrl = "Admin/Workingday/GetData";
        public const string TableWorkingdayCalculationPeriodUrl = "Admin/Workingday/GetData";
        public const string TableWorkingdaySupplementConfiguration = "Admin/WorkingdaySupplementConfiguration/GetData";
        public const string TableWorkingdayAnnualLeaveUrl = "Admin/Workingday/GetData";
        public const string TableWorkingdaySummaryFinalUrl = "Workingday/GetData";
        public const string TableSalaryElementUrl = "Admin/SalaryElement/GetData";
        public const string TableSalaryTypeUrl = "Admin/SalaryType/GetData";
        public const string TableSalary = "Admin/Salary/GetData";

    }
    public static class TableReloadUrl
    {
        public const string TableWorkingdayReloadUrl = "Admin/Workingday/TableReloadConfigUrl";
        public const string TableMasterDataReloadUrl = "Admin/MasterData/TableReloadConfigUrl";
        public const string TableStaffReloadUrl = "Staff/TableReloadConfigUrl";
    }
    public static class TableName
    {
        public const string TableStaff = "table-staff";
        public const string TableRelationShips = "table-relation-ship";
        public const string StaffWorkingDayMachine = "staff-workingday-machine";
        public const string TableWorkingProcess = "table-woking-process";
        public const string TableMasterData = "table-master-data";
        public const string TableStaffOrganization = "table-staff-organization";
        public const string TableOrganization = "table-organization";
        public const string Reward = "table-Reward";
        public const string Discipline = "table-Discipline";
        public const string MasterData = "table-master-data";
        public const string StaffSalary = "table-staff-salary";
        public const string StaffAllowance = "table-staff-allowance";
        public const string StaffBenefit = "table-staff-benefit";
        public const string WorkingdayAllStaff = "table-workingday-all-staff";
        public const string Furlough = "table-furlough";
        public const string TableSearchStaff = "table-search-global";
        public const string TableWorkingDaySupplement = "table-workingday-supplement";
        public const string WorkingDaySupplementNeedApproval = "table-workingday-supplement-need-approval";
        public const string TableHealthInsurance = "table-health-insurance";
        public const string TableSocialInsurance = "table-social-insurance";
        public const string TableWorkingdayHoliday = "table-workingday-holiday";
        public const string TableWorkingdayShift = "table-workingday-shift";
        public const string TableWorkingdayCalculationPeriod = "table-workingday-calculation-period";
        public const string TableWorkingdaySupplementConfiguration = "table-workingday-supplement-configuration";
        public const string TableWorkingdaySupplementConfigurationException = "table-workingday-supplement-configuration-exception";
        public const string TableWorkingdayAnnualLeave = "table-workingday-annual-leave";
        public const string TableWorkingdaySummaryFinal = "table-workingday-summary-final";
        public const string TableSalaryElement = "table-salary-element";
        public const string TableSalaryType = "table-salary-type";
        public const string TableSalary = "table-salary";

    }
    public static class DataType
    {
        public const string MasterData = "MasterData";
        public const string Organization = "Organization";
        public const string Menu = "Menu";
        public const string Pipeline = "Pipeline";
        public const string PipelineStep = "PipelineStep";
        public const string Role = "Role";
        public const string Personal = "Personal";
        public const string TableName = "TableColumn";
        public const string Permission = "Permission";
        public const string Email = "Email";
        public const string Document = "Document";
        public const string Shift = "WorkingdayShift";
        public const string WorkingDayMachine = "WorkingDayMachine";
        public const string RewarFile = "RewarFile";
        public const string SalaryElement = "SalaryElement";

    }
    public static class Color
    {
        public const string Black = "#ffffff";
    }
    public static class MenuName
    {
        public const string Setting = "Setting";
        public const string Onboarding = "Onboarding";
    }
    public static class ViewType
    {
        public const string Card = "Card";
        public const string List = "List";
        public const string Calendar = "Calendar";
        public const string View = "View";
    }

    public static class ChecklistDetailType
    {
        public const string Group = "Group";
        public const string Single = "Single";
    }
    public static class ControlType
    {
        public const string Checkbox = "Checkbox";
        public const string FieldUpdating = "FieldUpdating";
        public const string TextEditor = "TextEditor";
        public const string Datepicker = "Datepicker";
        public const string FileAttachment = "FileAttachment";
    }
    public static class StatusCode
    {
        public const int AccessDenied = 401;
        public const int Ok = 200;
        public const int NotFound = 404;
    }
    public static class ActionName
    {
        public const string View = "View";
        public const string Delete = "Del";
        public const string Update = "Update";
        public const string Add = "Add";
        public const string Edit = "Edit";
        public const string Copy = "Copy";
        public const string Import = "Import";
        public const string Export = "Export";
    }
    public static class MultipleLanguageMasterData
    {
        public const string Name = "Name";
        public const string Description = "Description";
    }
    public static class FileExtension
    {
        public const string Docx = "docx";
        public const string Xlsx = "xlsx";
        public const string Img = "png,jpg";

    }
    public static class MailTemplate
    {
        public const long TemplateWelcomeKit = 10;
    }
    public static class Mail
    {
        public const string MailFrom = "kimlam2207@gmail.com";
        public const string MailReplyTo = "kimlam2207@gmail.com";
    }
}

