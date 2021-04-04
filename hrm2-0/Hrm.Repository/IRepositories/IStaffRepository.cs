using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using Hrm.Repository.Type;

namespace Hrm.Repository
{
    public partial interface IStaffRepository
    {
        HrmResultEntity<StaffEntity> GetStaff(BasicParamType param, out int totalRecord);
        HrmResultEntity<StaffEntity> GetStaffByOrganizationId(BasicParamType param, long parentId, out int totalRecord);
        HrmResultEntity<StaffEntity> GetStaffParentById(long id, DateTime date, string dbName);
        HrmResultEntity<StaffEntity> GetStaffByParentId(long id, string dbName);
        HrmResultEntity<StaffEntity> GetStaffInformationById(long staffId, string dbName);
        HrmResultEntity<StaffEntity> GetPipelineStepStaffByMenuName(BasicParamType param, string menuName, out int totalRecord);
        HrmResultEntity<StaffEntity> GetAllStaffForDropDown(BasicParamType param);
        HrmResultEntity<UserEntity> GetDataUserByUserName(string userName, string dbName);
        HrmResultEntity<UserEntity> CheckStaffInfoByStaffCode(string staffCode, string dbName);
        HrmResultEntity<StaffEntity> SearchPermissionStaff(string searchKey, string dbName);

        #region benefit
        HrmResultEntity<StaffBenefitsEntity> GetStaffBenefitByStaff(BasicParamType param, long staffId);
        HrmResultEntity<StaffBenefitsEntity> GetStaffBenefitByWorkingprocess(BasicParamType param, long workingprocessId);
        HrmResultEntity<StaffBenefitsEntity> GetStaffBenefitById(BasicParamType param, long id);
        #endregion
        #region Allowance
        HrmResultEntity<StaffAllowanceEntity> GetStaffAllowanceByStaff(BasicParamType param, long staffId);
        HrmResultEntity<StaffAllowanceEntity> GetStaffAllowanceByWorkingprocess(BasicParamType param, long workingprocessId);
        HrmResultEntity<StaffAllowanceEntity> GetStaffAllowanceById(BasicParamType param, long id);
        #endregion
        #region Staff Experience
        HrmResultEntity<StaffExperienceEntity> GetStaffExperience(BasicParamType param, long staffId);
        HrmResultEntity<StaffExperienceEntity> GetStaffExperienceById(BasicParamType param, long id);
        HrmResultEntity<StaffExperienceEntity> SaveExperience(StaffExperienceEntity data, BasicParamType param);
        #endregion
        #region Staff Certificate
        HrmResultEntity<StaffCertificateEntity> GetStaffCertificate(BasicParamType param, long staffId);
        HrmResultEntity<StaffCertificateEntity> GetStaffCertificateById(BasicParamType param, long id);
        HrmResultEntity<StaffCertificateEntity> SaveCertificate(StaffCertificateEntity data, BasicParamType param);
        #endregion
        #region Staff Insurance
        HrmResultEntity<StaffSocialInsuranceEntity> GetStaffSocialInsurance(BasicParamType param, long staffId);
        HrmResultEntity<HealthInsuranceEntity> GetStaffHealthInsurance(BasicParamType param, long staffId);
        HrmResultEntity<HealthInsuranceEntity> SaveHealthInsurance(HealthInsuranceEntity data, BasicParamType param);
        HrmResultEntity<StaffSocialInsuranceEntity> SaveSocialInsurance(StaffSocialInsuranceEntity data, BasicParamType param);
        HrmResultEntity<HealthInsuranceEntity> GetHealthInsuranceByStaff(BasicParamType param, long staffId);
        HrmResultEntity<StaffSocialInsuranceEntity> GetSocialInsuranceByStaff(BasicParamType param, long staffId);
        #endregion
        #region staff contract
        HrmResultEntity<long> SaveContract(List<StaffContractType> contract, BasicParamType param);
        HrmResultEntity<StaffContractEntity> GetCurrentStaffContractByStaff(BasicParamType param, long staffId);
        HrmResultEntity<StaffContractEntity> GetStaffContractById(BasicParamType param, long id);
        #endregion
        #region Save Staff
        HrmResultEntity<bool> SaveStaff(StaffEntity data, BasicParamType param);         
        
        HrmResultEntity<bool> SaveStaffChecklist(List<ChecklistDetailType> data,BaseEntity staff, string dbName);
        
        HrmResultEntity<StaffEntity> SaveStaffFull(List<StaffType> staff, List<WorkingProcessType> workingPorcess, List<StaffContractType> contract, List<StaffRoleType> role, StaffOnboardInfoType staffOnboardInfo
            , List<StaffWorkingDayMachineType> staffWorkingDayMachine, List<StaffRelationshipsType> staffRelationship
            , List<StaffAllowanceType> staffAllowance, List<StaffBenefitsType> staffBenefit, BasicParamType param);
        HrmResultEntity<StaffEntity> SaveStaffInformation(List<StaffType> staff, List<StaffRoleType> role, List<StaffRelationshipsType> staffRelationship, List<StaffWorkingDayMachineType> staffWorkingDayMachine, BasicParamType param);
        #endregion
        HrmResultEntity<StaffBonusDisciplineEntity> SaveBonusDiscipline(StaffBonusDisciplineEntity data, BasicParamType param);
        HrmResultEntity<StaffEntity> SearchStaff(BasicParamType param, string searchKey, out int totalRecord);
        HrmResultEntity<bool> SaveStatusPipeline(PipelineStepEntity pipelineStep, long staffId, string dbName);
        HrmResultEntity<StaffOnboardInfoEntity> GetPipelineStepStaffByStaffIdAndMenuName(BasicParamType param, string menuName, long staffId);
        HrmResultEntity<StaffWorkingDayMachineEntity> GetStaffWorkingDayMachineByStaff(BasicParamType param, long staffId, out int totalRecord);
        HrmResultEntity<bool> CheckNextPipeLineStep(BasicParamType param, long pipeLineSteffId, long staffId, out bool result);
    }
}
