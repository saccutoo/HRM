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
    public class CustomerTransferHistoryDAL : BaseDal<ADOProvider>
    {
        public List<CustomerTransferHistory> getCustomerTransferHistory(BaseListParam listParam, ListFilterParam listFilterParam, int currencyID, out int? totalRecord, out TableColumnsTotal totalColumns)
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
                //param.Add("@OrganizationUnitId", listParam.DeptId);
                param.Add("@isExcel", 0);
                param.Add("@CurrencyID", currencyID);
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
                var list = UnitOfWork.Procedure<CustomerTransferHistory>("CustomerTransferHistory", param).ToList();
                param = HttpRuntime.Cache.Get("CustomerTransferHistory-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();
                totalColumns.Total3 = param.GetDataOutput<double>("@Total3").ToString();
                totalColumns.Total4 = param.GetDataOutput<double>("@Total4").ToString();
                totalColumns.Total5 = param.GetDataOutput<double>("@Total5").ToString();
                totalColumns.Total6 = param.GetDataOutput<double>("@Total6").ToString();

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<CustomerTransferHistory>();
            }
        }
        public List<CustomerTransferHistory> getCustomerTransferHistoryForExcel(BaseListParam listParam, ListFilterParam listFilterParam, int currencyID, out int? totalRecord, out TableColumnsTotal totalColumns)
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
                //param.Add("@OrganizationUnitId", listParam.DeptId);
                param.Add("@isExcel", 1);
                param.Add("@CurrencyID", currencyID);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                //param.Add("@ReportType", listFilterParam.filter7);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                
                var list = UnitOfWork.Procedure<CustomerTransferHistory>("sp_HistoryTransferCustomer", param).ToList();
                param = HttpRuntime.Cache.Get("sp_HistoryTransferCustomer-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<CustomerTransferHistory>();
            }
        }

    }
}
