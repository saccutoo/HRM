using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using HRM.Common;
using HRM.DataAccess.Entity;
using HRM.DataAccess.Entity.UserDefinedType;
using HRM.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HRM.DataAccess.DAL
{
  public  class StaffPlanFundRateDAL: BaseDal<ADOProvider>
    {
        public List<StaffPlanFundRateModel> StaffPlanFundRate_GetList(int pageNumber, int pageSize, string filter, int LanguageCode, out int total, out TableColumnsTotal totalColumns)
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
                param.Add("@TotalRecordQ1", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordQ2", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordQ3", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordQ4", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalRecordSum", 0, DbType.Double, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<StaffPlanFundRateModel>("StaffPlanFundRate_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("StaffPlanFundRate_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
        public SystemMessage StaffPlanFundRate_Save(List<StaffPlanFundRate> data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@StaffPlanFundRateType", data.ToUserDefinedDataTable(), DbType.Object);
                UnitOfWork.ProcedureExecute("StaffPlanFundRate_Save", param);
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
        public SystemMessage StaffPlanFundRate_Delete(int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                UnitOfWork.ProcedureExecute("StaffPlanFundRate_Delete", param);
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
