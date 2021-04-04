using System;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface IStaffService : IBaseService
    {
        string GetStaff(BasicParamType param, out int totalRecord);
        string GetStaffByOrganizationId(BasicParamType param, long parentId, out int totalRecord);
        string GetStaffParentById(long id, DateTime date);
        string GetStaffByParentId(long id);
        string GetStaffInformationById(long staffId);
        string GetPipelineStepStaffByMenuName(BasicParamType param, string menuName, out int totalRecord); 
        string GetPipelineStepStaffByStaffIdAndMenuName(BasicParamType param, string menuName, long staffId); 
        string GetStaffBenefitByStaff(BasicParamType param, long staffId);
        string GetStaffBenefitByWorkingprocess(BasicParamType param, long workingprocessId);
        string GetStaffBenefitById(BasicParamType param, long staffId);
        string GetStaffAllowanceByStaff(BasicParamType param, long staffId);
        string GetStaffAllowanceByWorkingprocess(BasicParamType param, long workingprocessId);
        string GetStaffAllowanceById(BasicParamType param, long id);
        string GetStaffExperience(BasicParamType param, long staffId);
        string GetStaffExperienceById(BasicParamType param, long id);
        string SaveExperience(StaffExperienceEntity data, BasicParamType param);
        string GetStaffCertificate(BasicParamType param, long staffId);
        string GetStaffCertificateById(BasicParamType param, long id);
        string SaveCertificate(StaffCertificateEntity data, BasicParamType param);
        string GetStaffSocialInsurance(BasicParamType param, long staffId);
        string GetStaffHealthInsurance(BasicParamType param, long staffId);
        string SaveHealthInsurance(HealthInsuranceEntity data, BasicParamType param);
        string SaveSocialInsurance(StaffSocialInsuranceEntity data, BasicParamType param);
        string GetHealthInsuranceByStaff(BasicParamType param, long staffId);
        string GetSocialInsuranceByStaff(BasicParamType param, long staffId);
        string SaveContract(List<StaffContractType> contract, BasicParamType param);
        string GetAllStaffForDropDown(BasicParamType param);
        string GetDataUserByUserName(string userName);
        string CheckStaffInfoByStaffCode(string staffCode);
        string SaveStaff(StaffEntity data, BasicParamType param);
        string SaveStaffChecklist(List<ChecklistDetailType> type, BaseEntity staff);
        string SaveStaffFull(List<StaffType> staff, List<WorkingProcessType> workingPorcess, List<StaffContractType> contract, List<StaffRoleType> role, StaffOnboardInfoType staffOnboardInfo
            , List<StaffWorkingDayMachineType> staffWorkingDayMachine, List<StaffRelationshipsType> staffRelationship
            , List<StaffAllowanceType> staffAllowance, List<StaffBenefitsType> staffBenefit, BasicParamType param);
        string SaveStaffInformation(List<StaffType> staff, List<StaffRoleType> role, List<StaffRelationshipsType> staffRelationship, List<StaffWorkingDayMachineType> staffWorkingDayMachine, BasicParamType param);
        string GetCurrentStaffContractByStaff(BasicParamType param, long staffId);
        string SearchStaff(BasicParamType param, string searchKey, out int totalRecord);
        string GetStaffContractById(BasicParamType param, long id);
        string SearchPermissionStaff(string searchKey);
        string SaveBonusDiscipline(StaffBonusDisciplineEntity data, BasicParamType param);
        string SaveStatusPipeline(PipelineStepEntity pipelineStep, long staffId);
        string GetStaffWorkingDayMachineByStaff(BasicParamType param, long staffId, out int totalRecord);
        string CheckNextPipeLineStep(BasicParamType param, long pipeLineSteffId, long staffId, out bool result);
    }
}
