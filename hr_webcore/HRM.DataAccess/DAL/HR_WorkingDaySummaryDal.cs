using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class HR_WorkingDaySummaryDal : BaseDal<ADOProvider>
    {
        public List<Sys_Table_Column> GetTableColumns(string tableName)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@tableName", tableName);
                var list = UnitOfWork.Procedure<Sys_Table_Column>("GetSys_Table_Column", param,useCache:true).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<HR_WorkingDaySummary> GetHR_WorkingDaySummary(BaseListParam listParam,out TableColumnsTotal totalColumns,int month,int year)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Month", month);
                param.Add("@Year", year);
                if(listParam.FilterField != "" && listParam.FilterField != null && listParam.FilterField != "0")
                {
                    param.Add("@UserId", listParam.FilterField);
                }
                else
                {
                    param.Add("@UserId", listParam.UserId);
                }
                param.Add("@TotalDay", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalFurlough", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalOvertime", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalWorkingDaySupplement", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalWorkingDayLeave", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@SumTotalWorkingDay", 0, DbType.Double, ParameterDirection.InputOutput);
                param.Add("@TotalHourLate", "", DbType.String, ParameterDirection.InputOutput);
                param.Add("@TotalHourEarly", "", DbType.String, ParameterDirection.InputOutput);
                param.Add("@TotalHour", "", DbType.String, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<HR_WorkingDaySummary>("HR_WorkingDaySummary_Get", param).ToList();
                param = HttpRuntime.Cache.Get("HR_WorkingDaySummary_Get-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@TotalDay").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@TotalFurlough").ToString();
                totalColumns.Total3 = param.GetDataOutput<double>("@TotalOvertime").ToString();
                totalColumns.Total4 = param.GetDataOutput<double>("@TotalWorkingDaySupplement").ToString();
                totalColumns.Total5 = param.GetDataOutput<double>("@TotalWorkingDayLeave").ToString();
                totalColumns.Total6 = param.GetDataOutput<double>("@SumTotalWorkingDay").ToString();
                totalColumns.Total7 = param.GetDataOutput<string>("@TotalHourLate");
                totalColumns.Total8 = param.GetDataOutput<string>("@TotalHourEarly");
                totalColumns.Total9 = param.GetDataOutput<string>("@TotalHour");

                return list;
            }
            catch (Exception ex)
            {
                totalColumns = new TableColumnsTotal();
                return new List<HR_WorkingDaySummary>();
            }

        }
        public List<HR_WorkingDaySummary> GetWorkingDayTemporary(BaseListParam listParam, int month, int year)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Month", month);
                param.Add("@Year", year);
                if (listParam.FilterField != "" && listParam.FilterField != null && listParam.FilterField != "0")
                {
                    param.Add("@UserId", listParam.FilterField);
                }
                else
                {
                    param.Add("@UserId", listParam.UserId);
                }
                var list = UnitOfWork.Procedure<HR_WorkingDaySummary>("WorkingDayTemporary_Get", param).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<HR_WorkingDaySummary>();
            }

        }
    }
}
