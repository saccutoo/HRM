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
    public class DetailAccountReportDAL : BaseDal<ADOProvider> 
    {
        public List<DetailReportAccount> ReportMccAccountDetailNew(BaseListParam listParam, ListFilterParam filterParam, int currencyID, out int? totalRecord,out TableColumnsTotal totalColumns)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderBy", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@DateFrom", filterParam.FromDate);
                param.Add("@DateTo", filterParam.ToDate);
                param.Add("@UserType", listParam.UserType);
                param.Add("@CurrencyID", currencyID);
                param.Add("@isExcel", false);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<DetailReportAccount>("ReportMccAccountDetailNew", param).ToList();
                param = HttpRuntime.Cache.Get("ReportMccAccountDetailNew-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<DetailReportAccount>();
            }


        }

        public List<DetailReportAccount> ExportExcelDetailAccountReport(BaseListParam listParam, ListFilterParam filterParam)
        {

            try
            {

                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderBy", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@DateFrom", filterParam.FromDate);
                param.Add("@DateTo", filterParam.ToDate);
                param.Add("@UserType", listParam.UserType);
                param.Add("@CurrencyID", 194);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@isExcel", true);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<DetailReportAccount>("ReportMccAccountDetailNew", param).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<DetailReportAccount>();
            }
        }
    }
}
