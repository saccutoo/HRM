using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Common;
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
    public class PerformanceReportDAL : BaseDal<ADOProvider>
    {
        public List<ReportsPerformanceBD> GetPerformanceReport(BaseListParam listParam,ListFilterParam listFilterParam, out int? totalRecord, out TableColumnsTotal totalColumns)
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
                //param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromMonth", listFilterParam.filter1);
                param.Add("@ToMonth", listFilterParam.filter2);
                param.Add("@FromQuarter", listFilterParam.filter3);
                param.Add("@ToQuarter", listFilterParam.filter4);
                param.Add("@Year", listFilterParam.filter5);
                param.Add("@Type", listFilterParam.filter6);
                param.Add("@ReportType", listFilterParam.filter7);
                param.Add("@ViewType", listFilterParam.viewType);
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
                param.Add("@Total14", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total15", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total16", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total17", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total18", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total19", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total20", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total21", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total22", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total23", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total24", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total25", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total26", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total27", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total28", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total29", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total30", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total31", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total32", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total33", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total34", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total35", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total36", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total37", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total38", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total39", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total40", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<ReportsPerformanceBD>("ReportsPerformanceBD", param).ToList();
                param = HttpRuntime.Cache.Get("ReportsPerformanceBD-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
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
                totalColumns.Total13 = param.GetDataOutput<double>("@Total13").ToString();
                totalColumns.Total14 = param.GetDataOutput<double>("@Total14").ToString();
                totalColumns.Total15 = param.GetDataOutput<double>("@Total15").ToString();
                totalColumns.Total16 = param.GetDataOutput<double>("@Total16").ToString();
                totalColumns.Total17 = param.GetDataOutput<double>("@Total17").ToString();
                totalColumns.Total18 = param.GetDataOutput<double>("@Total18").ToString();
                totalColumns.Total19 = param.GetDataOutput<double>("@Total19").ToString();
                totalColumns.Total20 = param.GetDataOutput<double>("@Total20").ToString();
                totalColumns.Total21 = param.GetDataOutput<double>("@Total21").ToString();
                totalColumns.Total22 = param.GetDataOutput<double>("@Total22").ToString();
                totalColumns.Total23 = param.GetDataOutput<double>("@Total23").ToString();
                totalColumns.Total24 = param.GetDataOutput<double>("@Total24").ToString();
                totalColumns.Total25 = param.GetDataOutput<double>("@Total25").ToString();
                totalColumns.Total26 = param.GetDataOutput<double>("@Total26").ToString();
                totalColumns.Total27 = param.GetDataOutput<double>("@Total27").ToString();
                totalColumns.Total28 = param.GetDataOutput<double>("@Total28").ToString();
                totalColumns.Total29 = param.GetDataOutput<double>("@Total29").ToString();
                totalColumns.Total30 = param.GetDataOutput<double>("@Total30").ToString();
                totalColumns.Total31 = param.GetDataOutput<double>("@Total31").ToString();
                totalColumns.Total32 = param.GetDataOutput<double>("@Total32").ToString();
                totalColumns.Total33 = param.GetDataOutput<double>("@Total33").ToString();
                totalColumns.Total34 = param.GetDataOutput<double>("@Total34").ToString();
                totalColumns.Total35 = param.GetDataOutput<double>("@Total35").ToString();
                totalColumns.Total36 = param.GetDataOutput<double>("@Total36").ToString();
                totalColumns.Total37 = param.GetDataOutput<double>("@Total37").ToString();
                totalColumns.Total38 = param.GetDataOutput<double>("@Total38").ToString();
                totalColumns.Total39 = param.GetDataOutput<double>("@Total39").ToString();
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<ReportsPerformanceBD>();
            }
        }

        public List<ReportsPerformanceBD> PerformanceReportONN(BaseListParam listParam, ListFilterParam listFilterParam, int currencyID, int isExcel , out int? totalRecord, out TableColumnsTotal totalColumns)
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
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromMonth", listFilterParam.filter1);
                param.Add("@ToMonth", listFilterParam.filter2);
                param.Add("@FromQuarter", listFilterParam.filter3);
                param.Add("@ToQuarter", listFilterParam.filter4);
                param.Add("@Year", listFilterParam.filter5);
                param.Add("@Type", listFilterParam.filter6);
                param.Add("@ReportType", listFilterParam.filter7);
                param.Add("@ViewType", listFilterParam.viewType);
                param.Add("@isExcel", isExcel);
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
                param.Add("@Total14", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total15", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total16", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total17", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total18", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total19", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total20", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total21", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total22", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total23", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total24", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total25", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total26", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total27", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total28", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total29", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total30", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total31", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total32", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total33", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total34", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total35", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total36", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total37", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total38", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total39", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total40", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<ReportsPerformanceBD>("PerformanceReportONN", param).ToList();
                param = HttpRuntime.Cache.Get("PerformanceReportONN-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
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
                totalColumns.Total13 = param.GetDataOutput<double>("@Total13").ToString();
                totalColumns.Total14 = param.GetDataOutput<double>("@Total14").ToString();
                totalColumns.Total15 = param.GetDataOutput<double>("@Total15").ToString();
                totalColumns.Total16 = param.GetDataOutput<double>("@Total16").ToString();
                totalColumns.Total17 = param.GetDataOutput<double>("@Total17").ToString();
                totalColumns.Total18 = param.GetDataOutput<double>("@Total18").ToString();
                totalColumns.Total19 = param.GetDataOutput<double>("@Total19").ToString();
                totalColumns.Total20 = param.GetDataOutput<double>("@Total20").ToString();
                totalColumns.Total21 = param.GetDataOutput<double>("@Total21").ToString();
                totalColumns.Total22 = param.GetDataOutput<double>("@Total22").ToString();
                totalColumns.Total23 = param.GetDataOutput<double>("@Total23").ToString();
                totalColumns.Total24 = param.GetDataOutput<double>("@Total24").ToString();
                totalColumns.Total25 = param.GetDataOutput<double>("@Total25").ToString();
                totalColumns.Total26 = param.GetDataOutput<double>("@Total26").ToString();
                totalColumns.Total27 = param.GetDataOutput<double>("@Total27").ToString();
                totalColumns.Total28 = param.GetDataOutput<double>("@Total28").ToString();
                totalColumns.Total29 = param.GetDataOutput<double>("@Total29").ToString();
                totalColumns.Total30 = param.GetDataOutput<double>("@Total30").ToString();
                totalColumns.Total31 = param.GetDataOutput<double>("@Total31").ToString();
                totalColumns.Total32 = param.GetDataOutput<double>("@Total32").ToString();
                totalColumns.Total33 = param.GetDataOutput<double>("@Total33").ToString();
                totalColumns.Total34 = param.GetDataOutput<double>("@Total34").ToString();
                totalColumns.Total35 = param.GetDataOutput<double>("@Total35").ToString();
                totalColumns.Total36 = param.GetDataOutput<double>("@Total36").ToString();
                totalColumns.Total37 = param.GetDataOutput<double>("@Total37").ToString();
                totalColumns.Total38 = param.GetDataOutput<double>("@Total38").ToString();
                totalColumns.Total39 = param.GetDataOutput<double>("@Total39").ToString();
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<ReportsPerformanceBD>();
            }
        }

        public List<ReportsPerformanceBD> ExportExcelPerformanceReport(BaseListParam listParam,ListFilterParam listFilterParam, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@OrganizationUnitId", listParam.DeptId);
                param.Add("@ViewType", listFilterParam.viewType);
                //param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromMonth", listFilterParam.filter1);
                param.Add("@ToMonth", listFilterParam.filter2);
                param.Add("@FromQuarter", listFilterParam.filter3);
                param.Add("@ToQuarter", listFilterParam.filter4);
                param.Add("@Year", listFilterParam.filter5);
                param.Add("@Type", listFilterParam.filter6);
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
                param.Add("@Total14", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total15", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total16", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total17", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total18", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total19", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total20", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total21", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total22", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total23", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total24", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total25", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total26", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total27", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total28", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total29", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total30", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total31", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total32", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total33", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total34", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total35", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total36", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total37", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total38", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total39", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total40", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<ReportsPerformanceBD>("ReportsPerformanceBD", param).ToList();
                param = HttpRuntime.Cache.Get("ReportsPerformanceBD-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
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
                totalColumns.Total13 = param.GetDataOutput<double>("@Total13").ToString();
                totalColumns.Total14 = param.GetDataOutput<double>("@Total14").ToString();
                totalColumns.Total15 = param.GetDataOutput<double>("@Total15").ToString();
                totalColumns.Total16 = param.GetDataOutput<double>("@Total16").ToString();
                totalColumns.Total17 = param.GetDataOutput<double>("@Total17").ToString();
                totalColumns.Total18 = param.GetDataOutput<double>("@Total18").ToString();
                totalColumns.Total19 = param.GetDataOutput<double>("@Total19").ToString();
                totalColumns.Total20 = param.GetDataOutput<double>("@Total20").ToString();
                totalColumns.Total21 = param.GetDataOutput<double>("@Total21").ToString();
                totalColumns.Total22 = param.GetDataOutput<double>("@Total22").ToString();
                totalColumns.Total23 = param.GetDataOutput<double>("@Total23").ToString();
                totalColumns.Total24 = param.GetDataOutput<double>("@Total24").ToString();
                totalColumns.Total25 = param.GetDataOutput<double>("@Total25").ToString();
                totalColumns.Total26 = param.GetDataOutput<double>("@Total26").ToString();
                totalColumns.Total27 = param.GetDataOutput<double>("@Total27").ToString();
                totalColumns.Total28 = param.GetDataOutput<double>("@Total28").ToString();
                totalColumns.Total29 = param.GetDataOutput<double>("@Total29").ToString();
                totalColumns.Total30 = param.GetDataOutput<double>("@Total30").ToString();
                totalColumns.Total31 = param.GetDataOutput<double>("@Total31").ToString();
                totalColumns.Total32 = param.GetDataOutput<double>("@Total32").ToString();
                totalColumns.Total33 = param.GetDataOutput<double>("@Total33").ToString();
                totalColumns.Total34 = param.GetDataOutput<double>("@Total34").ToString();
                totalColumns.Total35 = param.GetDataOutput<double>("@Total35").ToString();
                totalColumns.Total36 = param.GetDataOutput<double>("@Total36").ToString();
                totalColumns.Total37 = param.GetDataOutput<double>("@Total37").ToString();
                totalColumns.Total38 = param.GetDataOutput<double>("@Total38").ToString();
                totalColumns.Total39 = param.GetDataOutput<double>("@Total39").ToString();
                return list;
            }
            catch (Exception e)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<ReportsPerformanceBD>();
            }
        }
    }
}
