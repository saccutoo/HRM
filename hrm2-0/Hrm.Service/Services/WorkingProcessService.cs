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
    public partial class WorkingProcessService : IWorkingProcessService
    {
        private string _dbName;
        IWorkingProcessRepository _workingProcessRepository;
        public WorkingProcessService(IWorkingProcessRepository workingProcessRepository)
        {
            this._workingProcessRepository = workingProcessRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetWorkingProcessByStaff(BasicParamType param,long staffId, out int totalRecord)
        {
            var result = new List<WorkingProcessEntity>();
            var staffResponse = this._workingProcessRepository.GetWorkingProcessByStaff(param, staffId, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetCurrentWorkingProcessByStaff(BasicParamType param, long staffId)
        {
            var staffResponse = this._workingProcessRepository.GetCurrentWorkingProcessByStaff(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetCurrentWorkingManagerByStaff(BasicParamType param, long staffId)
        {
            var staffResponse = this._workingProcessRepository.GetCurrentWorkingManagerByStaff(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetStaffSalary(BasicParamType param, long staffId)
        {
            var staffResponse = this._workingProcessRepository.GetStaffSalary(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string SaveWorkingProcess(List<WorkingProcessType> workingProcess, List<StaffContractType> contract, List<StaffAllowanceType> staffAllowance, List<StaffBenefitsType> StaffBenefit, StaffOnboardInfoType staffOnboardInfo, bool isSalary, bool isPossition, bool isContract, BasicParamType param)
        {
            var staffResponse = this._workingProcessRepository.SaveWorkingProcess(workingProcess, contract, staffAllowance, StaffBenefit, staffOnboardInfo, isSalary, isPossition, isContract, param);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetWorkingProcessById(BasicParamType param, long id)
        {
            var staffResponse = this._workingProcessRepository.GetWorkingProcessById(param, id);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetSalaryDetailById(BasicParamType param, long staffId)
        {
            var staffResponse = this._workingProcessRepository.GetSalaryDetailById(param, staffId);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public void UpdateStaffParent()
        {
            this._workingProcessRepository.UpdateStaffParent(_dbName);
        }
        public string CheckDecisionNoExisted(BasicParamType param, string decisionNo, long id, out bool result)
        {
            var staffResponse = this._workingProcessRepository.CheckDecisionNoExisted(param, decisionNo, id, out result);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string CheckContractCodeExisted(BasicParamType param, string contractCode, long id, out bool result)
        {
            var staffResponse = this._workingProcessRepository.CheckContractCodeExisted(param, contractCode, id, out result);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string CheckWorkingProcessDate(DateTime? startDate, DateTime? endDate, long staffId, long organizationId, long id, out bool result)
        {
            var staffResponse = this._workingProcessRepository.CheckWorkingProcessDate(startDate, endDate, staffId, organizationId, id, _dbName, out result);
            return JsonConvert.SerializeObject(staffResponse);
        }
    }
}
