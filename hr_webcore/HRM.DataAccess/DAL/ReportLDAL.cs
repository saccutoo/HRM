using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Common;
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
    public class ReportLDAL : BaseDal<ADOProvider>
    {
        public List<ReportL> Report_L_By_Staff_BDX(BaseListParam listParam,string startdate, string enddate, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(startdate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                startdate = dt.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dt1 = DateTime.ParseExact(enddate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                enddate = dt1.ToString("yyyy-MM-dd HH:mm:ss");
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderBy", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserType", listParam.UserType);
                param.Add("@UserId", listParam.UserId);
                param.Add("@LanguageId", listParam.LanguageCode);
                param.Add("@startdate", startdate);
                param.Add("@enddate", enddate);
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
                var list = UnitOfWork.Procedure<ReportL>("Report_L_By_Staff_BDX", param).ToList();
                param = HttpRuntime.Cache.Get("Report_L_By_Staff_BDX-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
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
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<ReportL>();
            }
        }

        public List<ReportL> Report_L_By_Department_BDX(BaseListParam listParam, string startdate, string enddate, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(startdate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                startdate = dt.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dt1 = DateTime.ParseExact(enddate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                enddate = dt1.ToString("yyyy-MM-dd HH:mm:ss");
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderBy", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserType", listParam.UserType);
                param.Add("@UserId", listParam.UserId);
                param.Add("@LanguageId", listParam.LanguageCode);
                param.Add("@startdate", startdate);
                param.Add("@enddate", enddate);
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
                var list = UnitOfWork.Procedure<ReportL>("Report_L_By_Department_BDX", param).ToList();
                param = HttpRuntime.Cache.Get("Report_L_By_Department_BDX-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
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
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<ReportL>();
            }
        }
    }
}
