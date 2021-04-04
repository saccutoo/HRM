using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Hrm.Repository.Type;



namespace Hrm.Service
{
    public partial class StaffService : IStaffService
    {
        IStaffRepository _staffRepository;
        private string _dbName;
        public StaffService(IStaffRepository staffRepository)
        {
            this._staffRepository = staffRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetStaff(BasicParamType param, out int totalRecord)
        {
            var staffResponse = this._staffRepository.GetStaff(param, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffParentById(long id, DateTime date)
        {
            var response = this._staffRepository.GetStaffParentById(id, date, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetStaffByParentId(long id)
        {
            var response = this._staffRepository.GetStaffByParentId(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetStaffByOrganizationId(BasicParamType param, long parentId, out int totalRecord)
        {
            var staffResponse = this._staffRepository.GetStaffByOrganizationId(param, parentId, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffInformationById(long staffId)
        {
            var response = this._staffRepository.GetStaffInformationById(staffId, _dbName);
            return JsonConvert.SerializeObject(response);
        }

        public string GetPipelineStepStaffByMenuName(BasicParamType param, string menuName, out int totalRecord)
        {
            var staffResponse = this._staffRepository.GetPipelineStepStaffByMenuName(param, menuName, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetPipelineStepStaffByStaffIdAndMenuName(BasicParamType param, string menuName, long staffId)
        {
            var staffResponse = this._staffRepository.GetPipelineStepStaffByStaffIdAndMenuName(param, menuName, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffBenefitByStaff(BasicParamType param, long staffId)
        {
            var staffResponse = this._staffRepository.GetStaffBenefitByStaff(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffBenefitByWorkingprocess(BasicParamType param, long workingprocessId)
        {
            var staffResponse = this._staffRepository.GetStaffBenefitByWorkingprocess(param, workingprocessId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffBenefitById(BasicParamType param, long staffId)
        {
            var staffResponse = this._staffRepository.GetStaffBenefitById(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffAllowanceByStaff(BasicParamType param, long staffId)
        {
            var staffResponse = this._staffRepository.GetStaffAllowanceByStaff(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffAllowanceByWorkingprocess(BasicParamType param, long workingprocessId)
        {
            var staffResponse = this._staffRepository.GetStaffAllowanceByWorkingprocess(param, workingprocessId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffAllowanceById(BasicParamType param, long id)
        {
            var staffResponse = this._staffRepository.GetStaffAllowanceById(param, id);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffExperience(BasicParamType param, long staffId)
        {
            var staffResponse = this._staffRepository.GetStaffExperience(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffExperienceById(BasicParamType param, long id)
        {
            var staffResponse = this._staffRepository.GetStaffExperienceById(param, id);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveExperience(StaffExperienceEntity data, BasicParamType param)
        {
            var staffResponse = this._staffRepository.SaveExperience(data, param);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffCertificate(BasicParamType param, long staffId)
        {
            var staffResponse = this._staffRepository.GetStaffCertificate(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffCertificateById(BasicParamType param, long id)
        {
            var staffResponse = this._staffRepository.GetStaffCertificateById(param, id);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveCertificate(StaffCertificateEntity data, BasicParamType param)
        {
            var staffResponse = this._staffRepository.SaveCertificate(data, param);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffSocialInsurance(BasicParamType param, long staffId)
        {
            var staffResponse = this._staffRepository.GetStaffSocialInsurance(param, staffId);

            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffHealthInsurance(BasicParamType param, long staffId)
        {
            var staffResponse = this._staffRepository.GetStaffHealthInsurance(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveHealthInsurance(HealthInsuranceEntity data, BasicParamType param)
        {
            var staffResponse = this._staffRepository.SaveHealthInsurance(data, param);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveSocialInsurance(StaffSocialInsuranceEntity data, BasicParamType param)
        {
            var staffResponse = this._staffRepository.SaveSocialInsurance(data, param);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetHealthInsuranceByStaff(BasicParamType param, long staffId)
        {
            var staffResponse = this._staffRepository.GetHealthInsuranceByStaff(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetSocialInsuranceByStaff(BasicParamType param, long staffId)
        {
            var staffResponse = this._staffRepository.GetSocialInsuranceByStaff(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveContract(List<StaffContractType> contract, BasicParamType param)
        {
            var response = this._staffRepository.SaveContract(contract, param);
            return JsonConvert.SerializeObject(response);
        }
        public string GetAllStaffForDropDown(BasicParamType param)
        {
            var response = this._staffRepository.GetAllStaffForDropDown(param);
            return JsonConvert.SerializeObject(response);
        }
        public string GetDataUserByUserName(string userName)
        {
            var response = this._staffRepository.GetDataUserByUserName(userName, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string CheckStaffInfoByStaffCode(string staffCode)
        {
            var response = this._staffRepository.CheckStaffInfoByStaffCode(staffCode, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveStaff(StaffEntity data, BasicParamType param)
        {
            var staffResponse = this._staffRepository.SaveStaff(data, param);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveStaffFull(List<StaffType> staff, List<WorkingProcessType> workingPorcess, List<StaffContractType> contract, List<StaffRoleType> role, StaffOnboardInfoType staffOnboardInfo
            , List<StaffWorkingDayMachineType> staffWorkingDayMachine, List<StaffRelationshipsType> staffRelationship
            , List<StaffAllowanceType> staffAllowance, List<StaffBenefitsType> staffBenefit, BasicParamType param)
        {
            var staffResponse = this._staffRepository.SaveStaffFull(staff, workingPorcess, contract, role, staffOnboardInfo, staffWorkingDayMachine, staffRelationship, staffAllowance, staffBenefit, param);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveStaffInformation(List<StaffType> staff, List<StaffRoleType> role, List<StaffRelationshipsType> staffRelationship, List<StaffWorkingDayMachineType> staffWorkingDayMachine, BasicParamType param)
        {
            var staffResponse = this._staffRepository.SaveStaffInformation(staff, role, staffRelationship, staffWorkingDayMachine, param);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveStaffChecklist(List<Repository.Type.ChecklistDetailType> type, BaseEntity staff)
        {
            var staffResponse = this._staffRepository.SaveStaffChecklist(type, staff,_dbName);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetCurrentStaffContractByStaff(BasicParamType param, long staffId)
        {
            var staffResponse = this._staffRepository.GetCurrentStaffContractByStaff(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffContractById(BasicParamType param, long id)
        {
            var staffResponse = this._staffRepository.GetStaffContractById(param, id);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SearchStaff(BasicParamType param, string searchKey, out int totalRecord)
        {
            var staffResponse = this._staffRepository.SearchStaff(param, searchKey, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SearchPermissionStaff(string searchKey)
        {
            var staffResponse = this._staffRepository.SearchPermissionStaff(searchKey, _dbName);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveBonusDiscipline(StaffBonusDisciplineEntity data, BasicParamType param)
        {
            var staffResponse = this._staffRepository.SaveBonusDiscipline(data, param);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveStatusPipeline(PipelineStepEntity pipelineStep, long staffId)
        {
            var staffResponse = this._staffRepository.SaveStatusPipeline(pipelineStep, staffId, _dbName);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffWorkingDayMachineByStaff(BasicParamType param, long staffId, out int totalRecord)
        {
            var staffResponse = this._staffRepository.GetStaffWorkingDayMachineByStaff(param, staffId, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string CheckNextPipeLineStep(BasicParamType param, long pipeLineSteffId, long staffId, out bool result)
        {
            var staffResponse = this._staffRepository.CheckNextPipeLineStep(param, pipeLineSteffId, staffId, out result);
            return JsonConvert.SerializeObject(staffResponse);
        }
    }
}
