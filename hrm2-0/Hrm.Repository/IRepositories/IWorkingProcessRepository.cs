using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface IWorkingProcessRepository
    {
        HrmResultEntity<WorkingProcessEntity> GetWorkingProcessByStaff(BasicParamType param, long staffId, out int totalRecord);
        HrmResultEntity<WorkingProcessEntity> GetCurrentWorkingProcessByStaff(BasicParamType param, long staffId);
        HrmResultEntity<WorkingProcessEntity> GetCurrentWorkingManagerByStaff(BasicParamType param, long staffId);
        HrmResultEntity<WorkingProcessEntity> GetStaffSalary(BasicParamType param, long staffId);
        HrmResultEntity<WorkingProcessEntity> SaveWorkingProcess(List<WorkingProcessType> workingProcess, List<StaffContractType> contract, List<StaffAllowanceType> staffAllowance, List<StaffBenefitsType> StaffBenefit, StaffOnboardInfoType staffOnboardInfo, bool isSalary, bool isPossition, bool isContract, BasicParamType param);
        HrmResultEntity<WorkingProcessDetailEntity> GetWorkingProcessById(BasicParamType param, long id);
        HrmResultEntity<WorkingProcessEntity> GetSalaryDetailById(BasicParamType param, long id);
        void UpdateStaffParent(string dbName);
        HrmResultEntity<bool> CheckDecisionNoExisted(BasicParamType param, string decisionNo, long id, out bool result);
        HrmResultEntity<bool> CheckContractCodeExisted(BasicParamType param, string contractCode, long id, out bool result);
        HrmResultEntity<bool> CheckWorkingProcessDate(DateTime? startDate, DateTime? endDate, long staffId, long organizationId, long id, string dbName, out bool result);
    }
}
