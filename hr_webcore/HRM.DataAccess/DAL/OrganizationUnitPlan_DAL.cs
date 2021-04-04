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
using HRM.DataAccess.Entity.UserDefinedType;
using HRM.DataAccess.Helpers;
namespace HRM.DataAccess.DAL
{
    public class OrganizationUnitPlan_DAL : BaseDal<ADOProvider>
    {
        public List<OrganizationUnitPlan> OrganizationUnitPlan_GetList(int pageNumber, int pageSize, string filter,int Languagecode, out int total, out ToTalMonth ListTotalMonth)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                ListTotalMonth = new ToTalMonth();
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@LanguageID", Languagecode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM1", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM2", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM3", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM4", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM5", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM6", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM7", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM8", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM9", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM10", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM11", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordM12", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordSumMonth", 0, DbType.Double, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<OrganizationUnitPlan>("OrganizationUnitPlan_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("OrganizationUnitPlan_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                ListTotalMonth.SumM1 = param.GetDataOutput<double>("@TotalRecordM1");
                ListTotalMonth.SumM2 = param.GetDataOutput<double>("@TotalRecordM2");
                ListTotalMonth.SumM3 = param.GetDataOutput<double>("@TotalRecordM3");
                ListTotalMonth.SumM4 = param.GetDataOutput<double>("@TotalRecordM4");
                ListTotalMonth.SumM5 = param.GetDataOutput<double>("@TotalRecordM5");
                ListTotalMonth.SumM6 = param.GetDataOutput<double>("@TotalRecordM6");
                ListTotalMonth.SumM7 = param.GetDataOutput<double>("@TotalRecordM7");
                ListTotalMonth.SumM8 = param.GetDataOutput<double>("@TotalRecordM8");
                ListTotalMonth.SumM9 = param.GetDataOutput<double>("@TotalRecordM9");
                ListTotalMonth.SumM10 = param.GetDataOutput<double>("@TotalRecordM10");
                ListTotalMonth.SumM11 = param.GetDataOutput<double>("@TotalRecordM11");
                ListTotalMonth.SumM12 = param.GetDataOutput<double>("@TotalRecordM12");
                ListTotalMonth.SumMonth = param.GetDataOutput<double>("@TotalRecordSumMonth");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                ListTotalMonth = null;
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
        public List<OrganizationUnit> OrganizationUnit_GetALL()
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var list = UnitOfWork.Procedure<OrganizationUnit>("OrganizationUnit_GetALL", useCache: true).ToList();               
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
        public SystemMessage OrganizationUnitPlan_Save(OrganizationUnitPlan data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", data.AutoID);
                param.Add("@OrganizationUnitID", data.OrganizationUnitID);
                param.Add("@CurrencyTypeID", data.CurrencyTypeID);
                param.Add("@status", data.Status);
                param.Add("@Comment", data.Comment);
                param.Add("@Type", data.Type);
                param.Add("@Year", data.Year);
                param.Add("@M1", data.M1);
                param.Add("@M2", data.M2);
                param.Add("@M3", data.M3);
                param.Add("@M4", data.M4);
                param.Add("@M5", data.M5);
                param.Add("@M6", data.M6);
                param.Add("@M7", data.M7);
                param.Add("@M8", data.M8);
                param.Add("@M9", data.M9);
                param.Add("@M10", data.M10);
                param.Add("@M11", data.M11);
                param.Add("@M12", data.M12);
                param.Add("@CreatedBy", data.CreatedBy);
                param.Add("@CreatedOn", data.CreatedOn);
                param.Add("@ModifiedBy", data.CreatedBy);
                param.Add("@ModifiedOn", data.CreatedOn);
                param.Add("@ContractType", data.ContractType);
                UnitOfWork.ProcedureExecute("OrganizationUnitPlan_Save", param);
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
        public SystemMessage OrganizationUnitPlan_Delete(int AutoID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                UnitOfWork.ProcedureExecute("OrganizationUnitPlan_Delete", param);
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
    }

}

