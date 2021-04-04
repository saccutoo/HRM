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
    public class Policy_DAL : BaseDal<ADOProvider>
    {
        public List<Policy> Policy_GetList(int pageNumber, int pageSize, string filter,int LanguageCode, out int total)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@LanguageID", LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Policy>("Policy_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("Policy_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public SystemMessage Policy_Save(Policy data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                //var date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                //string[] ArrayDate = date.Split(' ');
                //var CreateDate = data.CreatedDate.ToString();
                //string[] ArrayCreateDate = CreateDate.Split(' ');
                //CreateDate= ArrayCreateDate[0]+" "+ ArrayDate[1];
                var param = new DynamicParameters();
                param.Add("@PolicyID", data.PolicyID);
                param.Add("@Name", data.Name);
                param.Add("@Status", data.Status);
                param.Add("@StartDate", data.StartDate);
                param.Add("@EndDate", data.EndDate);
                param.Add("@CreatedDate", data.CreatedDate);
                param.Add("@CreatedOn", data.CreatedOn);
                if (data.PolicyID!=0)
                {
                    param.Add("@ModifiedDate", DateTime.Now);
                }
                else
                {
                    param.Add("@ModifiedDate", data.CreatedDate);

                }
                param.Add("@Modifiedby", data.CreatedOn);
                param.Add("@Note", data.Note);
                param.Add("@PolicyincludeID",data.PolicyincludeID);
                param.Add("@PolicyBonusID", data.PolicyBonusID);
                param.Add("@PolicyIDOutput", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@SpendingAdjustmentRateType", data.ListSpendingAdjustmentRate.ToUserDefinedDataTable(), DbType.Object);
                UnitOfWork.ProcedureExecute("Policy_Save", param);
                systemMessage.IsSuccess = true;
                systemMessage.Message = param.GetDataOutput<int>("@PolicyIDOutput").ToString();

                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }

        }
        public SystemMessage Policy_Delete(int PolicyID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@PolicyID", PolicyID);
                UnitOfWork.ProcedureExecute("Policy_Delete", param);
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

        public SystemMessage PolicyKpi_Save(int PolicyID, string KPI)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@PolicyID", PolicyID);
                param.Add("@KpiID", KPI);
                UnitOfWork.ProcedureExecute("PolicyKpi_Save", param);
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

        public List<PolicyKpi> PolicyKpi_Get_ByPolicyID(int PolicyID)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@PolicyID", PolicyID);
                var list = UnitOfWork.Procedure<PolicyKpi>("PolicyKpi_Get_ByPolicyID", param).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }
        }

        public SystemMessage CheckPolicyName(Policy data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@KeyWord", data.Name);
                param.Add("@PolicyID", data.PolicyID);
                param.Add("@Status", 0, DbType.Int32, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("CheckPolicyName", param);
                systemMessage.IsSuccess = true;
                systemMessage.Message = param.GetDataOutput<int>("@Status").ToString();

                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }

        }
    }

}


