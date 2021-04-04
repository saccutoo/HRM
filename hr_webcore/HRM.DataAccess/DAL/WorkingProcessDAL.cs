using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using HRM.Common;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class WorkingProcessDAL : BaseDal<ADOProvider>
    {
        public List<WorkingProcess> GetWorkingProcess(int pageNumber, int pageSize, string filter, out int total, int LanguageID, int RoleID, int UserID, int DeptID, int staffID, int OfficePositionID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderByField", "");
                param.Add("@PageIndex", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@StaffID", staffID);
                param.Add("@UserId", UserID);
                param.Add("@RoleId", RoleID);
                param.Add("@OfficePositionID", OfficePositionID);
                param.Add("@DeptID", DeptID);
                param.Add("@LanguageID", LanguageID);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<WorkingProcess>("WorkingProcess_Gets", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("WorkingProcess_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }


        }
        public WorkingProcess GetWorkingProcessById(int roleId, int idTable, int id, int OfficePositionID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@WPID", id);
                param.Add("@RoleId", roleId);
                param.Add("@OfficePositionID", OfficePositionID);
                return UnitOfWork.Procedure<WorkingProcess>("WorkingProcess_GetInfo", param).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SystemMessage SaveWorkingProcess(int roleId, int idTable, WorkingProcess obj, int StaffID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@StartDate", obj.WPStartDate);
                param1.Add("@EndDate", obj.WPEndDate);
                param1.Add("@StaffID", StaffID);
                param1.Add("@WPID", obj.WPID);
                param1.Add("@ExistedResult", 0, DbType.Int32, ParameterDirection.InputOutput);
                param1.Add("@ExistedDate", "", DbType.String, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("WorkingProcess_CheckStartDate", param1);
                var existedResult = param1.GetDataOutput<int>("@ExistedResult");
                var existedDate = param1.GetDataOutput<string>("@ExistedDate");
                if (existedResult < 0)
                {
                    systemMessage.IsSuccess = false;
                    systemMessage.existedResult = existedResult;
                    if (existedDate != "")
                    {
                        DateTime tempDate = Convert.ToDateTime(existedDate, CultureInfo.InvariantCulture);
                        systemMessage.ExistedDate = tempDate.ToString().Substring(0, 10);
                    }
                    return systemMessage;
                }
                else
                {
                    var param = new DynamicParameters();
                    param.Add("@StaffID", StaffID);
                    param.Add("@WPTypeID", obj.WPTypeID);
                    param.Add("@WorkingStatus", obj.WorkingStatus);
                    param.Add("@DecisionNo", obj.DecisionNo);
                    param.Add("@Status", obj.Status);
                    param.Add("@StartDate", obj.WPStartDate);
                    param.Add("@EndDate", obj.WPEndDate);
                    param.Add("@CompanyID", obj.CompanyID);
                    param.Add("@OfficePositionID", obj.OfficePositionID);
                    param.Add("@OfficeID", obj.OfficeID);
                    param.Add("@OfficeRoleID", obj.OfficeRoleID);
                    param.Add("@StaffLevelID", obj.StaffLevelID);
                    param.Add("@ContractTypeID", obj.ContractTypeID);
                    param.Add("@OrganizationUnitID", obj.OrganizationUnitID);
                    param.Add("@ContractNo", obj.ContractNo);
                    param.Add("@StartDateContract", obj.StartDateContract);
                    param.Add("@EndDateContract", obj.EndDateContract);
                    param.Add("@ManagerID", obj.ManagerID);
                    param.Add("@HRIDs", obj.HRIDs);
                    param.Add("@PolicyID", obj.PolicyID);
                    param.Add("@CurrencyID", obj.CurrencyID);
                    param.Add("@BasicPay", obj.BasicPay);
                    param.Add("@EfficiencyBonus", obj.EfficiencyBonus);
                    param.Add("@Note", obj.WPNote);
                    param.Add("@WPID", obj.WPID, DbType.Int32, ParameterDirection.InputOutput);
                    param.Add("@TeamLeadLevelID", obj.TeamLeadLevelID);
                    UnitOfWork.ProcedureExecute("WorkingProcess_Save", param);
                    var resultWPID = param.GetDataOutput<int>("@WPID");
                    if (obj.EmployeeAllowanceList != null)
                    {
                        foreach (var item in obj.EmployeeAllowanceList)
                        {
                            var paramdetail = new DynamicParameters();
                            paramdetail.Add("@WPID", resultWPID);
                            paramdetail.Add("@DS_StaffID", StaffID);
                            paramdetail.Add("@AllowanceID", item.AllowanceID);
                            paramdetail.Add("@Amount", item.Amount);
                            paramdetail.Add("@StartDate", item.StartDate);
                            paramdetail.Add("@EndDate", item.EndDate);
                            paramdetail.Add("@Note", item.Note);
                            if (obj.Iscopy == 1)
                            {
                                paramdetail.Add("@AutoID", 0, DbType.Int32, ParameterDirection.InputOutput);
                            }
                            else
                            {
                                paramdetail.Add("@AutoID", item.AutoID, DbType.Int32, ParameterDirection.InputOutput);
                            }

                            UnitOfWork.ProcedureExecute("EmployeeAllowance_Save", paramdetail);
                        }
                    }

                    if (obj.EmployeeAllowanceDeleteList != null)
                    {
                        foreach (var item in obj.EmployeeAllowanceDeleteList)
                        {
                            var paramdetail = new DynamicParameters();
                            paramdetail.Add("@AutoID", item.AutoID);
                            paramdetail.Add("@Result");
                            UnitOfWork.ProcedureExecute("EmployeeAllowanceDelete", paramdetail);
                        }
                    }

                    systemMessage.IsSuccess = true;
                    return systemMessage;
                }
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }


        }

        public List<Policy> GetPolicy()
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@Result");
                return UnitOfWork.Procedure<Policy>("Get_All_Policy", param, useCache: true).ToList();
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public SystemMessage DeleteWorkingProcess(int roleId, int idTable, int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {

                var param = new DynamicParameters();
                param.Add("@WPID", id);
                param.Add("@Roleid", roleId);
                var checkExisted = UnitOfWork.Procedure<WorkingProcess>("WorkingProcess_GetInfo", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@WPID", id);
                    param1.Add("@Result");
                    UnitOfWork.ProcedureExecute("WorkingProcess_Delete", param1);
                    systemMessage.IsSuccess = true;
                    return systemMessage;
                }
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }
        public void UpdateStaffParent()
        {
            try
            {
                UnitOfWork.ProcedureExecute("RegenerateStaffParent");
            }
            catch(Exception ex)
            {
            }
        }
    }
}
