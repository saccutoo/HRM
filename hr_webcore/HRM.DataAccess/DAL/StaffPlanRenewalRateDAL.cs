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
using HRM.DataAccess.Entity.UserDefinedType;
using HRM.DataAccess.Helpers;
using HRM.Common;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class StaffPlanRenewalRateDAL : BaseDal<ADOProvider>
    {
        public List<StaffPlanRenewalRate> StaffPlanRenewalRate_GetList(int pageNumber, int pageSize, string filter, int LanguageCode, out int total,out TableColumnsTotal totalColumns)
        {
            try
            {               
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);               
                param.Add("@LanguageID", LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@TotalRecordQ1", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordQ2", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordQ3", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordQ4", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordSum", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@Type", 1);
                var list = UnitOfWork.Procedure<StaffPlanRenewalRate>("StaffPlanRenewalRate_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("StaffPlanRenewalRate_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                totalColumns.TotalQ1 = param.GetDataOutput<double>("@TotalRecordQ1").ToString();
                totalColumns.TotalQ2 = param.GetDataOutput<double>("@TotalRecordQ2").ToString(); 
                totalColumns.TotalQ3 = param.GetDataOutput<double>("@TotalRecordQ3").ToString(); 
                totalColumns.TotalQ4 = param.GetDataOutput<double>("@TotalRecordQ4").ToString();
                totalColumns.TotalQuaterSum = param.GetDataOutput<double>("@TotalRecordSum").ToString();
                return list;              
            }
            catch (Exception ex)
            {
                total = 0;
                totalColumns = new TableColumnsTotal();
                return new List<StaffPlanRenewalRate>();
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
        public SystemMessage StaffPlanRenewalRate_Save(List<StaffPlanFundRate> data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@StaffPlanFundRateType", data.ToUserDefinedDataTable(), DbType.Object);
                UnitOfWork.ProcedureExecute("StaffPlanRenewalRate_Save", param);
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
        public SystemMessage StaffPlanRenewalRate_Delete(int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                UnitOfWork.ProcedureExecute("StaffPlanRenewalRate_Delete", param);
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


