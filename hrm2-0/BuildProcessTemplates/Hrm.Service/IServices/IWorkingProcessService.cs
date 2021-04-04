using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial interface IWorkingProcessService : IBaseService
    {
        string GetWorkingProcessByStaff(BasicParamType param, long staffId, out int totalRecord);
        string GetCurrentWorkingProcessByStaff(BasicParamType param, long staffId);
        string GetCurrentWorkingManagerByStaff(BasicParamType param, long staffId);
        string GetStaffSalary(BasicParamType param, long staffId);
        string SaveWorkingProcess(List<WorkingProcessType> workingProcess, List<StaffContractType> contract, List<StaffAllowanceType> staffAllowance, List<StaffBenefitsType> StaffBenefit, StaffOnboardInfoType staffOnboardInfo, bool isSalary, bool isPossition, bool isContract, BasicParamType param);
        string GetWorkingProcessById(BasicParamType param, long id);
        string GetSalaryDetailById(BasicParamType param, long staffId);
        void UpdateStaffParent();
        string CheckDecisionNoExisted(BasicParamType param, string decisionNo, long id, out bool result);
        string CheckContractCodeExisted(BasicParamType param, string contractCode, long id, out bool result);
        string CheckWorkingProcessDate(DateTime? startDate, DateTime? endDate, long staffId, long organizationId, long id, out bool result);
    }
}
