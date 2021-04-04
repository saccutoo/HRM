using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Entity;
using Newtonsoft.Json;
using System.Collections;
using HRM.Common;
using System.Web;
using HRM.DataAccess.Helpers;
namespace HRM.DataAccess.DAL
{
    public class PolicyDetail_DAL : BaseDal<ADOProvider>
    {
        public List<PolicyDetail> PolicyDetail_GetList(int pageNumber, int pageSize, string filter, int LanguageCode, out int total)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@LanguageID", LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<PolicyDetail>("PolicyDetail_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("PolicyDetail_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
    
        public SystemMessage PolicyDetail_Save(PolicyDetail data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", data.Id);
                param.Add("@StaffLevelId", data.StaffLevelId);
                param.Add("@PolicyID", data.PolicyID);
                param.Add("@StandardSpendingAmount", data.StandardSpendingAmount);
                param.Add("@BasicSalaryTo", data.BasicSalaryTo);
                param.Add("@BasicSalaryFrom", data.BasicSalaryFrom);
                param.Add("@Margincompensation", data.Margincompensation);
                param.Add("@EfficiencyBonus", data.EfficiencyBonus);
                param.Add("@Commission", data.Commission);
                param.Add("@TotalIncome", data.TotalIncome);
                param.Add("@MinSpending", data.MinSpending);
                param.Add("@MinPerson", data.MinPerson);
                param.Add("@StartDate", data.StartDate);
                param.Add("@EndDate", data.EndDate);
                param.Add("@SFormular", data.SFormular);
                param.Add("@SFormularCompensation", data.SFormularCompensation);
                param.Add("@SFormularAllowances", data.SFormularAllowances);
                param.Add("@StandardProbation", data.StandardProbation);
                param.Add("@SFormularProbation", data.SFormularProbation);
                param.Add("@SFormularBonus", data.SFormularBonus);
                param.Add("@SFormularDecemberbonus", data.SFormularDecemberbonus);
                param.Add("@SFormularKPIYear", data.SFormularKPIYear);
                param.Add("@Description", data.Description);
                param.Add("@Status", data.Status);
                param.Add("@UserId", data.UserId);
                param.Add("@PolicyDetailIdOutput", 0, DbType.Int32, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("PolicyDetail_Save", param);
                systemMessage.IsSuccess = true;
                systemMessage.Message = param.GetDataOutput<int>("@PolicyDetailIdOutput").ToString();

                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }

        }
        public SystemMessage PolicyDetail_Delete(int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                UnitOfWork.ProcedureExecute("PolicyDetail_Delete", param);
                systemMessage.IsSuccess = true;
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }
        public List<string> PolicyDetail_GetListFormularByStaffIdAndPolicyId(int id, int staffId, int policyId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                param.Add("@StaffId", staffId);
                param.Add("@PolicyId", policyId);
                var list = UnitOfWork.Procedure<string>("PolicyDetail_GetListFormularByStaffIdAndPolicyId", param).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex.Message);
                return new List<string>();
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
        public string PolicyDetail_GetResultSFormular(string sFormularstr)
        {
            try
            {
                var param = new DynamicParameters();
               param.Add("@SFormularstr", sFormularstr);
                var list = UnitOfWork.Procedure<string>("PolicyDetail_GetResultSFormular", param).ToList().FirstOrDefault().ToString();
                return list;
            }
            catch (Exception ex)
            {
                return "Error";
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
        public List<PolicyDetail_Kpi> PolicyDetail_GetListKpicodeByPolicyId(int policyId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@PolicyId", policyId);
                var list = UnitOfWork.Procedure<PolicyDetail_Kpi>("PolicyDetail_GetListKpicodeByPolicyId", param).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex.Message);
                return new List<PolicyDetail_Kpi>();
            }
        }
        public string PolicyDetail_GetListFomularRunError(string fomularStr,out string resultFomularError)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FomularStr", fomularStr);
                param.Add("@FomularResult", string.Empty, DbType.String, ParameterDirection.InputOutput);
                var fomularStrResult= UnitOfWork.Procedure<string>("PolicyDetail_GetListFomularRunError", param).ToList().FirstOrDefault().ToString();
                resultFomularError = param.GetDataOutput<string>("@FomularResult");
                return fomularStrResult;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex.Message);
                resultFomularError = string.Empty;
                return string.Empty;
            }
        }

        public List<CheckFormularModel> CheckFomularReturnExcel(int month, int year, int typeBonusID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@month", month);
                param.Add("@year", year);
                param.Add("@TypeBonusID", typeBonusID);
                var fomularResult = UnitOfWork.Procedure<CheckFormularModel>("CheckFomularReturnExcel", param).ToList();
                return fomularResult;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex.Message);
                return new List<CheckFormularModel>();
            }
        }
        public int CopyPolicyDetail(string lstPolicyDetailId, int policyId, int staffLevelId, int currentId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@LstPolicyDetailId", lstPolicyDetailId);
                param.Add("@PolicyId", policyId);
                param.Add("@StaffLevelId", staffLevelId);
                param.Add("@CurrentId", currentId);
                var fomularResult = UnitOfWork.Procedure<int>("PolicyDetail_CopyPolicyDetail", param).FirstOrDefault();
                return fomularResult;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex.Message);
                return 0;
            }
        }

        public List<SpendingAdjustmentRate> GetSpendingAdjustmentRateByPolicyId(int policyId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@PolicyId", policyId);
                var result = UnitOfWork.Procedure<SpendingAdjustmentRate>("SpendingAdjustmentRate_GetAll", param).ToList();
                return result;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex.Message);
                return new List<SpendingAdjustmentRate>();
            }
        }

    }

}
