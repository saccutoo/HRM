using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Common.Helpers;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hrm.Repository
{
    public class WorkingProcessRepository : CommonRepository, IWorkingProcessRepository
    {
        public HrmResultEntity<WorkingProcessEntity> GetWorkingProcessByStaff(BasicParamType param, long staffId, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingProcessEntity>("WorkingProcess_Get_GetWorkingProcessByStaff", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                //par = HttpRuntime.Cache.Get(param.DbName + "-WorkingProcess_Get_GetWorkingProcessByStaff-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<WorkingProcessEntity> GetCurrentWorkingProcessByStaff(BasicParamType param, long staffId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId);
            return ListProcedure<WorkingProcessEntity>("WorkingProcess_Get_GetCurrentWorkingProcessByStaff", par, dbName: param.DbName);
        }
        public HrmResultEntity<WorkingProcessEntity> GetCurrentWorkingManagerByStaff(BasicParamType param, long staffId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId);
            return ListProcedure<WorkingProcessEntity>("WorkingProcess_Get_GetCurrentWorkingManagerByStaff", par, dbName: param.DbName);
        }
        public HrmResultEntity<WorkingProcessEntity> GetStaffSalary(BasicParamType param, long staffId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId);
            return ListProcedure<WorkingProcessEntity>("Staff_Get_GetStaffSalary", par, dbName: param.DbName);
        }
        public HrmResultEntity<WorkingProcessEntity> SaveWorkingProcess(List<WorkingProcessType> workingProcess, List<StaffContractType> contract, List<StaffAllowanceType> staffAllowance, List<StaffBenefitsType> StaffBenefit
                , StaffOnboardInfoType staffOnboardInfo, bool isSalary, bool isPossition, bool isContract, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@IsContract", isContract);
            par.Add("@IsPossition", isPossition);
            par.Add("@IsSalary", isSalary);
            par.Add("WorkingProcess", workingProcess.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("Contract", contract.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("StaffAllowance", staffAllowance.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("StaffBenefit", StaffBenefit.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("StaffOnboardInfo", staffOnboardInfo.ToUserDefinedDataTable(), DbType.Object);

            par.Add("@DbName", param.DbName);
            par.Add("@UserId", param.UserId);
            var result = ListProcedure<WorkingProcessEntity>("WorkingProcess_Update_SaveWorkingProcessFull", par, useCache:false);
            return result;
        }
        public HrmResultEntity<WorkingProcessDetailEntity> GetWorkingProcessById(BasicParamType param, long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", param.DbName);
            par.Add("@UserID", param.UserId);
            par.Add("@RoleID", param.RoleId);
            return ListProcedure<WorkingProcessDetailEntity>("WorkingProcess_Get_GetWorkingProcessById", par, dbName: param.DbName);
        }
        public HrmResultEntity<WorkingProcessEntity> GetSalaryDetailById(BasicParamType param, long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", param.DbName);
            par.Add("@UserID", param.UserId);
            par.Add("@RoleID", param.RoleId);
            return ListProcedure<WorkingProcessEntity>("Staff_Get_GetSalaryDetailById", par, dbName: param.DbName);
        }
        public void UpdateStaffParent(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            ListProcedure<WorkingProcessEntity>("System_Update_RegenerateStaffParent", par, dbName: dbName);
        }
        public HrmResultEntity<bool> CheckDecisionNoExisted(BasicParamType param, string decisionNo, long id, out bool result)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@Id", id);
            par.Add("@DecisionNo", decisionNo);
            par.Add("@Result", 0, DbType.Boolean, ParameterDirection.InputOutput);
            var response = ListProcedure<bool>("WorkingProcess_Get_CheckDecisionNoExisted", par);
            result = par.GetDataOutput<bool>("@Result");
            return response;
        }
        public HrmResultEntity<bool> CheckContractCodeExisted(BasicParamType param, string contractCode, long id, out bool result)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@Id", id);
            par.Add("@ContractCode", contractCode);
            par.Add("@Result", 0, DbType.Boolean, ParameterDirection.InputOutput);
            var response = ListProcedure<bool>("WorkingProcess_Get_CheckContractCodeExisted", par);
            result = par.GetDataOutput<bool>("@Result");
            return response;
        }
        public HrmResultEntity<bool> CheckWorkingProcessDate(DateTime? startDate, DateTime? endDate, long staffId, long organizationId, long id, string dbName, out bool result)
        {
            var par = new DynamicParameters();
            par.Add("@StartDate", startDate);
            par.Add("@EndDate", endDate);
            par.Add("@StaffId", staffId);
            par.Add("@OrganizationId", organizationId);
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            par.Add("@Result", 0, DbType.Boolean, ParameterDirection.InputOutput);
            var response = ListProcedure<bool>("WorkingProcess_Get_CheckWorkingProcessDate", par);
            result = par.GetDataOutput<bool>("@Result");
            return response;
        }
    }
}
