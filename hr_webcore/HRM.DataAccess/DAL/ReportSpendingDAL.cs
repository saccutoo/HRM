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
    public class ReportSpendingDAL : BaseDal<ADOProvider>
    {
        public List<ReportSpendingByAccountNumber> GetReportSpendingByAccountNumber(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord, out TableColumnsTotal totalColumns)
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
                param.Add("@OrganizationUnitId", listParam.DeptId);
                param.Add("@isExcel", 0);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                param.Add("@ReportType", listFilterParam.filter7);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total3", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total4", dbType: DbType.Double, direction: ParameterDirection.Output);

                var list = UnitOfWork.Procedure<ReportSpendingByAccountNumber>("ReportSpendingByAccountNumber", param).ToList();
                param = HttpRuntime.Cache.Get("ReportSpendingByAccountNumber-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();
                totalColumns.Total3 = param.GetDataOutput<double>("@Total3").ToString();
                totalColumns.Total4 = param.GetDataOutput<double>("@Total4").ToString();

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<ReportSpendingByAccountNumber>();
            }
        }

        public List<ReportSpendingByAccountNumber> ExportReportSpendingByAccountNumber(BaseListParam listParam, ListFilterParam listFilterParam)
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
                param.Add("@OrganizationUnitId", listParam.DeptId);
                param.Add("@isExcel", 1);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                param.Add("@ReportType", listFilterParam.filter7);

                var list = UnitOfWork.Procedure<ReportSpendingByAccountNumber>("ReportSpendingByAccountNumber", param).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return new List<ReportSpendingByAccountNumber>();
            }
        }

        public List<ReportGeneralSpending> GetReportGeneralSpending(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord, out TableColumnsTotal totalColumns, int isExcel)
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
                param.Add("@OrganizationUnitId", listParam.DeptId);
                param.Add("@isExcel", isExcel);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromMonth", listFilterParam.filter1);
                param.Add("@ToMonth", listFilterParam.filter2);
                param.Add("@Year", listFilterParam.filter5);
                param.Add("@ReportType", listFilterParam.filter7);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total3", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total4", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total5", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total6", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total7", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total8", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total9", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total10", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total11", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total12", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total13", dbType: DbType.Double, direction: ParameterDirection.Output);

                var list = UnitOfWork.Procedure<ReportGeneralSpending>("ReportGeneralSpending", param).ToList();
                param = HttpRuntime.Cache.Get("ReportGeneralSpending-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                foreach (var item in list)
                {
                    item.Total = item.M1 + item.M2 + item.M3 + item.M4 + item.M5 + item.M6 + item.M7 + item.M8 + item.M9 +
                                 item.M10 + item.M11 + item.M12;
                }
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();
                totalColumns.Total3 = param.GetDataOutput<double>("@Total3").ToString();
                totalColumns.Total4 = param.GetDataOutput<double>("@Total4").ToString();
                totalColumns.Total5 = param.GetDataOutput<double>("@Total5").ToString();
                totalColumns.Total6 = param.GetDataOutput<double>("@Total6").ToString();
                totalColumns.Total7 = param.GetDataOutput<double>("@Total7").ToString();
                totalColumns.Total8 = param.GetDataOutput<double>("@Total8").ToString();
                totalColumns.Total9 = param.GetDataOutput<double>("@Total9").ToString();
                totalColumns.Total10 = param.GetDataOutput<double>("@Total10").ToString();
                totalColumns.Total11 = param.GetDataOutput<double>("@Total11").ToString();
                totalColumns.Total12 = param.GetDataOutput<double>("@Total12").ToString();
                totalColumns.Total13 = (double.Parse(totalColumns.Total1) + double.Parse(totalColumns.Total2) + double.Parse(totalColumns.Total3) +
                                       double.Parse(totalColumns.Total4) + double.Parse(totalColumns.Total5) + double.Parse(totalColumns.Total6) +
                                       double.Parse(totalColumns.Total7) + double.Parse(totalColumns.Total8) + double.Parse(totalColumns.Total9) +
                                       double.Parse(totalColumns.Total10) + double.Parse(totalColumns.Total11) + double.Parse(totalColumns.Total12)).ToString();
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<ReportGeneralSpending>();
            }
        }

    }
}
