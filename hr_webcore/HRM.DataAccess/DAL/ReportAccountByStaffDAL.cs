using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Common;
using HRM.DataAccess.Entity;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class ReportAccountByStaffDAL : BaseDal<ADOProvider>
    {
        public List<ReportAccountByStaff> GetReportAccountByStaff(BaseListParam listParam, ListFilterParam listFilterParam, int currencyID, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleID", listParam.UserType);
                param.Add("@CurrencyID", currencyID);
                param.Add("@OrganizationUnitId", listParam.DeptId);
                param.Add("@isExcel",0);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                param.Add("@ReportType", listFilterParam.filter7);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total3", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total4", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total5", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total6", dbType: DbType.Double, direction: ParameterDirection.Output);
                //param.Add("@Total7", dbType: DbType.Double, direction: ParameterDirection.Output);
                //param.Add("@Total8", dbType: DbType.Double, direction: ParameterDirection.Output);
                //param.Add("@Total9", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total10", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total11", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total12", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total13", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total14", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total15", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total16", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total17", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total18", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<ReportAccountByStaff>("ReportAccountByStaff", param).ToList();
                param = HttpRuntime.Cache.Get("ReportAccountByStaff-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();
                totalColumns.Total3 = param.GetDataOutput<double>("@Total3").ToString();
                totalColumns.Total4 = param.GetDataOutput<double>("@Total4").ToString();
                totalColumns.Total5 = param.GetDataOutput<double>("@Total5").ToString();
                totalColumns.Total6 = param.GetDataOutput<double>("@Total6").ToString();
                //totalColumns.Total7 = param.GetDataOutput<double>("@Total7").ToString();
                //totalColumns.Total8 = param.GetDataOutput<double>("@Total8").ToString();
                //totalColumns.Total9 = param.GetDataOutput<double>("@Total9").ToString();
                totalColumns.Total10 = param.GetDataOutput<double>("@Total10").ToString();
                totalColumns.Total11 = param.GetDataOutput<double>("@Total11").ToString();
                totalColumns.Total12 = param.GetDataOutput<double>("@Total12").ToString();
                totalColumns.Total13 = param.GetDataOutput<double>("@Total13").ToString();
                totalColumns.Total14 = param.GetDataOutput<double>("@Total14").ToString();
                totalColumns.Total15 = param.GetDataOutput<double>("@Total15").ToString();
                totalColumns.Total16 = param.GetDataOutput<double>("@Total16").ToString();
                totalColumns.Total17 = param.GetDataOutput<double>("@Total17").ToString();
                totalColumns.Total18 = param.GetDataOutput<double>("@Total18").ToString();
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<ReportAccountByStaff>();
            }
        }
        public List<ReportAccountByStaff> ExportReportAccountByStaff(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleID", listParam.UserType);
                param.Add("@OrganizationUnitId", listParam.DeptId);
                param.Add("@isExcel", 1);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                param.Add("@ReportType", listFilterParam.filter7);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);              
                var list = UnitOfWork.Procedure<ReportAccountByStaff>("ReportAccountByStaff", param).ToList();
                param = HttpRuntime.Cache.Get("ReportAccountByStaff-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = list.Count;
                totalColumns = new TableColumnsTotal();
               
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<ReportAccountByStaff>();
            }
        }

    }
}
